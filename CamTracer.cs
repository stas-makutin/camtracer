using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Reflection;
using System.Xml;
using Emgu.CV;
using Emgu.CV.Structure;

namespace camtracer
{
    public partial class CamTracer : Form
    {
        private enum Mode
        {
            Preview,
            ResultView,
            Trace
        }

        private int _cameraIndex = 0;
        private Mode _mode = Mode.Preview;
        private VideoCapture _capture = null;
        private AutoResetEvent _resultReset = new AutoResetEvent(false);
        private Image<Bgr, Byte> _resultImage = null;
        private long _frameCount = 0;
        private System.Threading.Timer _fpsTimer = null;
        private Stopwatch _fpsWatch = null;
        private AutoResetEvent _modelChanged = new AutoResetEvent(true);
        private object _modelSync = new object();
        private int _method = 0;
        private double _threshold = 0.15;
        private double _alpha = 0.85;
        private double _colorAlpha = 0.7;
        private List<Color> colors = ColorsDialog.getDefaultColors();
        private Dictionary<Keys, int> keysToColorIndex = ColorsDialog.getDefaultColorKeys();
        private Size colorsDlgSize = new Size(0, 0);
        private int colorIndex = 0;

        public CamTracer()
        {
            InitializeComponent();
        }

        private void CamTracer_Load(object sender, EventArgs e)
        {
            lock (_modelSync)
            {
                LoadState();
                updateColorPanels();
            }

            methodBox.SelectedIndex = _method;
            thresholdField.Value = (decimal)_threshold;
            thresholdBar.Value = (int)(_threshold * 255);
            alphaField.Value = (decimal)_alpha;
            alphaBar.Value = (int)(_alpha * 255);
            colorAlphaField.Value = (decimal)_colorAlpha;
            colorAlphaBar.Value = (int)(_colorAlpha * 255);

            string name = "Camera #0";
            string[] names = SelectCameraDialog.GetNames();
            if (null != names && names.Length > 0)
            {
                if (_cameraIndex < 0 || _cameraIndex >= names.Length)
                    _cameraIndex = 0;
                name = names[_cameraIndex];
            }
            else
            {
                _cameraIndex = 0;
            }

            capture_Init(_cameraIndex, name);
        }

        private void CamTracer_FormClosing(object sender, FormClosingEventArgs e)
        {
            capture_Close();
            if (null != _fpsTimer)
            {
                _fpsTimer.Dispose();
                _fpsTimer = null;
            }
            lock (_modelSync)
            {
                SaveState();
            }
            _modelChanged.Close();
            ResetImage();
        }

        private bool capture_Open(int cameraIndex, string cameraName)
        {
            try
            {
                _capture = new VideoCapture(cameraIndex);
                _capture.ImageGrabbed += capture_ImageGrabbed;
                _capture.Start();
                _cameraIndex = cameraIndex;
                Text = String.Concat("CamTracer - ", cameraName);
                if (null == _fpsTimer)
                    _fpsTimer = new System.Threading.Timer(fpsTimerCallback, this,  Timeout.Infinite, 0);
                _fpsTimer.Change(0, 3000);
                return true;
            }
            catch (Exception /*e*/)
            {
                capture_Close();
            }
            return false;
        }

        private void capture_Close()
        {
            try
            {
                if (null != _fpsTimer)
                    _fpsTimer.Change(Timeout.Infinite, 0);

                if (null != _capture)
                {
                    _capture.Stop();
                    _capture.Dispose();
                }

                _resultImage = null;
                _colorImage = null;
            }
            catch
            {
            }

            _capture = null;
            Interlocked.Exchange(ref _frameCount, 0);
        }

        private void capture_Init(int cameraIndex, string cameraName)
        {
            Cursor current = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            capture_Close();

            if (!capture_Open(cameraIndex, cameraName))
            {
                string[] names = SelectCameraDialog.GetNames();
                bool failure = true;
                if (null != names && names.Length > 0)
                {
                    int index = cameraIndex + 1;
                    while (index != cameraIndex)
                    {
                        if (index >= names.Length)
                        {
                            index = 0;
                            continue;
                        }

                        if (capture_Open(index, names[index]))
                        {
                            failure = false;
                            break;
                        }

                        index++;
                    }
                }

                if (failure)
                {
                    MessageBox.Show(this, "Unable to connect to a camera, the application will be closed.", "CamTracer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }

            Cursor.Current = current;
        }

        private IntPtr _imageToRender = IntPtr.Zero;
        private Size _imageToRenderSize;
        private Object _imageToRenderLock = new Object();
        private static Size emptySize = new Size(0, 0);

        private void pictureBoxPaint(Graphics g, Rectangle clipRectange)
        {
            lock (_imageToRenderLock)
            {
                if (IntPtr.Zero != _imageToRender)
                {
                    Rectangle viewRec = pictureBox.ClientRectangle;
                    Size imageSz = _imageToRenderSize;
                    int x = 0;
                    int y = 0;
                    int w = viewRec.Width;
                    int h = (imageSz.Height * viewRec.Width) / imageSz.Width;

                    if (h > viewRec.Height)
                    {
                        w = (imageSz.Width * viewRec.Height) / imageSz.Height;
                        h = viewRec.Height;
                        x = (viewRec.Width - w) / 2;
                    }
                    else
                    {
                        y = (viewRec.Height - h) / 2;
                    }

                    if (x > 0)
                    {
                        Rectangle rec = new Rectangle(viewRec.Left, viewRec.Top, viewRec.Left + x, viewRec.Bottom);
                        rec.Intersect(clipRectange);
                        if (!rec.IsEmpty)
                            g.FillRectangle(SystemBrushes.ControlDark, rec);
                        rec = new Rectangle(viewRec.Left + x + w, viewRec.Top, viewRec.Right, viewRec.Bottom);
                        rec.Intersect(clipRectange);
                        if (!rec.IsEmpty)
                            g.FillRectangle(SystemBrushes.ControlDark, rec);
                    }

                    if (y > 0)
                    {
                        Rectangle rec = new Rectangle(viewRec.Left, viewRec.Top, viewRec.Right, viewRec.Top + y);
                        rec.Intersect(clipRectange);
                        if (!rec.IsEmpty)
                            g.FillRectangle(SystemBrushes.ControlDark, rec);
                        rec = new Rectangle(viewRec.Left, viewRec.Top + y + h, viewRec.Right, viewRec.Bottom);
                        rec.Intersect(clipRectange);
                        if (!rec.IsEmpty)
                            g.FillRectangle(SystemBrushes.ControlDark, rec);
                    }

                    IntPtr destHdc = g.GetHdc();
                    IntPtr srcHdc = GDI.CreateCompatibleDC(destHdc);

                    IntPtr prevBmp = GDI.SelectObject(srcHdc, _imageToRender);
                    GDI.StretchBlt(destHdc, viewRec.Left + x, viewRec.Top + y, w, h, 
                        srcHdc, 0, 0, imageSz.Width, imageSz.Height, GDI.TernaryRasterOperations.SRCCOPY);
                    GDI.SelectObject(srcHdc, prevBmp);

                    GDI.DeleteDC(srcHdc);
                    g.ReleaseHdc(destHdc);
                }
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            pictureBoxPaint(e.Graphics, e.ClipRectangle);
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            pictureBox.Invalidate();
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            ToggleFullScreen();
        }

        private void ResetImage()
        {
            lock (_imageToRenderLock)
            {
                if (IntPtr.Zero != _imageToRender)
                {
                    GDI.DeleteObject(_imageToRender);
                    _imageToRender = IntPtr.Zero;
                }
                _imageToRenderSize = emptySize;
            }
        }

        private void RenderImage(IImage image)
        {
            IntPtr imageToRender = IntPtr.Zero;
            Size imageToRenderSize = emptySize;
            if (null != image)
            {
                imageToRender = image.Bitmap.GetHbitmap();
                imageToRenderSize = image.Size;
            }

            lock (_imageToRenderLock)
            {
                if (IntPtr.Zero != _imageToRender)
                    GDI.DeleteObject(_imageToRender);
                _imageToRender = imageToRender;
                _imageToRenderSize = imageToRenderSize;
            }

            using (Graphics g = pictureBox.CreateGraphics())
                pictureBoxPaint(g, pictureBox.ClientRectangle);
        }

        private static Bgr bgrWhiteColor = new Bgr(255, 255, 255);
        private static Hls hlsWhiteColor = new Hls(255, 255, 255);
        private int activeMethod = 0;
        private double activeThreshold = 0;
        private double activeAlpha = 0;
        private Hls thresholdColor = new Hls(0, 0, 0);
        private Bgr activeColor = new Bgr(0, 128, 0);
        private double activeColorAlpha = 0;
        private Image<Bgr, Byte> _previousImage = null;
        private Bgr _previousActiveColor = new Bgr(0, 0, 0);
        private Image<Bgr, Byte> _colorImage = null;

        void capture_ImageGrabbed(object sender, EventArgs e)
        {
            if (Mode.ResultView == _mode)
            {
                return;
            }

            Interlocked.Increment(ref _frameCount);

            Mat mat = new Mat();
            _capture.Read(mat);

            if (Mode.Preview == _mode)
            {
                RenderImage(mat);
                return;
            }

            if (_resultReset.WaitOne(0))
                _resultImage = null;

            if (_modelChanged.WaitOne(0))
            {
                lock (_modelSync)
                {
                    activeMethod = _method;
                    activeThreshold = _threshold;
                    activeAlpha = _alpha;
                    activeColorAlpha = _colorAlpha;
                    if (colorIndex < colors.Count)
                        activeColor = new Bgr(colors[colorIndex]);
                    else
                        activeColorAlpha = 0;
                }
                thresholdColor = new Hls(0, 255 * activeThreshold, 0);
            }

            Image<Bgr, Byte> frame = mat.ToImage<Bgr,Byte>();

            if (null == _resultImage)
            {
                _previousImage = frame.Clone();
                _resultImage = frame.Clone();
            }
            else 
            {
                Image<Gray, byte> mask = null;

                if (0 == activeMethod)
                {
                    mask = frame.Sub(_previousImage).Convert<Hls, byte>().ThresholdBinary(thresholdColor, hlsWhiteColor).Convert<Gray, byte>();
                }
                else
                {
                    mask = _previousImage.Sub(frame).Convert<Hls, byte>().ThresholdBinary(thresholdColor, hlsWhiteColor).Convert<Gray, byte>();
                }

                Image<Bgr, Byte> basis = _previousImage.And(bgrWhiteColor, mask.Not());
                Image<Bgr, Byte> applied = _previousImage.And(bgrWhiteColor, mask).AddWeighted(frame.And(bgrWhiteColor, mask), 1.0 - activeAlpha, activeAlpha, 0.0);

                _previousImage = basis.Add(applied);

                if (activeColorAlpha > 0)
                {
                    if (null == _colorImage || !activeColor.Equals(_previousActiveColor))
                    {
                        _colorImage = new Image<Bgr, Byte>(applied.Width, applied.Height, activeColor);
                        _previousActiveColor = activeColor;
                    }

                    _resultImage = _resultImage.And(bgrWhiteColor, mask.Not())
                        .Add(applied.AddWeighted(_colorImage.And(bgrWhiteColor, mask), 1.0 - activeColorAlpha, activeColorAlpha, 0.0));
                }
                else
                {
                    _resultImage = _previousImage;
                }
            }

            RenderImage(_resultImage);
        }

        #pragma warning disable CS0197
        public static void fpsTimerCallback(Object state)
        {
            CamTracer This = (CamTracer)state;
            long frames = Interlocked.Exchange(ref This._frameCount, 0);
            if (null == This._fpsWatch)
            {
                This._fpsWatch = new Stopwatch();
                This._fpsWatch.Start();
            }
            else
            {
                This._fpsWatch.Stop();
                float fps = (1000.0f * frames) / This._fpsWatch.ElapsedMilliseconds;
                This._fpsWatch.Reset();
                This._fpsWatch.Start();

                This.fpsLabel.Invoke((MethodInvoker)delegate {
                    This.fpsLabel.Text = String.Format("{0:0.00} fps", fps);
                });
            }
        }
        #pragma warning restore CS0197

        private void changeCameraBtn_Click(object sender, EventArgs e)
        {
            SelectCameraDialog dlg = new SelectCameraDialog(_cameraIndex);
            if (DialogResult.OK == dlg.ShowDialog(this))
                capture_Init(dlg.CameraIndex, dlg.CameraName);
        }

        private void previewBtn_Click(object sender, EventArgs e)
        {
            if (Mode.ResultView == _mode)
            {
                _mode = Mode.Preview;
                previewBtn.Text = "R&esult";
            }
            else if (Mode.Preview == _mode)
            {
                _mode = Mode.ResultView;
                previewBtn.Text = "Pr&eview";
                RenderImage(_resultImage);
            }
        }

        private void startStopBtn_Click(object sender, EventArgs e)
        {
            if (Mode.Trace == _mode)
            {
                _mode = Mode.ResultView;
                startStopBtn.Text = "&Start";
                previewBtn.Text = "Pr&eview";
                previewBtn.Enabled = true;
            }
            else
            {
                startStopBtn.Text = "&Stop";
                previewBtn.Enabled = false;
                _mode = Mode.Trace;
                saveResultBtn.Enabled = true;
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            _resultReset.Set();

            if (Mode.Trace == _mode)
                return;

            if (Mode.ResultView == _mode)
                previewBtn_Click(null, null);

            previewBtn.Enabled = false;
            saveResultBtn.Enabled = false;
            _resultImage = null;
        }

        private void saveResultBtn_Click(object sender, EventArgs e)
        {
            if (null != _resultImage)
            {
                Image<Bgr, Byte> imageToSave = _resultImage.Clone();
                
                SaveFileDialog dlg = new SaveFileDialog();

                dlg.Filter = "JPEG files (*.jpg)|*.jpg|BMP files (*.bmp)|*.bmp|PNG files (*.png)|*.png|TIFF files (*.tiff)|*.tiff"  ;
                dlg.FilterIndex = 1;
                dlg.RestoreDirectory = true;

                if (DialogResult.OK == dlg.ShowDialog(this))
                {
                    try
                    {
                        Cursor current = Cursor.Current;
                        Cursor.Current = Cursors.WaitCursor;
                        imageToSave.Save(dlg.FileName);
                        Cursor.Current = current;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, String.Format("Unable to save the file %s, error details: %s", dlg.FileName, ex.Message ?? "Unknown"), "CamTracer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void methodBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int method = methodBox.SelectedIndex;
            if (method < 0) method = 0;
            if (method > 1) method = 1;
            lock (_modelSync)
                _method = method;
            _modelChanged.Set();
        }

        private bool _acceptThresholdChange = true;

        private void thresholdField_ValueChanged(object sender, EventArgs e)
        {
            if (_acceptThresholdChange)
            {
                _acceptThresholdChange = false;
                double threshold = (double)thresholdField.Value;
                thresholdBar.Value = (int)(255 * threshold);
                lock (_modelSync)
                    _threshold = threshold;
                _modelChanged.Set();
                _acceptThresholdChange = true;
            }
        }

        private void thresholdBar_ValueChanged(object sender, EventArgs e)
        {
            if (_acceptThresholdChange)
            {
                _acceptThresholdChange = false;
                double threshold = (double)thresholdBar.Value / 255.0;
                thresholdField.Value = (decimal)threshold;
                lock (_modelSync)
                    _threshold = threshold;
                _modelChanged.Set();
                _acceptThresholdChange = true;
            }
        }

        private bool _acceptAlphaChange = true;

        private void alphaField_ValueChanged(object sender, EventArgs e)
        {
            if (_acceptAlphaChange)
            {
                _acceptAlphaChange = false;
                double alpha = (double)alphaField.Value;
                alphaBar.Value = (int)(255 * alpha);
                lock (_modelSync)
                    _alpha = alpha;
                _modelChanged.Set();
                _acceptAlphaChange = true;
            }

        }

        private void alphaBar_ValueChanged(object sender, EventArgs e)
        {
            if (_acceptAlphaChange)
            {
                _acceptAlphaChange = false;
                double alpha = (double)alphaBar.Value / 255.0;
                alphaField.Value = (decimal)alpha;
                lock (_modelSync)
                    _alpha = alpha;
                _modelChanged.Set();
                _acceptAlphaChange = true;
            }
        }

        private void colorsButton_Click(object sender, EventArgs e)
        {
            int _colorIndex = 0;
            List<Color> _colors = null;
            Dictionary<Keys, int> _keysToColorIndex = null;
            lock (_modelSync)
            {
                _colors = new List<Color>(colors);
                _keysToColorIndex = new Dictionary<Keys, int>(keysToColorIndex);
                _colorIndex = colorIndex;
            }
            ColorsDialog dlg = new ColorsDialog(_colorIndex, _colors, _keysToColorIndex);
            if (!colorsDlgSize.IsEmpty)
                dlg.Size = colorsDlgSize;
            if (DialogResult.OK == dlg.ShowDialog(this))
            {
                lock (_modelSync)
                {
                    colors = dlg.Colors;
                    keysToColorIndex = dlg.ColorKeys;
                    colorIndex = dlg.ColorIndex;
                    if (colorIndex >= colors.Count)
                        colorIndex = 0;
                    updateColorPanels();
                }
                _modelChanged.Set();
            }
            colorsDlgSize = dlg.Size;
        }

        private void nextColorButton_Click(object sender, EventArgs e)
        {
            lock (_modelSync)
            {
                colorIndex++;
                if (colorIndex >= colors.Count)
                    colorIndex = 0;
                updateColorPanels();
            }
            _modelChanged.Set();
        }

        private void updateColorPanels()
        {
            if (colorIndex < colors.Count)
            {
                panelCurrentColor.BorderStyle = BorderStyle.FixedSingle;
                panelNextColor.BorderStyle = BorderStyle.FixedSingle;

                panelCurrentColor.BackColor = colors[colorIndex];
                int index = colorIndex + 1;
                if (index >= colors.Count)
                    index = 0;
                panelNextColor.BackColor = colors[index];
            }
            else
            {
                panelCurrentColor.BorderStyle = BorderStyle.None;
                panelNextColor.BorderStyle = BorderStyle.None;
                panelCurrentColor.BackColor = SystemColors.Control;
                panelNextColor.BackColor = SystemColors.Control;
            }
        }

        private bool _acceptColorAlphaChange = true;

        private void colorAlphaBar_ValueChanged(object sender, EventArgs e)
        {
            if (_acceptColorAlphaChange)
            {
                _acceptColorAlphaChange = false;
                double colorAlpha = (double)colorAlphaBar.Value / 255.0;
                colorAlphaField.Value = (decimal)colorAlpha;
                lock (_modelSync)
                    _colorAlpha = colorAlpha;
                _modelChanged.Set();
                _acceptColorAlphaChange = true;
            }
        }

        private void colorAlphaField_ValueChanged(object sender, EventArgs e)
        {
            if (_acceptColorAlphaChange)
            {
                _acceptColorAlphaChange = false;
                double colorAlpha = (double)colorAlphaField.Value;
                colorAlphaBar.Value = (int)(255 * colorAlpha);
                lock (_modelSync)
                    _colorAlpha = colorAlpha;
                _modelChanged.Set();
                _acceptColorAlphaChange = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((Keys.Alt | Keys.Enter) == keyData)
            {
                ToggleFullScreen();
                return true;
            }
            else if (Keys.S == keyData)
            {
                startStopBtn_Click(null, null);
                return true;
            }            
            else if (Keys.Space == keyData)
            {
                nextColorButton_Click(null, null);
                return true;
            }
            else
            {
                lock (_modelSync)
                {
                    int index = 0;
                    if (keysToColorIndex.TryGetValue(keyData, out index))
                    {
                        colorIndex = index;
                        _modelChanged.Set();
                        updateColorPanels();
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private FormWindowState _formWindowsState = FormWindowState.Normal;
        private Size _pictureBoxSize;

        private void ToggleFullScreen()
        {
            if (FormBorderStyle.None == FormBorderStyle)
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = _formWindowsState;
                pictureBox.Size = _pictureBoxSize;
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
                pictureBox.BackColor = SystemColors.ControlDark;
                pictureBox.SendToBack();
            }
            else
            {
                _formWindowsState = WindowState;
                _pictureBoxSize = pictureBox.Size;
                WindowState = FormWindowState.Normal;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                pictureBox.Size = this.Size;
                pictureBox.BorderStyle = BorderStyle.None;
                pictureBox.BackColor = Color.Black;
                pictureBox.BringToFront();
            }
        }

        private string GetStateXMLName()
        {
            string result = null;
            try
            {
                result = Assembly.GetEntryAssembly().Location;
            }
            catch
            {
            }
            if (String.IsNullOrEmpty(result))
            {
                result = "CamTracer";
            }
            return String.Concat(result, ".state");
        }

        private void SaveState()
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
                doc.AppendChild(doc.CreateElement("camTracer"));

                if (FormBorderStyle.None == FormBorderStyle)
                {
                    ToggleFullScreen();
                }

                doc.DocumentElement.SetAttribute("state", ((int)WindowState).ToString());
                WindowState = FormWindowState.Normal;
                doc.DocumentElement.SetAttribute("x", Location.X.ToString());
                doc.DocumentElement.SetAttribute("y", Location.Y.ToString());
                doc.DocumentElement.SetAttribute("width", Size.Width.ToString());
                doc.DocumentElement.SetAttribute("height", Size.Height.ToString());

                doc.DocumentElement.SetAttribute("cameraIndex", _cameraIndex.ToString());

                doc.DocumentElement.SetAttribute("method", _method.ToString());
                doc.DocumentElement.SetAttribute("threshold", _threshold.ToString());
                doc.DocumentElement.SetAttribute("alpha", _alpha.ToString());
                doc.DocumentElement.SetAttribute("colorAlpha", _colorAlpha.ToString());
                doc.DocumentElement.SetAttribute("colorsWidth", colorsDlgSize.Width.ToString());
                doc.DocumentElement.SetAttribute("colorsHeight", colorsDlgSize.Height.ToString());

                Dictionary<int, Keys> colorIndexToKeys = new Dictionary<int, Keys>(keysToColorIndex.Count);
                foreach (KeyValuePair<Keys, int> keysToIndex in keysToColorIndex)
                    colorIndexToKeys.Add(keysToIndex.Value, keysToIndex.Key);

                XmlElement colorsElement = doc.DocumentElement.AppendChild(doc.CreateElement("colors")) as XmlElement;
                for (int index = 0; index < colors.Count; index++)
                {
                    Color color = colors[index];
                    Keys keys = Keys.None;

                    colorIndexToKeys.TryGetValue(index, out keys);

                    XmlElement colorElement = colorsElement.AppendChild(doc.CreateElement("color")) as XmlElement;
                    colorElement.AppendChild(doc.CreateTextNode(color.ToArgb().ToString()));
                    if (Keys.None != keys)
                        colorElement.SetAttribute("keys", ((int)keys).ToString());
                }

                doc.Save(GetStateXMLName());
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        private void LoadState()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(GetStateXMLName());
            }
            catch
            {
                return;
            }

            if (doc.DocumentElement.Name != "camTracer") return;

            int x = GetXmlAttribute(doc.DocumentElement, "x", Location.X);
            int y = GetXmlAttribute(doc.DocumentElement, "y", Location.Y);
            int w = GetXmlAttribute(doc.DocumentElement, "width", Size.Width);
            int h = GetXmlAttribute(doc.DocumentElement, "height", Size.Height);
            FormWindowState state = FormWindowState.Normal;
            try
            {
                state = (FormWindowState)GetXmlAttribute(doc.DocumentElement, "state", (int)WindowState);
            }
            catch
            {
                state = FormWindowState.Normal;
            }
            
            Location = new Point(x, y);
            Size = new Size(w, h);
            if (FormWindowState.Minimized == state)
                state = FormWindowState.Normal;
            WindowState = state;

            _cameraIndex = GetXmlAttribute(doc.DocumentElement, "cameraIndex", _cameraIndex);

            _method = GetXmlAttribute(doc.DocumentElement, "method", _method);
            _threshold = GetXmlAttribute(doc.DocumentElement, "threshold", _threshold);
            _alpha = GetXmlAttribute(doc.DocumentElement, "alpha", _alpha);
            _colorAlpha = GetXmlAttribute(doc.DocumentElement, "colorAlpha", _colorAlpha);

            int colorsWidth = GetXmlAttribute(doc.DocumentElement, "colorsWidth", colorsDlgSize.Width);
            int colorsHeight = GetXmlAttribute(doc.DocumentElement, "colorsHeight", colorsDlgSize.Height);
            colorsDlgSize = new Size(colorsWidth, colorsHeight);

            XmlElement colorsElement = doc.DocumentElement.SelectSingleNode("colors") as XmlElement;
            if (null != colorsElement)
            {
                colors.Clear();
                foreach (XmlNode node in colorsElement.ChildNodes)
                {
                    if (node.NodeType == XmlNodeType.Element && node.LocalName == "color")
                    {
                        int val = 0;
                        if (int.TryParse(node.InnerText, out val))
                        {
                            Color color = Color.FromArgb(val);
                            Keys keys = (Keys)GetXmlAttribute(node, "keys", (int)Keys.None);

                            if (Keys.None != keys)
                                keysToColorIndex[keys] = colors.Count;
                            colors.Add(color);
                        }
                    }
                }
            }
        }

        private string GetXmlAttribute(XmlNode node, string name, string defaultValue)
        {
            string rc;
            try
            {
                rc = ((XmlElement)node).GetAttribute(name);
            }
            catch
            {
                rc = defaultValue;
            }
            return rc;
        }

        private int GetXmlAttribute(XmlNode node, string name, int defaultValue)
        {
            int rc;
            if (!int.TryParse(GetXmlAttribute(node, name, null), out rc))
                rc = defaultValue;
            return rc;
        }

        private double GetXmlAttribute(XmlNode node, string name, double defaultValue)
        {
            double rc;
            if (!double.TryParse(GetXmlAttribute(node, name, null), out rc))
                rc = defaultValue;
            return rc;
        }

    }
}

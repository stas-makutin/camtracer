using System;
using System.Windows.Forms;
using DirectShowLib;

namespace camtracer
{
    public partial class SelectCameraDialog : Form
    {
        private int _cameraIndex;
        private string _cameraName;

        public SelectCameraDialog(int camerIndex)
        {
            _cameraIndex = camerIndex;
            _cameraName = String.Empty;
            InitializeComponent();
            AcceptButton = okBtn;
        }

        public static string[] GetNames()
        {
            string[] rc = null;
            try
            {
                DsDevice[] devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
                if (null != devices)
                {
                    int count = devices.GetLength(0);
                    if (count > 0)
                    {
                        rc = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            DsDevice device = devices[i];
                            string name = null;
                            if (null != device)
                                name = device.Name;
                            if (String.IsNullOrEmpty(name))
                                name = String.Format("Camera #{0}", i);
                            rc[i] = name;
                        }
                    }
                }
            }
            catch
            {
                rc = null;
            }
            return rc;
        }

        public int CameraIndex
        {
            get { return _cameraIndex; }
        }

        public string CameraName
        {
            get { return _cameraName; }
        }

        private void SelectCameraDialog_Load(object sender, EventArgs e)
        {
            string[] names = GetNames();
            if (null == names || names.Length <= 0)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return;  
            }

            foreach (String name in names)
                devicesBox.Items.Add(name);

            if (_cameraIndex < 0 || _cameraIndex >= names.Length)
                _cameraIndex = 0;
            _cameraName = names[_cameraIndex];

            devicesBox.SelectedIndex = _cameraIndex;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            _cameraIndex = devicesBox.SelectedIndex;
            _cameraName = devicesBox.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

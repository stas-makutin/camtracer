using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace camtracer
{
    public partial class ColorsDialog : Form, IMessageFilter
    {
        private class ColorWithKey
        {
            public Color color;
            public Keys keys;

            public ColorWithKey(Color color, Keys keys)
            {
                this.color = color;
                this.keys = keys;
            }

            public ColorWithKey(Color color) : this(color, Keys.None)
            {
            }
        }

        private int colorIndex;
        private List<Color> colors;
        private Dictionary<Keys, int> keysToColorIndex;
        private HashSet<Keys> reservedKeys;
        private String longestKeyString = String.Empty;
        private static KeysConverter keysConverter = new KeysConverter();
        private bool captureHotkey = false;

        public ColorsDialog(int colorIndex, List<Color> colors, Dictionary<Keys, int> keysToColorIndex)
        {
            this.colorIndex = colorIndex;
            this.colors = colors;
            this.keysToColorIndex = keysToColorIndex;

            FillReservedKeys();

            InitializeComponent();
            AcceptButton = okButton;
        }

        public static List<Color> getDefaultColors()
        {
            List<Color> rv = new List<Color>();

            rv.Add(Color.LawnGreen);
            rv.Add(Color.Red);
            rv.Add(Color.Blue);
            rv.Add(Color.Fuchsia);
            rv.Add(Color.Orange);
            rv.Add(Color.Cyan);

            return rv;
        }

        public static Dictionary<Keys, int> getDefaultColorKeys()
        {
            Dictionary<Keys, int> rv = new Dictionary<Keys, int>();

            rv.Add(Keys.G, 0);
            rv.Add(Keys.R, 1);
            rv.Add(Keys.B, 2);
            rv.Add(Keys.P, 3);
            rv.Add(Keys.O, 4);
            rv.Add(Keys.C, 5);

            return rv;
        }


        public int ColorIndex
        {
            get { return colorIndex; }
        }

        public List<Color> Colors
        {
            get { return colors; }
        }

        public Dictionary<Keys, int> ColorKeys
        {
            get { return keysToColorIndex; }
        }

        private void FillReservedKeys()
        {
            reservedKeys = new HashSet<Keys>();

            reservedKeys.Add(Keys.S);
            reservedKeys.Add(Keys.Space);
            reservedKeys.Add(Keys.Alt | Keys.Enter);
            reservedKeys.Add(Keys.Alt | Keys.C);
            reservedKeys.Add(Keys.Alt | Keys.E);
            reservedKeys.Add(Keys.Alt | Keys.S);
            reservedKeys.Add(Keys.Alt | Keys.R);
            reservedKeys.Add(Keys.Alt | Keys.V);
            reservedKeys.Add(Keys.Alt | Keys.M);
            reservedKeys.Add(Keys.Alt | Keys.T);
            reservedKeys.Add(Keys.Alt | Keys.O);
            reservedKeys.Add(Keys.Alt | Keys.L);
            reservedKeys.Add(Keys.Alt | Keys.N);
            reservedKeys.Add(Keys.Alt | Keys.Y);
        }

        private void initalizeItems()
        {
            foreach (Color color in colors)
                listBox.Items.Add(new ColorWithKey(color));

            foreach (KeyValuePair<Keys, int> keyToIndex in keysToColorIndex)
            {
                if (keyToIndex.Value >= 0 && keyToIndex.Value < colors.Count)
                {
                    ((ColorWithKey)listBox.Items[keyToIndex.Value]).keys = keyToIndex.Key;
                }
            }

            findLongestKeyString();
        }

        private static char keyStringFillChar = 'e';

        private void findLongestKeyString()
        {
            int maxKeyStringLength = 0;
            foreach (Object item in listBox.Items)
            {
                int keyStringLength = keysConverter.ConvertToString(((ColorWithKey)item).keys).Length;
                if (keyStringLength > maxKeyStringLength)
                    maxKeyStringLength = keyStringLength;
            }

            if (maxKeyStringLength <= 0)
                longestKeyString = String.Empty;
            else
                longestKeyString = new string(keyStringFillChar, maxKeyStringLength);
        }

        private void updateLongestKeyString(Keys key)
        {
            int keyStringLength = keysConverter.ConvertToString(key).Length;
            if (keyStringLength > longestKeyString.Length)
                longestKeyString = new string(keyStringFillChar, keyStringLength);
        }

        private void ColorsDialog_Load(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
            initalizeItems();
            listBox.SetSelected(colorIndex, true);
        }

        private void ColorsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            releaseHotkeyCapture();
            Application.RemoveMessageFilter(this);
        }

        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            BufferedGraphicsContext ctx = BufferedGraphicsManager.Current;
            using (BufferedGraphics bufferedGraphics = ctx.Allocate(e.Graphics, e.Bounds))
            {
                DrawItemEventArgs args = new DrawItemEventArgs(bufferedGraphics.Graphics, e.Font, e.Bounds, e.Index, e.State, e.ForeColor, e.BackColor);

                Graphics g = args.Graphics;
                ColorWithKey color = (ColorWithKey)listBox.Items[args.Index];

                String text = new KeysConverter().ConvertToString(color.keys);
                SizeF textSz = g.MeasureString(longestKeyString, args.Font);
                Rectangle boxRect = new Rectangle(args.Bounds.Left + 6, args.Bounds.Top + 4, (int)(args.Bounds.Width - textSz.Width - 24), args.Bounds.Height - 8);

                if (boxRect.Width < 48)
                    boxRect.Width = 48;

                args.DrawBackground();

                using (SolidBrush brush = new SolidBrush(color.color))
                    g.FillRectangle(brush, boxRect);
                using (Pen pen = new Pen(args.ForeColor, 1))
                    g.DrawRectangle(pen, boxRect);
                using (SolidBrush brush = new SolidBrush(args.ForeColor))
                    g.DrawString(text, args.Font, brush, new PointF(boxRect.Right + 10, args.Bounds.Top + (args.Bounds.Height - textSz.Height) / 2));

                args.DrawFocusRectangle();

                bufferedGraphics.Render();
            }
        }

        private void listBox_Resize(object sender, EventArgs e)
        {
            listBox.Refresh();
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                for (int i = 0; i < listBox.Items.Count; i++)
                    listBox.SetSelected(i, true);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            hotKeyButton.Enabled = selectButton.Enabled = editButton.Enabled = listBox.SelectedIndices.Count == 1;
            removeButton.Enabled = listBox.SelectedIndices.Count > 0;
            moveUpButton.Enabled = !listBox.SelectedIndices.Contains(0);
            moveDownButton.Enabled = !listBox.SelectedIndices.Contains(listBox.Items.Count - 1);

            AcceptButton = selectButton.Enabled ? selectButton : okButton;
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            selectButton_Click(null, null);
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
                return;
            colorIndex = listBox.SelectedIndex;
            okButton_Click(null, null);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == colorDialog.ShowDialog(this))
            {
                listBox.Items.Add(new ColorWithKey(colorDialog.Color));
                updateLongestKeyString(Keys.None);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex < 0)
                return;
            colorDialog.Color = ((ColorWithKey)listBox.Items[listBox.SelectedIndex]).color;
            if (DialogResult.OK == colorDialog.ShowDialog(this))
            {
                ((ColorWithKey)listBox.Items[listBox.SelectedIndex]).color = colorDialog.Color;
                listBox.Refresh();
            }
        }

        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_LBUTTONDBLCLK = 0x0203;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_KEYUP = 0x0100;
        private const int WM_KEYDOWN = 0x0101;
        private const int WM_SYSKEYUP = 0x0103;
        private const int WM_SYSKEYDOWN = 0x0104;

        public bool PreFilterMessage(ref Message m)
        {
            if (captureHotkey)
            {
                switch (m.Msg)
                {
                    case WM_LBUTTONDOWN:
                    case WM_LBUTTONUP:
                    case WM_LBUTTONDBLCLK:
                    case WM_MOUSEMOVE:
                        int x = unchecked((short)(long)m.LParam);
                        int y = unchecked((short)((long)m.LParam >> 16));
                        return !hotKeyButton.ClientRectangle.Contains(x, y);
                }
            }
            return false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (captureHotkey)
            {
                Keys v = keyData;
                v &= ~Keys.Modifiers;
                v &= ~Keys.ControlKey;
                v &= ~Keys.ShiftKey;
                v &= ~Keys.Menu;

                if (v != Keys.None)
                {
                    bool reserved = reservedKeys.Contains(keyData);
                    if (!reserved)
                    {
                        int keyIndex = -1;
                        if (keysToColorIndex.TryGetValue(keyData, out keyIndex) && keyIndex != listBox.SelectedIndex)
                            reserved = true;
                    }

                    if (reserved)
                    {
                        SystemSounds.Exclamation.Play();
                    }
                    else
                    {
                        if (Keys.Escape != keyData && listBox.SelectedIndex >= 0)
                        {
                            ((ColorWithKey)listBox.Items[listBox.SelectedIndex]).keys = keyData;
                            keysToColorIndex[keyData] = listBox.SelectedIndex;
                            findLongestKeyString();
                            listBox.Refresh();
                        }
                        releaseHotkeyCapture();
                    }
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void releaseHotkeyCapture()
        {
            if (captureHotkey)
            {
                hotKeyButton.BackColor = SystemColors.Control;
                hotKeyButton.ForeColor = SystemColors.ControlText;
                Cursor = Cursors.Arrow;
                captureHotkey = false;
            }
        }

        private void hotKeyButton_Click(object sender, EventArgs e)
        {
            if (captureHotkey)
            {
                releaseHotkeyCapture();
            }
            else
            {
                hotKeyButton.BackColor = SystemColors.MenuHighlight;
                hotKeyButton.ForeColor = SystemColors.HighlightText;
                Cursor = Cursors.WaitCursor;
                captureHotkey = true;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndices.Count <= 0)
                return;

            int[] indices = new int[listBox.SelectedIndices.Count];
            listBox.SelectedIndices.CopyTo(indices, 0);
            Array.Sort(indices);
            Array.Reverse(indices);

            foreach (int index in indices)
            {
                Keys keys = ((ColorWithKey)listBox.Items[index]).keys;
                if (Keys.None != keys)
                    keysToColorIndex.Remove(keys);
                listBox.Items.RemoveAt(index);
            }

            findLongestKeyString();
        }

        private void moveUpbutton_Click(object sender, EventArgs e)
        {
            moveListBoxItems(listBox, false);
        }

        private void moveDownButton_Click(object sender, EventArgs e)
        {
            moveListBoxItems(listBox, true);
        }

        private void moveListBoxItems(ListBox listBox, bool down)
        {
            if (listBox.SelectedIndices.Count <= 0 || listBox.SelectedIndices.Contains(down ? listBox.Items.Count - 1 : 0))
                return;
            int[] indices = new int[listBox.SelectedIndices.Count];
            listBox.SelectedIndices.CopyTo(indices, 0);
            Array.Sort(indices);
            if (down)
                Array.Reverse(indices);

            int step = down ? 1 : -1;
            foreach (int index in indices)
            {
                int nextIndex = index + step;

                Keys keys = ((ColorWithKey)listBox.Items[index]).keys;
                if (Keys.None != keys)
                    keysToColorIndex[keys] = nextIndex;
                keys = ((ColorWithKey)listBox.Items[nextIndex]).keys;
                if (Keys.None != keys)
                    keysToColorIndex[keys] = index;

                Object item = listBox.Items[index];
                listBox.Items[index] = listBox.Items[nextIndex];
                listBox.Items[nextIndex] = item;

                listBox.SetSelected(index, false);
                listBox.SetSelected(nextIndex, true);
            }
            listBox.Refresh();
        }

        private void restoreDefaultsButton_Click(object sender, EventArgs e)
        {
            colors = getDefaultColors();
            keysToColorIndex = getDefaultColorKeys();

            listBox.Items.Clear();
            initalizeItems();
            listBox_SelectedIndexChanged(null, null);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            colors.Clear();
            foreach (Object item in listBox.Items)
                colors.Add(((ColorWithKey)item).color);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

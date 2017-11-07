using System.Windows.Forms;

namespace camtracer
{
    partial class ColorsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.restoreDefaultsButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.selectButton = new System.Windows.Forms.Button();
            this.hotKeyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox.BackColor = System.Drawing.SystemColors.Window;
            this.listBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBox.IntegralHeight = false;
            this.listBox.ItemHeight = 32;
            this.listBox.Location = new System.Drawing.Point(12, 10);
            this.listBox.Name = "listBox";
            this.listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox.Size = new System.Drawing.Size(149, 339);
            this.listBox.TabIndex = 11;
            this.listBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItem);
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            this.listBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox_KeyDown);
            this.listBox.Resize += new System.EventHandler(this.listBox_Resize);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(176, 50);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(96, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "&Add...";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // editButton
            // 
            this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(176, 79);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(96, 23);
            this.editButton.TabIndex = 3;
            this.editButton.Text = "&Edit...";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(176, 137);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(96, 23);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "&Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveUpButton.Enabled = false;
            this.moveUpButton.Location = new System.Drawing.Point(176, 176);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(96, 23);
            this.moveUpButton.TabIndex = 6;
            this.moveUpButton.Text = "Move &Up";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.moveUpbutton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDownButton.Enabled = false;
            this.moveDownButton.Location = new System.Drawing.Point(176, 205);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(96, 23);
            this.moveDownButton.TabIndex = 7;
            this.moveDownButton.Text = "Move &Down";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
            // 
            // restoreDefaultsButton
            // 
            this.restoreDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.restoreDefaultsButton.Location = new System.Drawing.Point(176, 245);
            this.restoreDefaultsButton.Name = "restoreDefaultsButton";
            this.restoreDefaultsButton.Size = new System.Drawing.Size(96, 23);
            this.restoreDefaultsButton.TabIndex = 8;
            this.restoreDefaultsButton.Text = "Res&tore Defaults";
            this.restoreDefaultsButton.UseVisualStyleBackColor = true;
            this.restoreDefaultsButton.Click += new System.EventHandler(this.restoreDefaultsButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(176, 330);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(96, 23);
            this.cancelButton.TabIndex = 10;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(176, 301);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(96, 23);
            this.okButton.TabIndex = 9;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            this.colorDialog.Color = System.Drawing.Color.Lime;
            this.colorDialog.FullOpen = true;
            // 
            // selectButton
            // 
            this.selectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectButton.Enabled = false;
            this.selectButton.Location = new System.Drawing.Point(176, 12);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(96, 23);
            this.selectButton.TabIndex = 1;
            this.selectButton.Text = "&Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // hotKeyButton
            // 
            this.hotKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeyButton.Enabled = false;
            this.hotKeyButton.Location = new System.Drawing.Point(176, 108);
            this.hotKeyButton.Name = "hotKeyButton";
            this.hotKeyButton.Size = new System.Drawing.Size(96, 23);
            this.hotKeyButton.TabIndex = 4;
            this.hotKeyButton.Text = "Assign &Hotkey";
            this.hotKeyButton.UseVisualStyleBackColor = true;
            this.hotKeyButton.Click += new System.EventHandler(this.hotKeyButton_Click);
            // 
            // ColorsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.hotKeyButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.restoreDefaultsButton);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.listBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 384);
            this.Name = "ColorsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Colors";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ColorsDialog_FormClosed);
            this.Load += new System.EventHandler(this.ColorsDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveDownButton;
        private System.Windows.Forms.Button restoreDefaultsButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private ColorDialog colorDialog;
        private Button selectButton;
        private Button hotKeyButton;
    }
}
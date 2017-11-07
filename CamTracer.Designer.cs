namespace camtracer
{
    partial class CamTracer
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label4;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamTracer));
            this.changeCameraBtn = new System.Windows.Forms.Button();
            this.startStopBtn = new System.Windows.Forms.Button();
            this.resetBtn = new System.Windows.Forms.Button();
            this.saveResultBtn = new System.Windows.Forms.Button();
            this.previewBtn = new System.Windows.Forms.Button();
            this.methodBox = new System.Windows.Forms.ComboBox();
            this.thresholdField = new System.Windows.Forms.NumericUpDown();
            this.thresholdBar = new System.Windows.Forms.TrackBar();
            this.alphaBar = new System.Windows.Forms.TrackBar();
            this.alphaField = new System.Windows.Forms.NumericUpDown();
            this.fpsLabel = new System.Windows.Forms.Label();
            this.colorsButton = new System.Windows.Forms.Button();
            this.nextColorButton = new System.Windows.Forms.Button();
            this.colorAlphaBar = new System.Windows.Forms.TrackBar();
            this.colorAlphaField = new System.Windows.Forms.NumericUpDown();
            this.pictureBox = new System.Windows.Forms.Panel();
            this.panelCurrentColor = new System.Windows.Forms.Panel();
            this.panelNextColor = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorAlphaBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorAlphaField)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(562, 265);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 13);
            label1.TabIndex = 6;
            label1.Text = "&Threshold:";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(565, 330);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(46, 13);
            label2.TabIndex = 9;
            label2.Text = "&Opacity:";
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(565, 222);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(46, 13);
            label3.TabIndex = 4;
            label3.Text = "&Method:";
            // 
            // groupBox1
            // 
            groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox1.Location = new System.Drawing.Point(565, 167);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(107, 10);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Location = new System.Drawing.Point(565, 41);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(107, 10);
            groupBox2.TabIndex = 16;
            groupBox2.TabStop = false;
            // 
            // label4
            // 
            label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(562, 471);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(73, 13);
            label4.TabIndex = 14;
            label4.Text = "Color Opacit&y:";
            // 
            // changeCameraBtn
            // 
            this.changeCameraBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeCameraBtn.Location = new System.Drawing.Point(565, 12);
            this.changeCameraBtn.Name = "changeCameraBtn";
            this.changeCameraBtn.Size = new System.Drawing.Size(107, 23);
            this.changeCameraBtn.TabIndex = 17;
            this.changeCameraBtn.Text = "&Change Camera...";
            this.changeCameraBtn.UseVisualStyleBackColor = true;
            this.changeCameraBtn.Click += new System.EventHandler(this.changeCameraBtn_Click);
            // 
            // startStopBtn
            // 
            this.startStopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startStopBtn.Location = new System.Drawing.Point(565, 89);
            this.startStopBtn.Name = "startStopBtn";
            this.startStopBtn.Size = new System.Drawing.Size(107, 43);
            this.startStopBtn.TabIndex = 1;
            this.startStopBtn.Text = "&Start";
            this.startStopBtn.UseVisualStyleBackColor = true;
            this.startStopBtn.Click += new System.EventHandler(this.startStopBtn_Click);
            // 
            // resetBtn
            // 
            this.resetBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetBtn.Location = new System.Drawing.Point(565, 138);
            this.resetBtn.Name = "resetBtn";
            this.resetBtn.Size = new System.Drawing.Size(107, 23);
            this.resetBtn.TabIndex = 2;
            this.resetBtn.Text = "&Reset";
            this.resetBtn.UseVisualStyleBackColor = true;
            this.resetBtn.Click += new System.EventHandler(this.resetBtn_Click);
            // 
            // saveResultBtn
            // 
            this.saveResultBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveResultBtn.Enabled = false;
            this.saveResultBtn.Location = new System.Drawing.Point(565, 186);
            this.saveResultBtn.Name = "saveResultBtn";
            this.saveResultBtn.Size = new System.Drawing.Size(107, 23);
            this.saveResultBtn.TabIndex = 3;
            this.saveResultBtn.Text = "Sa&ve Result...";
            this.saveResultBtn.UseVisualStyleBackColor = true;
            this.saveResultBtn.Click += new System.EventHandler(this.saveResultBtn_Click);
            // 
            // previewBtn
            // 
            this.previewBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.previewBtn.Enabled = false;
            this.previewBtn.Location = new System.Drawing.Point(565, 60);
            this.previewBtn.Name = "previewBtn";
            this.previewBtn.Size = new System.Drawing.Size(107, 23);
            this.previewBtn.TabIndex = 18;
            this.previewBtn.Text = "Pr&eview";
            this.previewBtn.UseVisualStyleBackColor = true;
            this.previewBtn.Click += new System.EventHandler(this.previewBtn_Click);
            // 
            // methodBox
            // 
            this.methodBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.methodBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.methodBox.FormattingEnabled = true;
            this.methodBox.Items.AddRange(new object[] {
            "Light On Dark",
            "Dark On Light"});
            this.methodBox.Location = new System.Drawing.Point(565, 238);
            this.methodBox.Name = "methodBox";
            this.methodBox.Size = new System.Drawing.Size(107, 21);
            this.methodBox.TabIndex = 5;
            this.methodBox.SelectedIndexChanged += new System.EventHandler(this.methodBox_SelectedIndexChanged);
            // 
            // thresholdField
            // 
            this.thresholdField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdField.DecimalPlaces = 2;
            this.thresholdField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.thresholdField.Location = new System.Drawing.Point(568, 301);
            this.thresholdField.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.thresholdField.Name = "thresholdField";
            this.thresholdField.Size = new System.Drawing.Size(107, 20);
            this.thresholdField.TabIndex = 8;
            this.thresholdField.ValueChanged += new System.EventHandler(this.thresholdField_ValueChanged);
            // 
            // thresholdBar
            // 
            this.thresholdBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thresholdBar.AutoSize = false;
            this.thresholdBar.LargeChange = 128;
            this.thresholdBar.Location = new System.Drawing.Point(565, 281);
            this.thresholdBar.Maximum = 255;
            this.thresholdBar.Name = "thresholdBar";
            this.thresholdBar.Size = new System.Drawing.Size(107, 20);
            this.thresholdBar.TabIndex = 7;
            this.thresholdBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.thresholdBar.ValueChanged += new System.EventHandler(this.thresholdBar_ValueChanged);
            // 
            // alphaBar
            // 
            this.alphaBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.alphaBar.AutoSize = false;
            this.alphaBar.LargeChange = 128;
            this.alphaBar.Location = new System.Drawing.Point(565, 346);
            this.alphaBar.Maximum = 255;
            this.alphaBar.Name = "alphaBar";
            this.alphaBar.Size = new System.Drawing.Size(107, 20);
            this.alphaBar.TabIndex = 10;
            this.alphaBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.alphaBar.ValueChanged += new System.EventHandler(this.alphaBar_ValueChanged);
            // 
            // alphaField
            // 
            this.alphaField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.alphaField.DecimalPlaces = 2;
            this.alphaField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.alphaField.Location = new System.Drawing.Point(565, 366);
            this.alphaField.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.alphaField.Name = "alphaField";
            this.alphaField.Size = new System.Drawing.Size(107, 20);
            this.alphaField.TabIndex = 11;
            this.alphaField.ValueChanged += new System.EventHandler(this.alphaField_ValueChanged);
            // 
            // fpsLabel
            // 
            this.fpsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Location = new System.Drawing.Point(562, 533);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(0, 13);
            this.fpsLabel.TabIndex = 22;
            // 
            // colorsButton
            // 
            this.colorsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorsButton.Location = new System.Drawing.Point(565, 401);
            this.colorsButton.Name = "colorsButton";
            this.colorsButton.Size = new System.Drawing.Size(107, 23);
            this.colorsButton.TabIndex = 12;
            this.colorsButton.Text = "Co&lors...";
            this.colorsButton.UseVisualStyleBackColor = true;
            this.colorsButton.Click += new System.EventHandler(this.colorsButton_Click);
            // 
            // nextColorButton
            // 
            this.nextColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nextColorButton.Location = new System.Drawing.Point(565, 430);
            this.nextColorButton.Name = "nextColorButton";
            this.nextColorButton.Size = new System.Drawing.Size(107, 23);
            this.nextColorButton.TabIndex = 13;
            this.nextColorButton.Text = "&Next Color";
            this.nextColorButton.UseVisualStyleBackColor = true;
            this.nextColorButton.Click += new System.EventHandler(this.nextColorButton_Click);
            // 
            // colorAlphaBar
            // 
            this.colorAlphaBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorAlphaBar.AutoSize = false;
            this.colorAlphaBar.LargeChange = 128;
            this.colorAlphaBar.Location = new System.Drawing.Point(562, 487);
            this.colorAlphaBar.Maximum = 255;
            this.colorAlphaBar.Name = "colorAlphaBar";
            this.colorAlphaBar.Size = new System.Drawing.Size(107, 20);
            this.colorAlphaBar.TabIndex = 15;
            this.colorAlphaBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.colorAlphaBar.ValueChanged += new System.EventHandler(this.colorAlphaBar_ValueChanged);
            // 
            // colorAlphaField
            // 
            this.colorAlphaField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorAlphaField.DecimalPlaces = 2;
            this.colorAlphaField.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.colorAlphaField.Location = new System.Drawing.Point(562, 507);
            this.colorAlphaField.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.colorAlphaField.Name = "colorAlphaField";
            this.colorAlphaField.Size = new System.Drawing.Size(107, 20);
            this.colorAlphaField.TabIndex = 16;
            this.colorAlphaField.ValueChanged += new System.EventHandler(this.colorAlphaField_ValueChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(548, 546);
            this.pictureBox.TabIndex = 23;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
            this.pictureBox.Resize += new System.EventHandler(this.pictureBox_Resize);
            // 
            // panelCurrentColor
            // 
            this.panelCurrentColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCurrentColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCurrentColor.Location = new System.Drawing.Point(565, 456);
            this.panelCurrentColor.Name = "panelCurrentColor";
            this.panelCurrentColor.Size = new System.Drawing.Size(50, 10);
            this.panelCurrentColor.TabIndex = 24;
            // 
            // panelNextColor
            // 
            this.panelNextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelNextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNextColor.Location = new System.Drawing.Point(621, 456);
            this.panelNextColor.Name = "panelNextColor";
            this.panelNextColor.Size = new System.Drawing.Size(50, 10);
            this.panelNextColor.TabIndex = 25;
            // 
            // CamTracer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(684, 546);
            this.Controls.Add(this.panelNextColor);
            this.Controls.Add(this.panelCurrentColor);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.colorAlphaBar);
            this.Controls.Add(label4);
            this.Controls.Add(this.colorAlphaField);
            this.Controls.Add(this.nextColorButton);
            this.Controls.Add(this.colorsButton);
            this.Controls.Add(this.fpsLabel);
            this.Controls.Add(groupBox2);
            this.Controls.Add(groupBox1);
            this.Controls.Add(label3);
            this.Controls.Add(this.alphaBar);
            this.Controls.Add(label2);
            this.Controls.Add(this.alphaField);
            this.Controls.Add(this.thresholdBar);
            this.Controls.Add(label1);
            this.Controls.Add(this.thresholdField);
            this.Controls.Add(this.methodBox);
            this.Controls.Add(this.previewBtn);
            this.Controls.Add(this.saveResultBtn);
            this.Controls.Add(this.resetBtn);
            this.Controls.Add(this.startStopBtn);
            this.Controls.Add(this.changeCameraBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(400, 585);
            this.Name = "CamTracer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CamTracer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CamTracer_FormClosing);
            this.Load += new System.EventHandler(this.CamTracer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.thresholdField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alphaField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorAlphaBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorAlphaField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button changeCameraBtn;
        private System.Windows.Forms.Button startStopBtn;
        private System.Windows.Forms.Button resetBtn;
        private System.Windows.Forms.Button saveResultBtn;
        private System.Windows.Forms.Button previewBtn;
        private System.Windows.Forms.ComboBox methodBox;
        private System.Windows.Forms.NumericUpDown thresholdField;
        private System.Windows.Forms.TrackBar thresholdBar;
        private System.Windows.Forms.TrackBar alphaBar;
        private System.Windows.Forms.NumericUpDown alphaField;
        private System.Windows.Forms.Label fpsLabel;
        private System.Windows.Forms.Button colorsButton;
        private System.Windows.Forms.Button nextColorButton;
        private System.Windows.Forms.TrackBar colorAlphaBar;
        private System.Windows.Forms.NumericUpDown colorAlphaField;
        private System.Windows.Forms.Panel pictureBox;
        private System.Windows.Forms.Panel panelCurrentColor;
        private System.Windows.Forms.Panel panelNextColor;
    }
}


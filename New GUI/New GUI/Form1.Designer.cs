namespace New_GUI
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.InputBox = new System.Windows.Forms.TextBox();
            this.DestinationBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.MinimizeWindow = new System.Windows.Forms.PictureBox();
            this.CloseWindow = new System.Windows.Forms.PictureBox();
            this.Script3rTitle = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Script3rTitle)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // InputBox
            // 
            this.InputBox.AllowDrop = true;
            this.InputBox.Location = new System.Drawing.Point(104, 114);
            this.InputBox.Margin = new System.Windows.Forms.Padding(2);
            this.InputBox.Multiline = true;
            this.InputBox.Name = "InputBox";
            this.InputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InputBox.Size = new System.Drawing.Size(522, 193);
            this.InputBox.TabIndex = 0;
            this.InputBox.Click += new System.EventHandler(this.textBox2_Click);
            this.InputBox.TextChanged += new System.EventHandler(this.InputBox_TextChanged);
            // 
            // DestinationBox
            // 
            this.DestinationBox.Location = new System.Drawing.Point(245, 327);
            this.DestinationBox.Margin = new System.Windows.Forms.Padding(2);
            this.DestinationBox.Name = "DestinationBox";
            this.DestinationBox.Size = new System.Drawing.Size(381, 20);
            this.DestinationBox.TabIndex = 1;
            this.DestinationBox.Click += new System.EventHandler(this.DestinationBox_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 316);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select Destination Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(488, 367);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 36);
            this.button2.TabIndex = 3;
            this.button2.Text = "Label Footage!";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(104, 367);
            this.clear.Margin = new System.Windows.Forms.Padding(2);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(75, 36);
            this.clear.TabIndex = 4;
            this.clear.Text = "Clear Clip Selection";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click_1);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            this.TopPanel.Controls.Add(this.MinimizeWindow);
            this.TopPanel.Controls.Add(this.CloseWindow);
            this.TopPanel.Controls.Add(this.Script3rTitle);
            this.TopPanel.Controls.Add(this.panel3);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(647, 74);
            this.TopPanel.TabIndex = 7;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // MinimizeWindow
            // 
            this.MinimizeWindow.Image = global::New_GUI.Properties.Resources.Icon_Minimize;
            this.MinimizeWindow.Location = new System.Drawing.Point(595, 7);
            this.MinimizeWindow.Name = "MinimizeWindow";
            this.MinimizeWindow.Size = new System.Drawing.Size(19, 22);
            this.MinimizeWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.MinimizeWindow.TabIndex = 4;
            this.MinimizeWindow.TabStop = false;
            this.MinimizeWindow.Click += new System.EventHandler(this.MinimizeWindow_Click);
            // 
            // CloseWindow
            // 
            this.CloseWindow.Image = global::New_GUI.Properties.Resources.Icon_Close;
            this.CloseWindow.Location = new System.Drawing.Point(618, 7);
            this.CloseWindow.Name = "CloseWindow";
            this.CloseWindow.Size = new System.Drawing.Size(20, 22);
            this.CloseWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.CloseWindow.TabIndex = 3;
            this.CloseWindow.TabStop = false;
            this.CloseWindow.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // Script3rTitle
            // 
            this.Script3rTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Script3rTitle.Image = global::New_GUI.Properties.Resources.eDIT;
            this.Script3rTitle.Location = new System.Drawing.Point(76, 0);
            this.Script3rTitle.Name = "Script3rTitle";
            this.Script3rTitle.Size = new System.Drawing.Size(379, 75);
            this.Script3rTitle.TabIndex = 2;
            this.Script3rTitle.TabStop = false;
            this.Script3rTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Script3rTitle_MouseDown);
            this.Script3rTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Script3rTitle_MouseMove);
            this.Script3rTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Script3rTitle_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(165)))), ((int)(((byte)(245)))));
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(75, 74);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pictureBox1.Image = global::New_GUI.Properties.Resources.Logo_256;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 74);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftPanel.Location = new System.Drawing.Point(0, 74);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(75, 344);
            this.LeftPanel.TabIndex = 8;
            this.LeftPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseDown);
            this.LeftPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseMove);
            this.LeftPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 90);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(278, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Click below to add footage (or drag files onto the text box)";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(647, 418);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DestinationBox);
            this.Controls.Add(this.InputBox);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainPage";
            this.Text = "Script3r - The Automatic Footage Labeler!";
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.TopPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Script3rTitle)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.TextBox DestinationBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Script3rTitle;
        private System.Windows.Forms.PictureBox MinimizeWindow;
        private System.Windows.Forms.PictureBox CloseWindow;
    }
}


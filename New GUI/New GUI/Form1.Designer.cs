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
            this.InputBox = new System.Windows.Forms.ListView();
            this.file_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clear_selected = new System.Windows.Forms.Button();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Script3rTitle)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DestinationBox
            // 
            this.DestinationBox.Location = new System.Drawing.Point(368, 503);
            this.DestinationBox.Name = "DestinationBox";
            this.DestinationBox.Size = new System.Drawing.Size(570, 26);
            this.DestinationBox.TabIndex = 1;
            this.DestinationBox.Click += new System.EventHandler(this.DestinationBox_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(156, 486);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(190, 73);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select Destination Folder";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(731, 571);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 55);
            this.button2.TabIndex = 3;
            this.button2.Text = "Label Footage!";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // clear
            // 
            this.clear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear.Location = new System.Drawing.Point(156, 571);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(112, 67);
            this.clear.TabIndex = 4;
            this.clear.Text = "Clear Clip Selection";
            this.clear.UseVisualStyleBackColor = false;
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
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(970, 114);
            this.TopPanel.TabIndex = 7;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseMove);
            this.TopPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseUp);
            // 
            // MinimizeWindow
            // 
            this.MinimizeWindow.Image = global::New_GUI.Properties.Resources.Icon_Minimize;
            this.MinimizeWindow.Location = new System.Drawing.Point(892, 11);
            this.MinimizeWindow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeWindow.Name = "MinimizeWindow";
            this.MinimizeWindow.Size = new System.Drawing.Size(28, 34);
            this.MinimizeWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.MinimizeWindow.TabIndex = 4;
            this.MinimizeWindow.TabStop = false;
            this.MinimizeWindow.Click += new System.EventHandler(this.MinimizeWindow_Click);
            // 
            // CloseWindow
            // 
            this.CloseWindow.Image = global::New_GUI.Properties.Resources.Icon_Close;
            this.CloseWindow.Location = new System.Drawing.Point(927, 11);
            this.CloseWindow.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CloseWindow.Name = "CloseWindow";
            this.CloseWindow.Size = new System.Drawing.Size(30, 34);
            this.CloseWindow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.CloseWindow.TabIndex = 3;
            this.CloseWindow.TabStop = false;
            this.CloseWindow.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // Script3rTitle
            // 
            this.Script3rTitle.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Script3rTitle.Image = global::New_GUI.Properties.Resources.eDIT;
            this.Script3rTitle.Location = new System.Drawing.Point(114, 0);
            this.Script3rTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Script3rTitle.Name = "Script3rTitle";
            this.Script3rTitle.Size = new System.Drawing.Size(568, 115);
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
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(112, 114);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pictureBox1.Image = global::New_GUI.Properties.Resources.Logo_256;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(111, 114);
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
            this.LeftPanel.Location = new System.Drawing.Point(0, 114);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(112, 546);
            this.LeftPanel.TabIndex = 8;
            this.LeftPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseDown);
            this.LeftPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseMove);
            this.LeftPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LeftPanel_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 138);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(413, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Click below to add footage (or drag files onto the text box)";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // InputBox
            // 
            this.InputBox.AllowDrop = true;
            this.InputBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.file_name});
            this.InputBox.GridLines = true;
            this.InputBox.Location = new System.Drawing.Point(156, 175);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(782, 295);
            this.InputBox.TabIndex = 10;
            this.InputBox.UseCompatibleStateImageBehavior = false;
            this.InputBox.View = System.Windows.Forms.View.Details;
            this.InputBox.SelectedIndexChanged += new System.EventHandler(this.InputBox2_SelectedIndexChanged);
            // 
            // file_name
            // 
            this.file_name.Text = "File Name";
            this.file_name.Width = 396;
            // 
            // clear_selected
            // 
            this.clear_selected.BackColor = System.Drawing.Color.WhiteSmoke;
            this.clear_selected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clear_selected.Location = new System.Drawing.Point(274, 571);
            this.clear_selected.Name = "clear_selected";
            this.clear_selected.Size = new System.Drawing.Size(121, 67);
            this.clear_selected.TabIndex = 11;
            this.clear_selected.Text = "Clear Selected Items";
            this.clear_selected.UseVisualStyleBackColor = false;
            this.clear_selected.Click += new System.EventHandler(this.clear_selected_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(970, 660);
            this.Controls.Add(this.clear_selected);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DestinationBox);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.ListView InputBox;
        private System.Windows.Forms.ColumnHeader file_name;
        private System.Windows.Forms.Button clear_selected;
    }
}


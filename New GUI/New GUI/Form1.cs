﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Script3rLibrary;
using Script3rSpeech;



namespace New_GUI
{
    public partial class MainPage : Form
    {
        private FolderBrowserDialog fbd;
        private List<string> files_to_move;
        private Dictionary<string, string[]> source_file_dict;
        private string destination;
        private Settings settings;
        private SpeechRecognizer recognizer;
        private bool push = false;
        int TogMove, MValX, MValY;

        public MainPage()
        {
            InitializeComponent();
            this.AllowDrop = true;
            InputBox.DragEnter += new DragEventHandler(textBox_DragEnter);
            InputBox.DragDrop += new DragEventHandler(textBox_DragDrop);
            this.fbd = new FolderBrowserDialog();
            this.files_to_move = new List<string> { };
            this.source_file_dict = new Dictionary<string, string[]> { };
            this.destination = "";
            this.settings = new Settings();
            this.recognizer = new SpeechRecognizer(settings);
        }



        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        void textBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && !push) e.Effect = DragDropEffects.Copy;
        }

        void textBox_DragDrop(object sender, DragEventArgs e)
        {
            if (push) return;
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            filesAdded = 0;
            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    // This path is a file
                    ProcessFile(path);
                }
                else if (Directory.Exists(path))
                {
                    // This path is a directory
                    ProcessDirectory(path);
                }
            }
            if (filesAdded == 0) {
                MessageBox.Show("Unfortunately, none of the files you dragged and dropped were .mov or .wav files!");
            }
        }

        private void MainPage_Load(object sender, EventArgs e) {
            // Load in the previous output directory from settings.txt (if it exists)
            string directoryName = settings.GetOutputDirectory();
            DestinationBox.Text = directoryName;
            this.destination = directoryName;
        }

        int filesAdded = 0;

        // Process all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        // Insert logic for processing found files here.
        void ProcessFile(string path)
        {
            // Check path ending.
            int dot = path.LastIndexOf('.');
            if (dot == -1) return;
            string extension = path.Substring(dot+1).ToLower();
            string[] supportedExtensions = {
                "wav","mov","mp4"
            };
            //TODO (neil): Fill this out
            if (Array.IndexOf(supportedExtensions, extension) == -1) return;
            
            //InputBox2.Text += path + "\r\n";
            var item = new ListViewItem(new[] { path, "" });
            this.InputBox.Items.Add(item);
            files_to_move.Add(path);
            filesAdded++;
        }
        
        private void clear_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            clear.Enabled = false;
            var confirmResult = MessageBox.Show("Are you sure you want to clear your selection?",
                                     "Confirm Clear!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                // If 'Yes', do something here.
                InputBox.Clear();
                InputBox.Columns.Add("File Name", -2, HorizontalAlignment.Left);
                InputBox.Columns.Add("Status", -2, HorizontalAlignment.Left); //TODO(neil): Check this
                files_to_move.Clear();
                source_file_dict.Clear();
            }
            button1.Enabled = true;
            button2.Enabled = true;
            clear.Enabled = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            clear.Enabled = false;
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DestinationBox.Text = fbd.SelectedPath;
                this.destination = fbd.SelectedPath;
            }
            button1.Enabled = true;
            button2.Enabled = true;
            clear.Enabled = true;
        }

        // ASSUMES: x and y are not both empty.
        private static int SortByFirstElement(string[] x, string[] y) {
            return x[0].CompareTo(y[0]);
        }

        private void FillMissingInformation() {
            if (source_file_dict.Count == 0) return;
            // Attempt to fill in any incomplete information on files.
            // We can assume a few things, within any directory of videos:
            // (1) Videos with contiguous names were shot contiguously,
            // and sorting them like 1, 2, 3... will place them in the order
            // they were shot.
            // (2) The 2nd AC does not skip takes.
            // So, we can fill in the following information (for a file C in between
            // files A and B)
            // - If A and B have the same scene number, C's scene number will be the
            // same as those.
            // - If A and B have the same subscene number, C's scene number will be the
            // same as those.
            // - If A and B are n shots apart and their take numbers differ by n and their scenes match, then
            //   each shot in between A and B contains exactly one take.

            // So, we need to (1) have logic to process each directory of videos individually,
            // and (2) sort and fill in the missing information for the videos in order for each directory.
            // The "if their scenes match" bit means we might have to process previous files multiple times -
            // but realistically, we can assume the number of files is small.

            // By default, we start at scene 1A, take 0, so that incomplete files at the start match up.
            // First, let's create a temporary structure consisting of a list of all of the files.
            // We'll then iterate through it using a stack.

            List<string[]> fileList = new List<string[]>();
            foreach (KeyValuePair<string, string[]> kvp in source_file_dict) {
                if (kvp.Value.Length != 3) {
                    throw new Exception("Development team error: Length of labels must be 3.");
                }
                fileList.Add(new String[4] {
                    kvp.Key,kvp.Value[0],kvp.Value[1],kvp.Value[2]
                });
            }

            fileList.Sort(SortByFirstElement);

            //state machine, whee!
            string currentDirectory = Path.GetDirectoryName(fileList[0][0]);
            int prevScene = 1;
            string prevLetter = "A";
            int prevTake = 0;
            int expectedTake = 1;

            //TODO(neil): This code doesn't look nice.
            for (int field = 1; field < 4; field++) { //match scenes, then letters, then takes
                prevScene = 1;
                prevLetter = "A";
                prevTake = 0;
                expectedTake = 1; //TODO: turn this into a state object that gets passed Muratori-style
                for (int i = 0; i < fileList.Count; i++) {
                    // Have we switched to a new directory?
                    string fileDirectory = Path.GetDirectoryName(fileList[i][0]);
                    if (fileDirectory != currentDirectory) {
                        prevScene = 1;
                        prevLetter = "A";
                        prevTake = 0;
                        expectedTake = 1;
                    }

                    //For each piece of information, see if we can use it to fill in previous information.
                    switch (field) {
                        case 1:
                            //Scenes
                            if (fileList[i][1] != "match_fail") {
                                int myScene = int.Parse(fileList[i][1]);
                                if (myScene == prevScene) {
                                    //Fill in previous scenes
                                    int j = i - 1;
                                    while (j >= 0) {
                                        if (fileList[j][1] != "match_fail") break;
                                        fileList[j][1] = fileList[i][1];
                                        j--;
                                    }
                                }
                                prevScene = myScene;
                            }
                            break;
                        case 2:
                            if (fileList[i][2] != "match_fail" && fileList[i][1]!="match_fail") {
                                string myLetter = fileList[i][2];
                                if (prevLetter == myLetter) {
                                    // Fill in previous letters
                                    // NOTE: Do we need to check prevScene=myScene?
                                    int j = i - 1;
                                    while (j >= 0) {
                                        if (!(fileList[j][2] == "match_fail" || fileList[j][2] == myLetter)) break;
                                        fileList[j][2] = myLetter;
                                        j--;
                                    }
                                }
                            }
                            if (fileList[i][2] != "match_fail") {
                                prevLetter = fileList[i][2];
                            }
                            break;
                        case 3:
                            if(fileList[i][1]!="match_fail" && fileList[i][2]!="match_fail" && fileList[i][3] != "match_fail") {
                                int myTake = int.Parse(fileList[i][3]);
                                if (myTake == expectedTake) {
                                    //AWESOME.
                                    for(int j=1;j<=i && j<=myTake-prevTake; j++) {
                                        fileList[i - j][3] = (myTake - j).ToString();
                                    }
                                }
                            }
                            if (fileList[i][3] != "match_fail") {
                                prevTake = int.Parse(fileList[i][3]);
                                expectedTake = prevTake;
                            }
                            break;
                    }

                    expectedTake++;
                }
            }

            //Cool. Fill in the additional information in the dictionary.
            for (int i = 0; i < fileList.Count; i++) {
                string key = fileList[i][0];
                source_file_dict[key][0] = fileList[i][1];
                source_file_dict[key][1] = fileList[i][2];
                source_file_dict[key][2] = fileList[i][3];
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = false;
            clear.Enabled = false;
            clear_selected.Enabled = false;

            push = true;
            settings.SetOutputDirectoryName(DestinationBox.Text);
            backgroundWorker1.RunWorkerAsync();
        }

        // Main processing stage, run from the backgroundWorker.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            foreach (string path in files_to_move) {
                UpdateItemStatus(path, "Generating audio...");
                string exPath = Application.StartupPath + "\\";

                string outPath = exPath + "temporary.wav";
                int x = 0;
                while (File.Exists(outPath)) {
                    x++;
                    outPath = exPath + "temporary" + x + ".wav";
                }

                string cmdText = "/c ffmpeg -ss 0 -i " + "\"" + path + "\"" + " -t 30 -acodec pcm_s16le -ac 1 -ar 16000 " + "\"" + outPath + "\"";
                string pee = cmdText;
                Process pro = Process.Start("CMD.exe", cmdText);
                pro.WaitForExit();
                UpdateItemStatus(path, "Initializing speech engine.");

                string transcribed = String.Join(" ", this.recognizer.RecognizeSpeech(outPath, path, this))+" ";

                UpdateItemStatus(path, "Parsed transcript; Removing temporary audio file.");

                if (File.Exists(outPath)) {
                    File.Delete(outPath);
                }

                UpdateItemStatus(path, "Parsing transcript: "+transcribed);

                source_file_dict.Add(path, text_to_take.SearchStr(transcribed));

                UpdateItemStatus(path, "Ready to move: Scene " + source_file_dict[path][0] + source_file_dict[path][1]+
                    " take " + source_file_dict[path][2]);
            }

            //TODO (neil): Export files as soon as we have all three parts of their information.
            FillMissingInformation();

            // This has just been changed to possibly append numbers to the end - please check it.
            file_move.move_and_org(files_to_move, source_file_dict, destination);
        }

        // From https://msdn.microsoft.com/en-us/library/ms171728(v=vs.110).aspx
        delegate void StringArgReturningVoidDelegate(string text);

        //This is tricky.
        private void AppendTextAsync(string text) {
            // InvokeRequired required compares the thread ID of the  
            // calling thread to the thread ID of the creating thread.  
            // If these threads are different, it returns true.  
            if (this.InputBox.InvokeRequired) {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(AppendTextAsync);
                this.Invoke(d, new object[] { text });
            } else {
                this.InputBox.Text += text;
            }
        }

        delegate void TwoStringArgReturningVoidDelegate(string originalFilename, string text);

        public void UpdateItemStatus(string originalFilename, string text) {
            if (this.InputBox.InvokeRequired) {
                TwoStringArgReturningVoidDelegate d = new TwoStringArgReturningVoidDelegate(UpdateItemStatus);
                this.Invoke(d, new object[] { originalFilename, text });
            } else {
                int foundIndex = -1;
                for(int i = 0; i < InputBox.Items.Count; i++) {
                    if (this.InputBox.Items[i].SubItems[0].Text.Equals(originalFilename)) {
                        foundIndex = i;
                    }
                }
                if (foundIndex == -1) return;
                this.InputBox.Items[foundIndex].SubItems[1].Text = text;
            }
        
        }

        // Callback to the main form.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) {
                MessageBox.Show("The background thread ran into an error!\n" +
                    "Here it is, for the developers: \n" +
                    e.Error.ToString());
                return;
            }

            // Re-enable buttons
            button2.Enabled = true;
            button1.Enabled = true;
            clear.Enabled = true;
            clear_selected.Enabled = true;
            push = false;
            files_to_move.Clear();
            source_file_dict.Clear();
            try {
                // Open the output Windows Explorer directory, showing all of the files in their properly sorted order.
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = "file:\\\\" + destination;
                p.Start();
            }catch(Exception ex) {
                MessageBox.Show("Tried to open explorer.exe at " + destination + ", but was unable to.\n" +
                    "Here's the full error (for developers): " + ex.ToString());
            }

            //TODO: Re-enable checkboxes and text fields so that we can add more files and keep on going,
            // or close the application.
            //CloseApplication();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            if (!push) { // don't handle this if currently processing
                OpenFileDialog x = new OpenFileDialog();
                x.Multiselect = true;
                x.ShowDialog();
                string[] result = x.FileNames;

                foreach (String file in result) {
                    ProcessFile(file);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DestinationBox_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DestinationBox.Text = fbd.SelectedPath;
                this.destination = fbd.SelectedPath;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }

        private void MinimizeWindow_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MValX = e.X; MValY = e.Y; TogMove = 1;
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1) {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void Script3rTitle_MouseDown(object sender, MouseEventArgs e)
        {
            MValX = e.X; MValY = e.Y; TogMove = 1;
        }

        private void Script3rTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX - 74, MousePosition.Y - MValY);
            }
        }

        private void Script3rTitle_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void LeftPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MValX = e.X; MValY = e.Y; TogMove = 1;
        }

        private void LeftPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY -74);
            }
        }

        private void LeftPanel_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void InputBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void clear_selected_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem v in InputBox.SelectedItems)
            {
                InputBox.Items.Remove(v);
                if (files_to_move.IndexOf(v.SubItems[0].Text) != -1)
                {
                    files_to_move.RemoveAt(files_to_move.IndexOf(v.SubItems[0].Text));
                }
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void CloseApplication() {
            // Dispose objects, shut down the application
            recognizer.Dispose();
            // Application.Exit();
            Environment.Exit(0);
        }
    }
}

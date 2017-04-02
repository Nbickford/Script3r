﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Script3rLibrary;
using Script3rSpeech;

namespace New_GUI
{
    public partial class Form1 : Form
    {
        private FolderBrowserDialog fbd;
        private List<string> files_to_move;
        private Dictionary<string, string[]> source_file_dict;
        private string destination;
        private SpeechRecognizer recognizer;

        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            textBox2.DragEnter += new DragEventHandler(textBox_DragEnter);
            textBox2.DragDrop += new DragEventHandler(textBox_DragDrop);
            this.fbd = new FolderBrowserDialog();
            this.files_to_move = new List<string> { };
            this.source_file_dict = new Dictionary<string, string[]> { };
            this.destination = "";
            this.recognizer = new SpeechRecognizer();
        }

        void textBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void textBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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
            textBox2.Text += path + "\r\n";
            files_to_move.Add(path);
        }
        
        private void clear_Click_1(object sender, EventArgs e)
        {
            textBox2.Text = "";
            files_to_move.Clear();
            source_file_dict.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
                this.destination = fbd.SelectedPath;
            }
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

            for(int i=0; i < fileList.Count; i++) {
                // Have we switched to a new directory?
                string fileDirectory = Path.GetDirectoryName(fileList[i][0]);
                if (fileDirectory != currentDirectory) {
                    prevScene = 1;
                    prevLetter = "A";
                    prevTake = 0;
                }
            }
            //TODO (neil): Finish this
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            foreach (string path in files_to_move)
            {
                string exPath = Application.StartupPath;
                string outPath = exPath + "temporary.wav";
                int x = 0;
                while(File.Exists(outPath))
                {
                    x++;
                    outPath = exPath + "temporary" + x + ".wav";
                }

                string cmdText = "/c ffmpeg -ss 0 -t 30 -i " + path + " " + outPath;
                Process.Start("CMD.exe", cmdText);
                string transcribed = String.Join(" ", this.recognizer.RecognizeSpeech(outPath));
                if(File.Exists(outPath))
                {
                    File.Delete(outPath);
                }

                textBox2.Text += transcribed + "\r\n\r\n";

                source_file_dict.Add(path, text_to_take.SearchStr(transcribed));
            }

            //TODO (neil): Export files as soon as we have all three parts of their information.
            FillMissingInformation();

            file_move.move_and_org(files_to_move, source_file_dict, destination);
        }
    }
}

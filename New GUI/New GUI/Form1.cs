using System;
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

        private void FillMissingInformation() {
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

                textBox2.Text += transcribed;

                source_file_dict.Add(path, text_to_take.SearchStr(transcribed));
            }

            //TODO (neil): Export files as soon as we have all three parts of their information.
            FillMissingInformation();

            file_move.move_and_org(files_to_move, source_file_dict, destination);
        }
    }
}

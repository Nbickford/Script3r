using System;
using System.IO;
using System.Windows.Forms;

namespace New_GUI
{
    class Settings
    {
        private string outputDirectory = "";
        private string subscriptionKey = null;
        private string websocketUri = null;

        public Settings()
        {
            if (!File.Exists("settings.txt"))
            {
                ShowKeyError();
            }
            
            string[] lines = File.ReadAllLines("settings.txt");
            if(lines.Length < 3)
            {
                ShowKeyError();
            }

            if (Directory.Exists(lines[0]))
            {
                outputDirectory = lines[0];
            }

            subscriptionKey = lines[1];
            websocketUri = lines[2];
        }

        private void ShowKeyError()
        {
            MessageBox.Show("Error: As of version 0.43, e.DIT needs a separate API key to work correctly, and you'll need this key to get started. Contact the developers for a key.");
        }

        public string GetOutputDirectory()
        {
            return outputDirectory;
        }

        public string GetSubscriptionKey()
        {
            return subscriptionKey;
        }

        public string GetWebsocketURI()
        {
            return websocketUri;
        }

        public void SetOutputDirectoryName(string directoryName)
        {
            outputDirectory = directoryName;
            Save();
        }

        private void Save()
        {
            try
            {
                TextWriter tw = new StreamWriter("settings.txt");
                tw.WriteLine(outputDirectory);
                tw.WriteLine(subscriptionKey);
                tw.Write(websocketUri);
                tw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured while writing settings file: " + ex.ToString());
            }
        }
    }
}

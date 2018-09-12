using System;
using System.Collections.Generic;
using Microsoft.CognitiveServices.SpeechRecognition;
using System.IO;
using System.Threading;

namespace Script3rSpeech
{
    class SpeechRecognizer
    {
        public bool Succeeded = false;
        private bool runAgain = false;
        public RecognitionStatus lastSpeechStatus;
        public string message = "";

        private readonly string subscriptionKey;
        private readonly string websocketUri;
        private string authenticationUri = ""; // according to the sample app, this is optional
        private const bool DEBUG = false; // For printing out debug information

        private string parentKeyName = "";
        private New_GUI.MainPage m_parent;

        // Manual reset event for making the speech call synchronous
        ManualResetEvent oSignalEvent;

        List<string> parsedPhrases;

        /// <summary>
        /// The data recognition client
        /// </summary>
        private Microsoft.CognitiveServices.SpeechRecognition.DataRecognitionClient dataClient;
        
        public SpeechRecognizer(New_GUI.Settings settings)
        {
            subscriptionKey = settings.GetSubscriptionKey();
            websocketUri = settings.GetWebsocketURI();

            parsedPhrases = new List<string>();
            oSignalEvent = new ManualResetEvent(false);

            //TODO (neil): conversation errors
            //TODO (neil): Check for Internet connection, error out if not available.
            //this.dataClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            //this.dataClient.OnConversationError += this.OnConversationErrorHandler;
        }

        // Destuctor
        ~SpeechRecognizer()
        {
            if (dataClient != null)
            {
                dataClient.Dispose();
            }
        }

        public void Dispose() {
            if (dataClient != null) {
                dataClient.Dispose();
            }
        }

        public List<string> RecognizeSpeech(string inputFile, string videoFile, New_GUI.MainPage parent)
        {
            //TODO (neil): Input file verification
            parsedPhrases = new List<string>();
            Succeeded = false;
            runAgain = true;
            parentKeyName = videoFile;
            m_parent = parent;
            message = "Running...";
            parent.UpdateItemStatus(parentKeyName, message);

            for (int numTries = 3; numTries > 0 && runAgain; numTries--) {
                /*this.dataClient = SpeechRecognitionServiceFactory.CreateDataClient(
                SpeechRecognitionMode.LongDictation,
                "en-us",
                subscriptionKey, websocketUri);*/
                dataClient = SpeechRecognitionServiceFactory.CreateDataClient(
                    SpeechRecognitionMode.ShortPhrase,
                    "en-us",
                    subscriptionKey,
                    subscriptionKey,
                    websocketUri);
                //dataClient.
                dataClient.AuthenticationUri = "https://westus.api.cognitive.microsoft.com/sts/v1.0/issueToken";

                dataClient.OnResponseReceived += OnDataDictationResponseReceivedHandler;

                parsedPhrases.Clear();
                //Read the file into memory in blocks and send to the services API.
                // This is copy-and pasted from the sample code.
                // Input file must be a WAV.
                FileStream fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);

                // Note for wave files, we can just send data from the file right to the server.
                // In the case you are not an audio file in wave format, and instead you have just
                // raw data (for example audio coming over bluetooth), then before sending up any 
                // audio data, you must first send up an SpeechAudioFormat descriptor to describe 
                // the layout and format of your raw audio data via DataRecognitionClient's sendAudioFormat() method.

                //this.dataClient.AudioStart();

                // Length of buffer can be changed; currently set to 2 seconds at 16kHz.
                // (This was originally set to 1024 samples)
                int bytesRead = 0;
                byte[] buffer = new byte[32000];
                int bytesSent = 0;
                parent.UpdateItemStatus(parentKeyName, "Uploading audio file...");
                try {
                    do {
                        // Get more Audio data to send into byte buffer.
                        bytesRead = fileStream.Read(buffer, 0, buffer.Length);

                        // Send of audio data to service. 
                        this.dataClient.SendAudio(buffer, bytesRead);
                        bytesSent += bytesRead;
                        parent.UpdateItemStatus(parentKeyName, "Uploading audio file... "+(bytesRead/1000).ToString()+" kB sent.");
                    }
                    while (bytesRead > 0);
                } finally {
                    // We are done sending audio.  Final recognition results will arrive in OnResponseReceived event call.
                    this.dataClient.EndAudio();
                }

                this.dataClient.AudioStop();

                fileStream.Close();

                parent.UpdateItemStatus(parentKeyName, "Waiting for audio transcription from server...");

                //Wait for the final response event to occur in the OnResponseRecieved event call.
                oSignalEvent.WaitOne(5000);
                oSignalEvent.Reset();
                if (dataClient != null) {
                    dataClient.Dispose();
                }
                //runAgain will tell us whether to run this again or not.
            }

            return parsedPhrases;
        }

        /// <summary>
        /// Called when a final response is received;
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnDataDictationResponseReceivedHandler(object sender, SpeechResponseEventArgs e)
        {
            WriteResponseResult(e);

            // Also copy-and pasted from the sample code.
            if (DEBUG) Console.WriteLine("--- OnDataDictationResponseReceivedHandler ---");
            if (DEBUG) Console.WriteLine(e.PhraseResponse.RecognitionStatus);
            lastSpeechStatus = e.PhraseResponse.RecognitionStatus;

            switch (e.PhraseResponse.RecognitionStatus) {
                case RecognitionStatus.EndOfDictation:
                case RecognitionStatus.DictationEndSilenceTimeout:
                case RecognitionStatus.RecognitionSuccess:
                    if (DEBUG) Console.WriteLine("We have final result!");
                    Succeeded = true;
                    runAgain = false;
                    message = "Succeeded";
                    m_parent.UpdateItemStatus(parentKeyName, message);
                    oSignalEvent.Set();
                    /*Dispatcher.Invoke(
                    (Action)(() => {
                        _startButton.IsEnabled = true;
                        _radioGroup.IsEnabled = true;

                        // we got the final result, so it we can end the mic reco.  No need to do this
                        // for dataReco, since we already called endAudio() on it as soon as we were done
                        // sending all the data.
                    }));*/
                    break;
                case RecognitionStatus.Cancelled:
                    message = "Cancelled by server or client";
                    m_parent.UpdateItemStatus(parentKeyName, message);
                    Succeeded = false;
                    runAgain = true;
                    oSignalEvent.Set();
                    break;
                case RecognitionStatus.RecognitionError:
                    message = "Recognition error (please check file format - ask us for more info)";
                    m_parent.UpdateItemStatus(parentKeyName, message);
                    Succeeded = false;
                    runAgain = false;
                    oSignalEvent.Set();
                    break;
                case RecognitionStatus.NoMatch:
                case RecognitionStatus.InitialSilenceTimeout:
                case RecognitionStatus.BabbleTimeout:
                case RecognitionStatus.HotWordMaximumTime:
                    message = "Too much noise or silence (error: " + e.PhraseResponse.RecognitionStatus.ToString() + ")";
                    m_parent.UpdateItemStatus(parentKeyName, message);
                    runAgain = false;
                    Succeeded = false;
                    //oSignalEvent.Set(); <- why does it keep running after this???
                    break;
                default:
                    message = "Unknown response code: "+e.PhraseResponse.RecognitionStatus.ToString();
                    m_parent.UpdateItemStatus(parentKeyName, message);
                    break;
            }
        }

        /// <summary>
        /// Writes the response result.
        /// </summary>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void WriteResponseResult(SpeechResponseEventArgs e)
        {
            if (e.PhraseResponse.Results.Length == 0)
            {
                if (DEBUG) Console.WriteLine("No phrase response is available.");
            }
            else
            {
                if (DEBUG)
                {
                    Console.WriteLine("********* Final n-BEST Results *********");
                    for (int i = 0; i < e.PhraseResponse.Results.Length; i++)
                    {
                        Console.WriteLine(
                            "[{0}] Confidence={1}, Text=\"{2}\"",
                            i,
                            e.PhraseResponse.Results[i].Confidence,
                            e.PhraseResponse.Results[i].LexicalForm);
                    }
                    Console.WriteLine();
                }

                parsedPhrases.Add(e.PhraseResponse.Results[0].LexicalForm);

            }
        }
    }
}

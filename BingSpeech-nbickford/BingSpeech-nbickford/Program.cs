﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.SpeechRecognition;
using System.IO;

namespace BingSpeech_nbickford {
    class Program {
        static void Main(string[] args) {
            // Test code for running Microsoft's Cognitive Speech Services on an input audio file.

            SpeechRecognizer recognizer = new SpeechRecognizer();
            string recognizedSpeech = recognizer.RecognizeSpeech(@"D:\N.B\Footage\Recordings\LA Hacks Ovation\s1bt2.wav");

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
        
    }

    class SpeechRecognizer{
        private string subscriptionKey = "fd25c649aae54f4aa902edb3a80a34d1";
        private string authenticationUri = ""; // according to the sample app, this is optional

        /// <summary>
        /// The data recognition client
        /// </summary>
        private Microsoft.CognitiveServices.SpeechRecognition.DataRecognitionClient dataClient;

        public SpeechRecognizer() {
            this.dataClient = SpeechRecognitionServiceFactory.CreateDataClient(
                SpeechRecognitionMode.LongDictation,
                "en-us",
                subscriptionKey);
            dataClient.AuthenticationUri = authenticationUri;

            dataClient.OnResponseReceived += OnDataDictationResponseReceivedHandler;
            
            //TODO (neil): Partial responses and conversation errors
            //this.dataClient.OnPartialResponseReceived += this.OnPartialResponseReceivedHandler;
            //this.dataClient.OnConversationError += this.OnConversationErrorHandler;
        }

        public string RecognizeSpeech(string inputFile) {
            //TODO (neil): Input file verification
            //TODO (neil): Make this a blocking call

            //Read the file into memory in blocks and send to the services API.
            // This is copy-and pasted from the sample code.
            // Input file must be a WAV.
            FileStream fileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);

            // Note for wave files, we can just send data from the file right to the server.
            // In the case you are not an audio file in wave format, and instead you have just
            // raw data (for example audio coming over bluetooth), then before sending up any 
            // audio data, you must first send up an SpeechAudioFormat descriptor to describe 
            // the layout and format of your raw audio data via DataRecognitionClient's sendAudioFormat() method.
            int bytesRead = 0;
            byte[] buffer = new byte[1024];

            try {
                do {
                    // Get more Audio data to send into byte buffer.
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);

                    // Send of audio data to service. 
                    this.dataClient.SendAudio(buffer, bytesRead);
                }
                while (bytesRead > 0);
            } finally {
                // We are done sending audio.  Final recognition results will arrive in OnResponseReceived event call.
                this.dataClient.EndAudio();
            }

            return "";
        }
        
        /// <summary>
        /// Called when a final response is received;
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void OnDataDictationResponseReceivedHandler(object sender, SpeechResponseEventArgs e) {
            // Also copy-and pasted from the sample code.
            Console.WriteLine("--- OnDataDictationResponseReceivedHandler ---");
            if (e.PhraseResponse.RecognitionStatus == RecognitionStatus.EndOfDictation || //TODO (neil) Should this be changed?
                e.PhraseResponse.RecognitionStatus == RecognitionStatus.DictationEndSilenceTimeout) {
                Console.WriteLine("We have final result!");

                /*Dispatcher.Invoke(
                    (Action)(() => {
                        _startButton.IsEnabled = true;
                        _radioGroup.IsEnabled = true;

                        // we got the final result, so it we can end the mic reco.  No need to do this
                        // for dataReco, since we already called endAudio() on it as soon as we were done
                        // sending all the data.
                    }));*/
            }

            WriteResponseResult(e);
        }

        /// <summary>
        /// Writes the response result.
        /// </summary>
        /// <param name="e">The <see cref="SpeechResponseEventArgs"/> instance containing the event data.</param>
        private void WriteResponseResult(SpeechResponseEventArgs e) {
            if (e.PhraseResponse.Results.Length == 0) {
                Console.WriteLine("No phrase response is available.");
            } else {
                Console.WriteLine("********* Final n-BEST Results *********");
                for (int i = 0; i < e.PhraseResponse.Results.Length; i++) {
                    Console.WriteLine(
                        "[{0}] Confidence={1}, Text=\"{2}\"",
                        i,
                        e.PhraseResponse.Results[i].Confidence,
                        e.PhraseResponse.Results[i].LexicalForm);
                }

                Console.WriteLine();
            }
        }

        public void Dispose() {
            if (dataClient != null) {
                dataClient.Dispose();
            }
        }
    }
}

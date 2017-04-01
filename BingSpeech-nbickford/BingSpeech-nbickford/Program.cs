using System;
using System.Collections.Generic;
using Script3rSpeech;

namespace BingSpeech_nbickford {
    class Program {
        static void Main(string[] args) {
            // Test code for running Microsoft's Cognitive Speech Services on an input audio file.

            // Call this to set up the speech recognizer
            SpeechRecognizer recognizer = new SpeechRecognizer();

            // Call this for each file you want to convert to text
            List<string> recognizedSpeech = recognizer.RecognizeSpeech(@"D:\N.B\Footage\Recordings\LA Hacks Ovation\s1bt2.wav");

            Console.WriteLine("Here are the phrases we recieved:");
            for (int i = 0; i < recognizedSpeech.Count; i++) {
                Console.WriteLine(recognizedSpeech[i]);
            }

            Console.WriteLine("As a single string:");
            string singleString = String.Join(" ", recognizedSpeech);
            Console.WriteLine(singleString);

            // SpeechRecognizer disposes the sound recognition engine automatically
            // when it is destroyed.

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }
        
    }

    
}

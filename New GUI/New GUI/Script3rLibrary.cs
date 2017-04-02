﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Script3rLibrary {
    class text_to_take {
        //returns integer value of a string of numbers as words
        public static int ParseEnglish(string number) {
            if (number == "")
                return -1;
            string[] words = number.Split(new char[] { ' ', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] ones = { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE" };
            string[] teens = { "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            string[] tens = { "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
            Dictionary<string, int> modifiers = new Dictionary<string, int>()
            {
                {"BILLION", 1000000000},
                {"MILLION", 1000000},
                {"THOUSAND", 1000},
                {"HUNDRED", 100}
            };

            if (words.Length >= 2) {
                if (ones.Contains(words[0]) && tens.Contains(words[1])) {
                    string[] words2 = new string[words.Length + 1];
                    words2[0] = words[0];
                    words2[1] = "HUNDRED";
                    for (int x = 1; x < words.Length; x++) {
                        words2[x + 1] = words[x];
                    }
                    words = words2;
                }
            }

            if (number == "ONE HUNDRED TEN BILLION")
                return int.MaxValue; // 110,000,000,000 is out of range for an int!

            int result = 0;
            int currentResult = 0;
            int lastModifier = 1;

            foreach (string word in words) {
                if (modifiers.ContainsKey(word)) {
                    lastModifier *= modifiers[word];
                } else {
                    int n;

                    if (lastModifier > 1) {
                        result += currentResult * lastModifier;
                        lastModifier = 1;
                        currentResult = 0;
                    }

                    if ((n = Array.IndexOf(ones, word) + 1) > 0) {
                        currentResult += n;
                    } else if ((n = Array.IndexOf(teens, word) + 1) > 0) {
                        currentResult += n + 10;
                    } else if ((n = Array.IndexOf(tens, word) + 1) > 0) {
                        currentResult += n * 10;
                    } else if (word != "AND") {
                        return (-1);
                    }
                }
            }

            return result + currentResult * lastModifier;
        }

        public static Boolean IsNum(string numOrNot) {
            if (ParseEnglish(numOrNot) == -1)
                return false;
            return true;
        }

        //parses audio string to return string list of Scene Number/Letter and Take number OR returns match_fail
        public static string[] SearchStr(string transcript) {
            var fileData = new string[3]; // scene number, scene letter, take number
            for(int i = 0; i < 3; i++) {
                fileData[i] = "match_fail";
            }

            //# replace every homophone of two with two

            transcript = transcript.Replace(" too ", " two ");
            transcript = transcript.Replace(" to ", " two ");
            transcript = transcript.ToUpper();

            //# search for "take"

            int position = transcript.IndexOf("TAKE ");
            if (position == -1) {
                return fileData;
            }

            //# starting from next character, increment until next space to get takenum
            //TODO (neil): clean this up
            int positiont = position + 5;
            string takeNum = "";

            //Positiont starts at a letter; find the next word, and see if it's a number word.
            int nextSpace = transcript.IndexOf(' ', positiont);
            if (nextSpace != -1) {
                takeNum = transcript.Substring(positiont, nextSpace - positiont);
                if (IsNum(takeNum)) {
                    fileData[2] = ParseEnglish(takeNum).ToString();
                }
            }

            //# increment backwards to get the next term for sceneword
            //If we're at the beginning... well, we can't really go back.
            if (position <= 1) return fileData;
            position--; //position should now be a space
            string sceneWord = "";

            int prevSpace = transcript.LastIndexOf(' ', position-1);
            prevSpace++; // this is a letter if there's a word, and the beginning of the file otherwise.
            sceneWord = transcript.Substring(prevSpace, position - prevSpace);

            //If the scene word is a number, we've accidentally read the scene number.
            if (!IsNum(sceneWord) && sceneWord.Length >= 1) {
                fileData[1] = sceneWord[0].ToString(); // get the first letter
                position = prevSpace-1; // position should now be a space
            }

            //# increment backwards to get scene number
            string sceneNum = "";
            if (position <= 0) return fileData;

            prevSpace = transcript.LastIndexOf(' ', position-1);
            prevSpace++; // should now start at a letter
            sceneNum = transcript.Substring(prevSpace, position - prevSpace);

            if (IsNum(sceneNum)) {
                fileData[0] = ParseEnglish(sceneNum).ToString();
            }

            return fileData;
        }
    }

    public class file_move {
        public static void move_and_org(List<string> files_to_move, Dictionary<string, string[]> source_file_dict, string destination) {
            foreach (string path in files_to_move) {
                if (source_file_dict[path][0] == "match_fail") {
                    System.IO.Directory.CreateDirectory(destination + "\\match_fail");
                    File.Move(path, destination + "\\match_fail\\" + Path.GetFileName(path));
                } else {
                    string scene = source_file_dict[path][0];
                    string take = source_file_dict[path][1];
                    string extention = Path.GetExtension(path);
                    System.IO.Directory.CreateDirectory(destination + "\\" + "Scene_" + scene);
                    File.Move(path, destination + "\\" + "Scene_" + scene + "\\" + scene + "_" + take + extention);
                }
            }
        }
    }
}

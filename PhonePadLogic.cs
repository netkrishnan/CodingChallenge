using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge
{
    internal class PhonePadLogic
    {
        // Mapping of keys
        public static Dictionary<char, string> buttonMap = new Dictionary<char, string>
        {
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" }
        };

        public static string OldPhonePad(string input)
        {

            var result = new List<char>(); // Stores the final result
            char lastButton = '\0'; // Last pressed button
            int pressCount = 0; // Number of times the button was pressed

            foreach (char c in input)
            {
                if (c == '#')
                {
                    // Finalize the last character
                    if (lastButton != '\0' && buttonMap.ContainsKey(lastButton))
                    {
                        string letters = buttonMap[lastButton];
                        result.Add(letters[(pressCount - 1) % letters.Length]);
                    }
                    break;
                }
                else if (c == '*')  //Remove the last character
                {
                    if (result.Count > 0)
                    {
                        char? key = FindKeyForCharacter(result[result.Count - 1]);
                        if (key.HasValue)
                        {
                            if ((lastButton != key && pressCount > 1))
                            {
                                pressCount--;
                            }
                            else if (!(lastButton != key && pressCount == 1))
                            {
                                result.RemoveAt(result.Count - 1);
                            }
                            else 
                            {
                                lastButton = '\0';
                                pressCount = 0;
                            }
                        }
                    }
                   
                }
                else if (c == ' ')
                {
                    // Finalize current button and reset
                    if (lastButton != '\0' && buttonMap.ContainsKey(lastButton))
                    {
                        string letters = buttonMap[lastButton];
                        result.Add(letters[(pressCount - 1) % letters.Length]);
                    }
                    lastButton = '\0';
                    pressCount = 0;
                }
                else if (char.IsDigit(c) && buttonMap.ContainsKey(c))
                {
                    if (c == lastButton)
                    {
                        // Increment count if the same button is pressed
                        pressCount++;
                    }
                    else
                    {
                        // Finalize the previous button
                        if (lastButton != '\0' && buttonMap.ContainsKey(lastButton))
                        {
                            string letters = buttonMap[lastButton];
                            result.Add(letters[(pressCount - 1) % letters.Length]);
                        }

                        // Start a new button press
                        lastButton = c;
                        pressCount = 1;
                    }
                }
            }

            return new string(result.ToArray());
        }


        public static char? FindKeyForCharacter(char target)
        {
            foreach (var entry in buttonMap)
            {
                if (entry.Value.Contains(target))
                {
                    return entry.Key;
                }
            }

            // Return null if the character was not found
            return null;
        }



    }
}

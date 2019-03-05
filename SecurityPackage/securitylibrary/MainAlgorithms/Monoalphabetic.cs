using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
          //  throw new NotImplementedException();
            char X = 'a';
            char[] alphaL = new char[26];
            char[] rem = new char[26];
            char[] CT = new char[26];
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            for (int i = 0; i < alphaL.Length; i++)
            {
                alphaL[i] = X;
                X++;
            }


            for (int i = 0; i < cipherText.Length; i++)
            {
                int index1 = Array.IndexOf(alphaL, plainText[i]);
                CT[index1] = cipherText[i];
            }

            int index = 0;
            for (int i = 0; i < CT.Length; i++)
            {

                if (CT.Contains(alphaL[i]) != true)
                {
                    rem[index] = alphaL[i];
                    index++;
                }
            }

            index = 0;
            for (int i = 0; i < CT.Length; i++)
            {
                if (index <= 25)
                {
                    if (CT[i] == '\0')
                    {
                        CT[i] = rem[index];
                        index++;
                    }
                }
            }
           string S;
          return  S = new string(CT);
        }

        public string Decrypt(string cipherText, string key)
        {
            char X = 'A';
            char[] alphaU = new char[26];
            int[] CTIndex = new int[cipherText.Length];
            char[] CT = new char[cipherText.Length];
            int[] KEY = new int[key.Length];
            cipherText = cipherText.ToUpper();
            key = key.ToUpper();
            for (int i = 0; i < alphaU.Length; i++)
            {
                alphaU[i] = X;
                X++;
            }

            for (int i = 0; i < key.Length; i++)
            {
                KEY[i] = key[i];
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                int index1 = Array.IndexOf(KEY, cipherText[i]);
                CTIndex[i] = index1;
                CT[i] = alphaU[CTIndex[i]];

            }
            string s;
            return s = new string(CT);
           // throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            char X = 'A';
            char[] alphaU = new char[26];
            int[] CTIndexP = new int[plainText.Length];
            char[] CT = new char[plainText.Length];
            plainText = plainText.ToUpper();
            key = key.ToUpper();
            for (int i = 0; i < alphaU.Length; i++)
            {
                alphaU[i] = X;
                X++;
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                int index1 = Array.IndexOf(alphaU, plainText[i]);
                CTIndexP[i] = index1;
                CT[i] = key[CTIndexP[i]];

            }

           
            string s;
            return s = new string(CT);

            //throw new NotImplementedException();
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            cipher = cipher.ToLower();
            int count = 0;
            int index = 0;
            char[] SoredArray = new char[cipher.Length];
            Dictionary<char, int> output = new Dictionary<char, int>();
            char[] alpha = { 'e', 't', 'a', 'o', 'i', 'n', 's', 'r', 'h', 'l', 'd', 'c', 'u', 'm', 'f', 'p', 'g', 'w', 'y', 'b', 'v', 'k', 'x', 'j', 'q', 'z' };
            Dictionary<char, int> freq = new Dictionary<char, int>()
            {
                {'a',count },
                {'b', count},
                {'c',count },
                {'d', count},
                {'e',count },
                {'f', count},
                {'g',count },
                {'h', count},
                {'i',count },
                {'j', count},
                {'k',count },
                {'l', count},
                {'m',count },
                {'n', count},
                {'o',count },
                {'p',count },
                {'q', count},
                {'r',count },
                {'s', count},
                {'t',count },
                {'u', count},
                {'v',count },
                {'w', count},
                {'x',count },
                {'y', count},
                {'z', count},
            };
            for (int i = 0; i < cipher.Length; i++)
            {
                count = freq[cipher[i]];

                if (freq.ContainsKey(cipher[i]))
                {
                    freq[cipher[i]] = count + 1;
                }
            }

            var items = from pair in freq
                        orderby pair.Value descending
                        select pair;


            foreach (KeyValuePair<char, int> pair in items)
            {
                output.Add(pair.Key, pair.Value);
            }

            for (int i = 0; i < cipher.Length; i++)
            {
                index = output.Keys.ToList().IndexOf(cipher[i]);
                SoredArray[i] = alpha[index];

            }


            string s;
            return s = new string(SoredArray);


        //    throw new NotImplementedException();
        }
    }
}
 
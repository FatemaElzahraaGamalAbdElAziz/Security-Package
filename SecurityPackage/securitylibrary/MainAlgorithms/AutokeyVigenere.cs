using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public static string ChopKeyAuto(string key,string plainText)
        {
            string result = "";
            int l = 0;
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] != plainText[l])
                {
                    result += key[i];
                }
                else
                {
                    if (i + plainText.Length > key.Length)
                    {
                        key = result;

                        break;
                    }
                    string temp = key.Substring(i, plainText.Length);
                    if (temp == plainText)
                    {
                        key = key.Substring(0, i);
                    }
                }
            }
            return key;
        }
        public static char[,] GenerateKeyStream()
        {
            char[,] Keystream = new char[26, 26];
            char First = 'A';
            bool StartOver = false;
            for (int i = 0; i < 26; i++)
            {
                char Second = 'A';
                for (int j = 0; j < 26; j++)
                {
                    if (StartOver == true)
                    {
                        Keystream[i, j] = Second;
                        Second = Convert.ToChar(Second + 1);
                    }
                    else
                    {
                        Keystream[i, j] = Convert.ToChar((First + j));
                    }
                    if (Keystream[i, j] == 'Z')
                    {
                        StartOver = true;
                    }
                }
                First = Convert.ToChar(First + 1);
                StartOver = false;
            }
            return Keystream;
        }

        public string Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string CipherText, string key)
        {
            string plainText = "";
            int IntialKeyLength = 0, MaxPlainTextLength = 0;
            while (plainText.Length < CipherText.Length)
            {
                for (int i = IntialKeyLength; i < key.Length; i++)
                {
                    if (i >= CipherText.Length) break;
                    int Column = Convert.ToInt32(CipherText[i] + 32 - key[i]);
                    if (Column < 0)
                    {
                        Column += 26;
                    }
                    plainText += Convert.ToChar(Column + 'a');

                }
                IntialKeyLength = key.Length;
                key += plainText.Substring(MaxPlainTextLength, plainText.Length - MaxPlainTextLength);
                MaxPlainTextLength += (plainText.Length - MaxPlainTextLength);
            }
            return plainText;   
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            string CipherText = plainText;
            while (key.Length < plainText.Length)
            {
                key += plainText;
            }
            char[,] Keystream = GenerateKeyStream();

            for (int i = 0; i < plainText.Length; i++)
            {
                StringBuilder sb = new StringBuilder(CipherText);
                sb[i] = Keystream[Convert.ToInt32(plainText[i] - 'a'), Convert.ToInt32(key[i] - 'a')];
                CipherText = sb.ToString();
            }
            return CipherText;
        }
    }
}

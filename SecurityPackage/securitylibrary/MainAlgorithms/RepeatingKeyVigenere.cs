﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {

        public static char[,] GenerateKeyStream()
        {
            char[,] Keystream = new char[26, 26];
            char First = 'a';
            bool StartOver = false;
            for (int i = 0; i < 26; i++)
            {
                char Second = 'a';
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
                    if (Keystream[i, j] == 'z')
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
           // throw new NotImplementedException();
            string  plainText = CipherText;
            char[,] Keystream = GenerateKeyStream();
            while (key.Length < plainText.Length)
            {
                key += key;
            }

            for (int i = 0; i < CipherText.Length; i++)
            {
                StringBuilder sb = new StringBuilder(plainText);
                int Column = Convert.ToInt32(CipherText[i] - key[i]);
                if (Column < 0)
                {
                    Column += 26;
                }
                sb[i] = Convert.ToChar(Column + 'a');
                plainText = sb.ToString();
            }
            return plainText;   
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            string CipherText = plainText;
            while (key.Length < plainText.Length)
            {
                key += key;
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
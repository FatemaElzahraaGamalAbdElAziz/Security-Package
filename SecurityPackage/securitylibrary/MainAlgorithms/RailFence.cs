using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            throw new NotImplementedException();
        }

        public string Decrypt(string CipherText, int key)
        {
            int Depth = (int)Math.Ceiling((double)CipherText.Length / key);
            char[,] Matrix = new char[key, Depth];
            int index = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < Depth; j++)
                {
                    Matrix[i, j] = CipherText[index];
                    index++;
                }
            }
            string Result = "";
            for (int i = 0; i < Depth; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    Result += (Matrix[j, i]);
                }
            }
            return Result;
        }

        public string Encrypt(string plainText, int key)
        {
            int Depth = (int)Math.Ceiling((double)plainText.Length / key);
            char[,] Matrix = new char[key, Depth];
            int index = 0;
            for (int i = 0; i < Depth; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    if (index < plainText.Length)
                    {
                        Matrix[j, i] = plainText[index];
                        index++;
                    }
                    else
                    {
                        break;
                        Matrix[j, i] = 'x';

                    }
                }
            }
            string Result = "";
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < Depth; j++)
                {
                    Result += (Matrix[i, j]);
                }
            }
            return Result;
        }
    }
}

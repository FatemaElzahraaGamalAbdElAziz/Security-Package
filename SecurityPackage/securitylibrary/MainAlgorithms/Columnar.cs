using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public static void GetPermutation(List<List<int>> All, List<int> key, int size)
        {
            if (size == 1)
            {
                All.Add(new List<int>(key));

            }
            for (int i = 0; i < size; i++)
            {
                GetPermutation(All, key, size - 1);
                if (size % 2 == 0)
                {
                    int temp = key[i];
                    key[i] = key[size - 1];
                    key[size - 1] = temp;
                }
                else
                {
                    int temp = key[0];
                    key[0] = key[size - 1];
                    key[size - 1] = temp;
                }
            }
        }
        public List<int> Analyse(string plainText, string CipherText)
        {
           // throw new NotImplementedException();
            List<int> key = new List<int>();
            int size = 1;
            bool Stop = false;
            while (Stop == false)
            {
                if (size > 10) break;
                key = new List<int>(size);
                for (int i = 0; i < size; i++)
                {
                    key.Add(i + 1);
                }
                List<List<int>> AllKeys = new List<List<int>>();
                GetPermutation(AllKeys, key, key.Count);
                foreach (var tempKey in AllKeys)
                {
                    string Result = Encrypt(plainText, tempKey);
                    if (Result.Equals(CipherText, StringComparison.InvariantCultureIgnoreCase))
                    {
                        key = tempKey;
                        Stop = true;
                        break;
                    }
                }
                size++;
            }
            return key;
        }

        public string Decrypt(string CipherText, List<int> key)
        {
            //throw new NotImplementedException();
            int col = key.Count, row = (int)Math.Ceiling((double)CipherText.Length / key.Count);
            char[,] Matrix = new char[row, col], NewMatrix = new char[row, col];
            int index = 0;
            for (int j = 0; j < col; j++)
            {
                for (int i = 0; i < row; i++)
                {
                    if (index < CipherText.Length)
                    {
                        Matrix[i, j] = CipherText[index];
                        index++;
                    }
                    else
                    {
                        Matrix[i, j] = ' ';
                    }
                }
            }
            string Result = "";
            for (int j = 0; j < col; j++)
            {
                for (int i = 0; i < row; i++)
                {
                    index = key.IndexOf(j + 1);
                    NewMatrix[i, index] = Matrix[i, j];
                }
            }
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Result += NewMatrix[i, j];
                }
            }
            return Result.ToLower();
        }

        public string Encrypt(string plainText, List<int> key)
        {
            int col = key.Count, row = (int)Math.Ceiling((double)plainText.Length / key.Count);
            char[,] Matrix = new char[row, col], NewMatrix = new char[row, col];
            int index = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (index < plainText.Length)
                    {

                        Matrix[i, j] = plainText[index];
                        index++;
                    }
                    else
                    {
                        Matrix[i, j] = 'x';
                    }
                }
            }
            string Result = "";
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int t = key.IndexOf(j + 1);
                    NewMatrix[i, j] = Matrix[i, t];
                }
            }
            for (int j = 0; j < col; j++)
            {
                for (int i = 0; i < row; i++)
                {
                    Result += NewMatrix[i, j];
                }
            }
            return Result.ToUpper();
        }
    }
}

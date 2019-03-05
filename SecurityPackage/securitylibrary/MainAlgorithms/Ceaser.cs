using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {

        public string Encrypt(string plainText, int key)
        {
            char X = 'A';
            char[] alphaU = new char[26];
            int[] CTIndex = new int[plainText.Length];
            char[] CT = new char[plainText.Length];
            plainText = plainText.ToUpper();
            for (int i = 0; i < alphaU.Length; i++)
            {
                alphaU[i] = X;
                X++;
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                    int index1 = Array.IndexOf(alphaU, plainText[i]);
                    CTIndex[i] = (index1 + key) % 26;
                    CT[i] = alphaU[CTIndex[i]];
               }
                   string s;
                   return s = new string(CT);
        }


        public string Decrypt(string cipherText, int key)
        {
            char X = 'A';
            char[] alphaU = new char[26];
            int[] CTIndex = new int[cipherText.Length];
            char[] CT = new char[cipherText.Length];
            cipherText = cipherText.ToUpper();
            for (int i = 0; i < alphaU.Length; i++)
            {
                alphaU[i] = X;
                X++;
            }

            for (int i = 0; i < cipherText.Length; i++)
            {
                int index1 = Array.IndexOf(alphaU, cipherText[i]);
                int calc = index1 - key;
                if (calc >= 0)
                {

                    CTIndex[i] = (calc) % 26;
                }
                else
                {
                    calc = calc * -1;
                    CTIndex[i] = 26 - calc;
                }

                CT[i] = alphaU[CTIndex[i]];
            }
                   string s;
                   return s = new string(CT);
        }
                    
            
        

        public int Analyse(string plainText, string cipherText)
        {
            
            int Key = 0;
            char X = 'A';
            char[] alphaU = new char[26];
            int[] CTIndex = new int[cipherText.Length];
            char[] CT = new char[cipherText.Length];
            int[] CTIndexP = new int[plainText.Length];
            char[] CTP = new char[plainText.Length];
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            for (int i = 0; i < alphaU.Length; i++)
            {
                alphaU[i] = X;
                X++;
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                int index1 = Array.IndexOf(alphaU, plainText[i]);
                CTIndexP[i] = index1;
                // Console.WriteLine(CTIndexP[i]);
                //CT[i] = alphaU[CTIndex[i]];
            }



            for (int i = 0; i < cipherText.Length; i++)
            {
                int index1 = Array.IndexOf(alphaU, cipherText[i]);
                CTIndex[i] = index1;

                //   Console.WriteLine("Hello");
                // Console.WriteLine(CTIndex[i]);
            }

            for (int i = 0; i < plainText.Length; i++)
            {
                Key = (CTIndex[i] - CTIndexP[i]);
                if (Key >= 0)
                {
                    Key = Key % 26;

                }
                else
                {

                    Key = Key * -1;
                    Key = 26 - Key;
                }
            }

            return Key;
            //throw new NotImplementedException();
        }
    }
}

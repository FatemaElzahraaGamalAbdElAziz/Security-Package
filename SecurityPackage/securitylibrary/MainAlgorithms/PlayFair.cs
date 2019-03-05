using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        char[,] arr = new char[5, 5];
        int[] marked = new int[27];
        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();

            key = key.ToLower();

            for (int i = 0; i < marked.Length; i++)
            {
                marked[i] = -1;
            }
            BuildGrid(key);

            string ans = "";
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                int r1, col1, r2, col2, tmp1, tmp2;
                tmp1 = marked[cipherText[i] - 'a'];
                tmp2 = marked[cipherText[i + 1] - 'a'];
                r1 = tmp1 / 5;
                col1 = tmp1 - (r1 * 5);
                r2 = tmp2 / 5;
                col2 = tmp2 - (5 * r2);
                if (r1 == r2)
                {
                    if (col1 == 0) { col1 = 4; }
                    else col1--;

                    if (col2 == 0) { col2 = 4; }
                    else col2--;

                    ans += arr[r1, col1];
                    ans += arr[r1, col2];
                }
                else if (col2 == col1)
                {
                    if (r1 == 0) { r1 = 4; }
                    else r1--;

                    if (r2 == 0) { r2 = 4; }
                    else r2--;

                    ans += arr[r1, col1];
                    ans += arr[r2, col1];
                }
                else
                {
                    ans += arr[r1, col2];
                    ans += arr[r2, col1];
                }

            }
            string res = "";
            for (int i = 0; i < ans.Length; i += 2)
            {
                if (i + 1 == ans.Length - 1 && ans[i + 1] == 'x')
                {
                    res += ans[i];
                    break;
                }

                if (i + 2 < ans.Length && ans[i + 1] == 'x' && ans[i] == ans[i + 2])
                {
                    res += ans[i];
                }
                else
                {
                    res += ans[i];
                    res += ans[i + 1];
                }
            }
            return res;

        }

        public void BuildGrid(string key)
        {
            int r = 0, col = 0;
            for (int i = 0; i < key.Length; i++)
            {

                if (marked[key[i] - 'a'] == -1)
                {

                    marked[key[i] - 'a'] = (5 * r) + col;
                    arr[r, col] = key[i];
                    if (key[i] == 'i' || key[i] == 'j')
                    {
                        marked['i' - 'a'] = (5 * r) + col;
                        marked['j' - 'a'] = (5 * r) + col;
                    }
                    col++;
                    if (col == 5)
                    {
                        r++;
                    }
                    r %= 5; col %= 5;
                }
            }


            for (int j = 0; j < 26; j++)
            {
                char y = 'a';
                y += (char)j;
                if (marked[y - 'a'] == -1)
                {
                    marked[y - 'a'] = (5 * r) + col;
                    arr[r, col] = y;
                    if (y == 'i' || y == 'j')
                    {
                        marked['i' - 'a'] = (5 * r) + col;
                        marked['j' - 'a'] = (5 * r) + col;
                    }
                    col++;
                    if (col == 5)
                    {
                        r++;
                    }
                    r %= 5; col %= 5;
                }
                if (j == 8) j++;
            }

        }


        public string Encrypt(string plainText, string key)
        {
            plainText = plainText.ToLower();

            key = key.ToLower();

            for (int i = 0; i < marked.Length; i++)
            {
                marked[i] = -1;
            }
            BuildGrid(key);
            string ans = "", tmp = "";

            for (int i = 0; i < plainText.Length; i += 2)
            {
                tmp += plainText[i];
                if (i + 1 >= plainText.Length) break;
                if (plainText[i] == plainText[i + 1])
                {

                    tmp += 'x';
                    i--;
                }
                else tmp += plainText[i + 1];

            }
            if (tmp.Length % 2 == 1)
            {
                tmp += 'x';
            }

            for (int i = 0; i < tmp.Length; i += 2)
            {
                int r1 = 0, col1 = 0, r2 = 0, col2 = 0, tmp1, tmp2;
                tmp1 = marked[tmp[i] - 'a'];
                tmp2 = marked[tmp[i + 1] - 'a'];
                r1 = tmp1 / 5;
                col1 = tmp1 - (r1 * 5);
                r2 = tmp2 / 5;
                col2 = tmp2 - (r2 * 5);
                if (r1 == r2)
                {
                    ans += arr[r1, (col1 + 1) % 5];
                    ans += arr[r1, (col2 + 1) % 5];
                }
                else if (col1 == col2)
                {
                    ans += arr[(r1 + 1) % 5, col1];
                    ans += arr[(r2 + 1) % 5, col1];
                }
                else
                {

                    ans += arr[r1, col2];
                    ans += arr[r2, col1];
                }
            }
            return ans;



            // throw new NotImplementedException();
        }
    }
}
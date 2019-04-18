using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.DiffieHellman
{
    public class DiffieHellman 
    {
      
        public static int FastPower(int b, int p, int mod)
        {
            if (p == 0) return 1;
            int res = FastPower(b, p / 2, mod);
            res *= res;
            res %= mod;
            if (p % 2 == 1) res = (res * b) % mod;
            return res;
        }
        public List<int> GetKeys(int q, int alpha, int xa, int xb)
        {
            List<int> Result = new List<int>();
            int ya = FastPower(alpha, xa, q);
            int yb = FastPower(alpha, xb, q);
            Console.WriteLine(ya + " " + yb);
            Result.Add( FastPower(yb, xa, q));
            Result.Add(FastPower(ya, xb, q));
            return Result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace KingSortProgram
{
    public class RomanNumbersComparer : IComparer<string>
    {
        public int Compare(string x, string y) { return ValueOf(x).CompareTo(ValueOf(y)); }
        public int GetValueOf(string s) { return ValueOf(s); }

        bool IsValidRomanNumber(string s) { return true; }
        int ValueOf(char c)
        {
            switch (c)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                default: throw new Exception("Número romando inválido.");
            }
        }

        public int ValueOf(string s)
        {
            if (IsValidRomanNumber(s) == false) throw new Exception("Número romando inválido.");
            int acc, q, w, e;
            acc = q = w = e = 0;

            for (int i = 0; i < s.Length; ++i)
            {
                q = w; w = e; e = ValueOf(s[i]);
                if (w == 0) continue;
                if (q == 0)
                {
                    if (w < e) { acc += e - w; w = e = 0; }
                    else if (w > e) { acc += w; w = 0; }
                }
                else
                {
                    if (q == w && w == e) { acc += q + w + e; q = w = e = 0; }
                    else if (q == w) { acc += q + w; q = w = 0; }
                    else { acc += q; }
                }
            }
            acc += q + w + e;

            return acc;
        }
    }
}

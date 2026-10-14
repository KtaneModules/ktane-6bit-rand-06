using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SixBitExtensionsNamespace
{
    public static class SixBitExtensions
    {
        public static char getChar(int index) =>
            "_abcdefg0123456789.,!@#$ABCDEFGHIJKLMNOPQRSTUVWXYZ^&*-+=~?<>[]()"[index];

        public static int getInt(char c) =>
            "_abcdefg0123456789.,!@#$ABCDEFGHIJKLMNOPQRSTUVWXYZ^&*-+=~?<>[]()".IndexOf(c);

        public static int mod(int a, int b) => b == 0 ? 0 : ((a % b) + b) % b;

        public static int fromBinary(string bin) =>
            bin.ToCharArray().AsEnumerable().Reverse().Select((x, i) => x == '1' ? 1 << i : 0).Sum();

        public static string binToOct(string bin)
        {
            List<bool> rBin = bin.ToCharArray().AsEnumerable().Reverse().Select(x => x == '1').ToList();
            List<char> ans = new List<char>();
            int m = 0;
            for (int i = 0; i < rBin.Count; i++)
            {
                if (rBin[i]) m += 1 << (i % 3);
                if (i % 3 == 2 || i == rBin.Count - 1)
                {
                    ans.Add("01234567"[m]);
                    m = 0;
                }
            }

            return ans.AsEnumerable().Reverse().Select(x => x.ToString()).Aggregate((a, b) => a + b);
        }

        public static string decToOct(int num, int octDigits)
        {
            List<char> ans = new List<char>();
            for (; octDigits > 0; octDigits--)
            {
                ans.Add("01234567"[num % 8]);
                num /= 8;
            }
            return ans.AsEnumerable().Reverse().Select(x => x.ToString()).Aggregate((a, b) => a + b);
        }

        public static string randomBinary(int length)
        {
            return Enumerable.Range(0, length).Select(_ => Random.value>.5f?"1":"0").Aggregate((a, b) => a + b);
        }

        public static List<List<T>> Chunk<T>(this List<T> list, int chunkSize)
        {
            int taken = 0;
            List<List<T>> chunks = new List<List<T>>();
            while (taken < list.Count)
            {
                chunks.Add(list.Skip(taken).Take(chunkSize).ToList());
                taken += chunkSize;
            }
            return chunks;
        }

        public static int reverseNumber(int num)
        {
            return Enumerable.Range(0, 6).Select(x => (num & (1 << x)) > 0).Reverse().Select((b, i) => b ? 1 << i : 0)
                .Sum();
        }
    }
}
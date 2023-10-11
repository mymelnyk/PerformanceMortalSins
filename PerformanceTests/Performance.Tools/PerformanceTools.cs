using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Performance.Tools
{
    public class PerformanceTools
    {
        private const string VowCharList = "AEIOUY-  ";
        private const string ConsCharList = "BCDFGHJKLMNPQRSTVWXZ-  ";

        public static IEnumerable<int> GetIntCollection(int n = 10000000)
        {
            var randomizer = new Random();

            for (var i = 0; i < n; i++)
                yield return randomizer.Next(0, 1000);
        }

        public static IEnumerable<string> GetStringCollection(int n = 10000000)
        {
            for (var i = 0; i < n; i++)
                yield return GetRandomString();
        }

        public static bool GetRandomBool()
            => new Random().Next(0, 9) >= 5;


        public static string GetRandomString()
        {
            var randomizer = new Random();

            var valueLength = 10;
            var value = string.Empty;
            var vowel = false;
            var prevSymbol = string.Empty;
            for (var i = 0; i < valueLength; i++)
            {
                var consCharList = prevSymbol == " " || prevSymbol == "-" || i == 0
                    ? ConsCharList.Trim().Replace("-", "") : ConsCharList;
                var vowCharList = prevSymbol == " " || prevSymbol == "-"
                    ? VowCharList.Trim().Replace("-", "") : VowCharList;
                var symbol = vowel
                    ? vowCharList[randomizer.Next(0, vowCharList.Length - 1)]
                    : consCharList[randomizer.Next(0, consCharList.Length - 1)];
                value += symbol;
                prevSymbol = symbol.ToString();
                vowel = !vowel;
            }

            return value;
        }


        public static TimeSpan EstimateAction(Action action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            action?.Invoke();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        public static TimeSpan MinTimeSpan(params TimeSpan[] t)
        {
            if (t.Length == 0) return default;
            if (t.Length == 1) return t[0];
            var min = t[0];
            for (var i = 1; i < t.Length; i++)
            {
                if (t[i] < min)
                    min = t[i];
            }
            return min;
        }

        public static int MinTimeSpanIdx(params TimeSpan[] t)
        {
            if (t.Length == 0) return 0;
            if (t.Length == 1) return 1;
            var min = 0;
            for (var i = 1; i < t.Length; i++)
            {
                if (t[i] < t[min])
                    min = i;
            }
            return min + 1;
        }
        public static int MaxTimeSpanIdx(params TimeSpan[] t)
        {
            if (t.Length == 0) return 0;
            if (t.Length == 1) return 1;
            var min = 0;
            for (var i = 1; i < t.Length; i++)
            {
                if (t[i] > t[min])
                    min = i;
            }
            return min + 1;
        }

    }
}

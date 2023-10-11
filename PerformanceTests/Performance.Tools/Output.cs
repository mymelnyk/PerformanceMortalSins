using System;

namespace Performance.Tools
{
    public class Output
    {
        private const int Padding = 20;
        private readonly string[] _headers;
        private readonly int[] _effectiveness;
        private int _iterations;
        private string _title;

        public Output(params string[] headerList)
        {
            _headers = headerList;
            _effectiveness = new int[_headers.Length];
        }

        public void Headers(string title)
        {
            _title = title;

            _iterations = 0;
            for (var i = 0; i < _effectiveness.Length; i++)
                _effectiveness[i] = 0;

            Console.WriteLine($" {title.ToUpper()} ");
            Console.WriteLine("".PadRight(title.Length + 2,'*'));
            Console.WriteLine();

            foreach (var h in _headers)
                Console.Write($"{h,Padding}");
            Console.WriteLine($"{"Winner",Padding}{"Loser",Padding}");
        }

        public void Data(params TimeSpan[] times)
        {
            foreach (var t in times)
                Console.Write($"{t.TotalMilliseconds,(Padding - 3)} ms");
            var winnerIdx = PerformanceTools.MinTimeSpanIdx(times);
            var looserIdx = PerformanceTools.MaxTimeSpanIdx(times);
            Console.WriteLine($"{_headers[winnerIdx - 1],Padding}{_headers[looserIdx - 1],Padding}");
            _effectiveness[winnerIdx - 1]++;
            _iterations++;
        }

        public void Effectiveness()
        {
            if (_iterations == 0)
                return;
            Console.WriteLine();
            Console.WriteLine($"'{_title}' Effectiveness:");
            for (var i = 0; i < _effectiveness.Length; i++)
                Console.WriteLine($"{_headers[i],20} {(double)_effectiveness[i] / _iterations,10:P2}%");
        }

        public void WaitKey()
        {
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}

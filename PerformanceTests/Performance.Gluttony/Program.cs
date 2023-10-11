using System.Linq;
using System.Text;
using Performance.Tools;

namespace Performance.Gluttony
{
    internal class Program
    {
        private const int CollectionSize = 25000;
        private const int TestNo = 20;
        private static Output _output = new Output("String", "StringBuilder", "List");

        private static void ConcatenateWithString(int n = CollectionSize)
        {
            var s = string.Empty;
            for (int i = 0; i < n; i++)
            {
                s += "," + PerformanceTools.GetRandomString();
            }
        }

        private static string ConcatenateWithStringBuilder(int n = CollectionSize)
        {
            var s = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                s.Append("," + PerformanceTools.GetRandomString());
            }

            return s.ToString();
        }

        private static string ConcatenateWithList(int n = CollectionSize)
        {
            var s = Enumerable.Range(0, n).Select(_ => PerformanceTools.GetRandomString());
            return string.Join(",m", s);
        }


        private static void Main(string[] args)
        {
            _output.Headers("Concatenation types");
            for (var i = 0; i < TestNo; i++)
            {
                var time1 = PerformanceTools.EstimateAction(() => ConcatenateWithString());
                var time2 = PerformanceTools.EstimateAction(() => ConcatenateWithStringBuilder());
                var time3 = PerformanceTools.EstimateAction(() => ConcatenateWithList());

                _output.Data(time1, time2, time3);
            }
            _output.Effectiveness();

            _output.WaitKey();
        }
    }
}

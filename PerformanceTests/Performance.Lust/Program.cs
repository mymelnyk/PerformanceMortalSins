using System.Linq;
using Performance.Tools;

namespace Performance.Lust
{
    internal class Program
    {
        private static Output _output = new Output("Light+Heavy","Heavy+Light");
        private const int TestNo = 20; 
        public static bool CalculatedPredicate()
        {
            var collection = PerformanceTools.GetIntCollection();
            var oddsCount = collection.Count(e => e % 2 == 1);
            return oddsCount % 2 == 0;
        }

        private static void Main(string[] args)
        {
            _output.Headers("Predicate order");
            for (var i = 0; i < TestNo; i++)
            {
                var time1 = PerformanceTools.EstimateAction(() =>
                {
                    var test = PerformanceTools.GetRandomBool() || CalculatedPredicate();
                    //var test = true || CalculatedPredicate();
                });

                var time2 = PerformanceTools.EstimateAction(() =>
                {
                    //var test = CalculatedPredicate() || true;
                    var test = CalculatedPredicate() || PerformanceTools.GetRandomBool();
                });
                _output.Data(time1, time2);
            }
            _output.Effectiveness();
            _output.WaitKey();
        }
    }
}

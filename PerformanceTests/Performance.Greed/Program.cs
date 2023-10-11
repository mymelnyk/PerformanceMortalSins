using Performance.Tools;

namespace Performance.Greed
{
    internal class Program
    {
        private const int IterationNo = 10000000;
        private const int TestNo = 20;
        private static readonly object LockObj = new object();
        private static readonly Output _output = new Output("Many locks", "One Lock", "NoLock");

        private static void IncWithManyLocks()
        {
            var n = 0;
            for (var i = 0; i < IterationNo; i++)
            {
                lock (LockObj)
                {
                    n++;
                }
            }
        }

        private static void IncWithOneLock()
        {
            var n = 0;
            lock (LockObj)
            {
                for (var i = 0; i < IterationNo; i++)
                    n++;
            }
        }

        private static void IncWithoutLock()
        {
            var n = 0;
            for (var i = 0; i < IterationNo; i++)
                n++;
        }

        static void Main(string[] args)
        {
            _output.Headers("Locks");

            for (var i = 0; i < TestNo; i++)
            {
                var time1 = PerformanceTools.EstimateAction(IncWithManyLocks);
                var time2 = PerformanceTools.EstimateAction(IncWithOneLock);
                var time3 = PerformanceTools.EstimateAction(IncWithoutLock);
                _output.Data(time1, time2, time3);
            }
            _output.Effectiveness();
            _output.WaitKey();
        }
    }
}

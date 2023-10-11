using Performance.Tools;

const int IterationNo = 10000000;
const int TestNo = 20;
object LockObj = new object();
Output _output = new Output("Many locks", "One lock", "NoLock");

void IncWithManyLocks()
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

void IncWithOneLock()
{
    var n = 0;
    lock (LockObj)
    {
        for (var i = 0; i < IterationNo; i++)
        {
                n++;
        }
    }
}

void IncWithoutLock()
{
    var n = 0;
    for (var i = 0; i < IterationNo; i++)
        n++;
}

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


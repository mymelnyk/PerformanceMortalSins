using Performance.Tools;

var output = new Output("For", "Foreach", "LINQ", "Parallel", "ForEach list");
const int testNo = 30;


output.Headers("Loops race");
for (var j = 0; j < testNo; j++)
{
    var collection = PerformanceTools.GetIntCollection().ToList();

    var time1 = PerformanceTools.EstimateAction(() =>
    {
        var oddCount = 0;
        for (var i = 0; i < collection.Count; i++)
            if (collection[i] % 2 == 1)
                oddCount++;
    });

    var time2 = PerformanceTools.EstimateAction(() =>
    {
        var oddCount = 0;
        foreach (var e in collection)
            if (e % 2 == 1)
                oddCount++;
    });

    var time3 = PerformanceTools.EstimateAction(() =>
    {
        var oddCount = collection.Count(e => e % 2 == 1);
    });

    var time4 = PerformanceTools.EstimateAction(() =>
    {
        var oddCount = 0;
        Parallel.ForEach(collection, e =>
        {
            if (e % 2 == 1) Interlocked.Increment(ref oddCount);
        });
    });

    var time5 = PerformanceTools.EstimateAction(() =>
    {
        var oddCount = 0;
        collection.ForEach(e =>
        {
            if (e % 2 == 1)
                oddCount++;
        });
    });

    output.Data(time1, time2, time3, time4, time5);
}
output.Effectiveness();
output.WaitKey();
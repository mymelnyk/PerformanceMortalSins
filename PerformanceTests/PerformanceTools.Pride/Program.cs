using System.Collections.Concurrent;
using Performance.Tools;

const int TestNo = 20;

var output = new Output("Array", "List", "HashSet", "ConcurrentBag", "Span", "Memory");

string[] array;
List<string> list;
HashSet<string> hashSet;
ConcurrentBag<string> bag;
IEnumerable<string> collection;
Memory<string> memory;

output.Headers("Materialization");

var key = Console.ReadKey();
if (key.Key != ConsoleKey.Escape)
{
    for (var i = 0; i < TestNo; i++)
    {
        collection = PerformanceTools.GetStringCollection(100000);

        var timeArray = PerformanceTools.EstimateAction(() =>
        {
            array = collection.ToArray();
        });

        var timeList = PerformanceTools.EstimateAction(() =>
        {
            list = collection.ToList();
        });

        var timeHashSet = PerformanceTools.EstimateAction(() =>
        {
            hashSet = collection.ToHashSet();
        });

        var timeBag = PerformanceTools.EstimateAction(() =>
        {
            bag = new ConcurrentBag<string>(collection);
        });

        var arr = collection.ToArray();
        var timeSpan = PerformanceTools.EstimateAction(() =>
        {
            var span = new Span<string>(arr);
        });

        var timeMem = PerformanceTools.EstimateAction(() =>
        {
            memory = new Memory<string>(arr);
        });

        output.Data(timeArray, timeList, timeHashSet, timeBag, timeSpan, timeMem);
    }
    output.Effectiveness();
}


output.Headers("Calculating max");

key = Console.ReadKey();
if (key.Key != ConsoleKey.Escape)
{
    for (var i = 0; i < TestNo; i++)
    {
        collection = PerformanceTools.GetStringCollection(100000);
        array = collection.ToArray();
        list = collection.ToList();
        hashSet = collection.ToHashSet();
        bag = new ConcurrentBag<string>(collection);
        memory = new Memory<string>(array);

        var arr = collection.ToArray();

        var timeArray = PerformanceTools.EstimateAction(() =>
        {
            var max = array.Max();
        });

        var timeList = PerformanceTools.EstimateAction(() =>
        {
            var max = list.Max();
        });

        var timeHashSet = PerformanceTools.EstimateAction(() =>
        {
            var max = hashSet.Max();
        });

        var timeBag = PerformanceTools.EstimateAction(() =>
        {
            var max = bag.Max();
        });

        var timeMem = PerformanceTools.EstimateAction(() =>
        {
            var max = string.Empty;
            for (var i = 0; i < memory.Length; i++)
            {
                var e = memory.Span[i];
                if (string.Compare(e, max, StringComparison.Ordinal) > 0) max = e;
            }
        });

        var timeSpan = PerformanceTools.EstimateAction(() =>
        {
            var span = new Span<string>(arr);

            var max = string.Empty;
            foreach (var e in span)
            {
                if (string.Compare(e, max, StringComparison.Ordinal) > 0) max = e;
            }
        });

        output.Data(timeArray, timeList, timeHashSet, timeBag, timeSpan, timeMem);
    }
    output.Effectiveness();
}



output.Headers("Add");

key = Console.ReadKey();
if (key.Key != ConsoleKey.Escape)
{
    for (var i = 0; i < TestNo; i++)
    {
        collection = PerformanceTools.GetStringCollection(100000);
        array = collection.ToArray();
        list = collection.ToList();
        hashSet = collection.ToHashSet();
        bag = new ConcurrentBag<string>(collection);
        memory = new Memory<string>(array);

        var timeArray = PerformanceTools.EstimateAction(() =>
        {
            array = array.Append("New element").ToArray();
        });

        var timeList = PerformanceTools.EstimateAction(() =>
        {
            list.Add("New element");
        });

        var timeHashSet = PerformanceTools.EstimateAction(() =>
        {
            hashSet.Add("New element");
        });

        var timeBag = PerformanceTools.EstimateAction(() =>
        {
            bag.Add("New element");
        });

        var timeSpan = PerformanceTools.EstimateAction(() =>
        {
            var span = new Span<string>(array.Append("Newer element").ToArray());
        });

        var timeMem = PerformanceTools.EstimateAction(() =>
        {
            array.Append("Newest element");
            memory = new Memory<string>(array);
        });

        output.Data(timeArray, timeList, timeHashSet, timeBag, timeSpan, timeMem);
    }
    output.Effectiveness();
}


output.Headers("Sort");

key = Console.ReadKey();
if (key.Key != ConsoleKey.Escape)
{
    for (var i = 0; i < TestNo; i++)
    {
        collection = PerformanceTools.GetStringCollection(100000);
        array = collection.ToArray();
        list = collection.ToList();
        hashSet = collection.ToHashSet();
        bag = new ConcurrentBag<string>(collection);
        memory = new Memory<string>(array);

        var timeArray = PerformanceTools.EstimateAction(() =>
        {
            array.OrderBy(a => a);
        });

        var timeList = PerformanceTools.EstimateAction(list.Sort);

        var timeHashSet = PerformanceTools.EstimateAction(() =>
        {
            hashSet = hashSet.OrderBy(h => h).ToHashSet();
        });

        var timeBag = PerformanceTools.EstimateAction(() =>
        {
            bag.OrderBy(o => o);
        });

        var timeSpan = PerformanceTools.EstimateAction(() =>
        {
            var span = new Span<string>(array);
            span.Sort();
        });

        var timeMem = PerformanceTools.EstimateAction(() =>
        {
            var memory = new Memory<string>(array);
            memory.Span.Sort();
        });

        output.Data(timeArray, timeList, timeHashSet, timeBag, timeSpan, timeMem);
    }
    output.Effectiveness();
}



var output1 = new Output("Multiple material.", "Single material.");
output1.Headers("IEnumerables");
key = Console.ReadKey();
if (key.Key != ConsoleKey.Escape)
{
    for (var i = 0; i < TestNo; i++)
    {
        var intCollection = PerformanceTools.GetIntCollection();


        var time1 = PerformanceTools.EstimateAction(() =>
        {
            var sum = intCollection.Count();
            var sumOdds = intCollection.Count(e => e % 2 == 1);
        });

        var time2 = PerformanceTools.EstimateAction(() =>
        {
            var intList = intCollection.Select(n => (long)n).ToList();
            var sum = intList.Count;
            var sumOdds = intList.Count(e => e % 2 == 1);
        });

        output1.Data(time1, time2);
    }
    output1.Effectiveness();
}
Console.ReadKey();



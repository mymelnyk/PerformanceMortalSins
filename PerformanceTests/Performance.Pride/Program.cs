using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Performance.Tools;

namespace Performance.Pride
{
    internal class Program
    {
        private const int TestNo = 20;
        private static Output _output = new Output("Array", "List", "HashSet", "ConcurrentBag");


        static void Main(string[] args)
        {
            string[] array;
            List<string> list;
            HashSet<string> hashSet;
            ConcurrentBag<string> bag;
            IEnumerable<string> collection;

            _output.Headers("Materialization");
            
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
                    _output.Data(timeArray, timeList, timeHashSet, timeBag);

                }
                _output.Effectiveness();
            }

            _output.Headers("Calculating max");
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

                    _output.Data(timeArray, timeList, timeHashSet, timeBag);

                }
                _output.Effectiveness();
            }

            _output.Headers("Add element");
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


                    var timeArray = PerformanceTools.EstimateAction(() =>
                    {
                        array.Append("New element");
                    });

                    var timeList = PerformanceTools.EstimateAction(() =>
                    {
                        list.Append("New element");
                    });

                    var timeHashSet = PerformanceTools.EstimateAction(() =>
                    {
                        hashSet.Append("New element");
                    });

                    var timeBag = PerformanceTools.EstimateAction(() =>
                    {
                        bag.Add("New element");
                    });

                    _output.Data(timeArray, timeList, timeHashSet, timeBag);
                }
                _output.Effectiveness();
            }

            _output.Headers("Sort");
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


                    var timeArray = PerformanceTools.EstimateAction(() =>
                    {
                        array.OrderBy(a => a);
                    });

                    var timeList = PerformanceTools.EstimateAction(list.Sort);

                    var timeHashSet = PerformanceTools.EstimateAction(() =>
                    {
                        hashSet.OrderBy(o => o);
                    });

                    var timeBag = PerformanceTools.EstimateAction(() =>
                    {
                        bag.OrderBy(o => o);
                    });

                    _output.Data(timeArray, timeList, timeHashSet, timeBag);
                }
                _output.Effectiveness();

            }

            var output = new Output("Multiple material.", "Single material.");
            output.Headers("IEnumerables");
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

                    output.Data(time1, time2);
                }
                output.Effectiveness();
            }
            Console.ReadKey();
        }
    }
}

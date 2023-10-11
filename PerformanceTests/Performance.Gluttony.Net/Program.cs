using System.Text;
using Performance.Tools;

const int collectionSize = 25000;
const int testNo = 20;
var output = new Output("String", "StringBuilder", "List");

void ConcatenateWithString(int n = collectionSize)
{
    var s = string.Empty;
    for (var i = 0; i < n; i++)
    {
        s += "," + PerformanceTools.GetRandomString();
    }
}

void ConcatenateWithStringBuilder(int n = collectionSize)
{
    var s = new StringBuilder();
    for (var i = 0; i < n; i++)
    {
        s.Append("," + PerformanceTools.GetRandomString());
    }

    _ = s.ToString();
}

void ConcatenateWithEnumerable(int n = collectionSize)
{
    var s = Enumerable.Range(0, n).Select(_ => PerformanceTools.GetRandomString());
    string.Join(",m", s);
}



output.Headers("Concatenation types");
for (var i = 0; i < testNo; i++)
{
    var time1 = PerformanceTools.EstimateAction(() => ConcatenateWithString());
    var time2 = PerformanceTools.EstimateAction(() => ConcatenateWithStringBuilder());
    var time3 = PerformanceTools.EstimateAction(() => ConcatenateWithEnumerable());

    output.Data(time1, time2, time3);
}

output.Effectiveness();
output.WaitKey();

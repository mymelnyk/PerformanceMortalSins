using Performance.Tools;

var _output = new Output("Error codes", "TryCatch");
const int TestNo = 20;
const int IterationNo = 10000000;

void ReturnCodes()
{
    for (var i = 0; i < IterationNo; i++)
    {
        var d = i % 5;
        var result = d == 0 ? 0 : (double)i / d;
    }
}

void TryCatch()
{
    double result = 0;
    for (var i = 0; i < IterationNo; i++)
    {
        try
        {
            result = (double)i / (i % 5);
        }
        catch (Exception e)
        {
            result = 0;
        }
    }
}

_output.Headers("Errors handling");

string Join(string separator, IReadOnlyList<string> strings)
{
    if (strings.Count == 0) return string.Empty;
    int totalSize = 0;
    for (int i = 0; i < strings.Count; i++)
        totalSize += strings[i].Length;

    if (!string.IsNullOrEmpty(separator))
        totalSize += separator.Length * (strings.Count - 1);

    return string.Create(totalSize, (strings, separator), (chars, state) =>
    {
        var offset = 0;
        var separatorSpan = state.separator.AsSpan();
        for (int i = 0; i < state.strings.Count; i++)
        {
            var currentStr = state.strings[i];
            currentStr.AsSpan().CopyTo(chars.Slice(offset));
            offset += currentStr.Length;
            if (!string.IsNullOrEmpty(state.separator) && i < state.strings.Count - 1)
            {
                separatorSpan.CopyTo(chars.Slice(offset));
                offset += state.separator.Length;
            }
        }
    });
}


//for (var i = 0; i < TestNo; i++)
//{
//    var time1 = PerformanceTools.EstimateAction(ReturnCodes);
//    var time2 = PerformanceTools.EstimateAction(TryCatch);
//    _output.Data(time1, time2);
//}
//_output.Effectiveness();
//_output.WaitKey();


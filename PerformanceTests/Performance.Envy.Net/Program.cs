using Performance.Tools;
using System.Data.SqlClient;

var output = new Output("Separate queries", "One query", "Optimized query");
const int testNo = 20;

static void ExecuteQuery(string sql)
{
    var connectionString = "Data Source=(LocalDb)\\OrionIntegrationTests;Initial Catalog=OrionIntegrationTests;MultipleActiveResultSets=true;";

    using (var connection = new SqlConnection(connectionString))
    {
        var command = new SqlCommand(sql, connection);
        connection.Open();
        var reader = command.ExecuteReader();
        try
        {
            while (reader.Read())
            {
                var id = reader["VoipOperationInstanceID"];
                var name = reader["OperationName"];
            }
        }
        finally
        {
            reader.Close();
        }
    }
}


void RequestInMultipleQueries()
{
    for (var i = 1; i < 50; i++)
    {
        var sql = $"SELECT [VoipOperationInstanceID],[OperationName] from [dbo].[VoipOperationInstances] where [VoipOperationInstanceID]={i}";
        ExecuteQuery(sql);
    }
}
void RequestInOneQuery()
{
    var sql = Enumerable.Range(1, 49)
        .Select(id => $"SELECT [VoipOperationInstanceID],[OperationName] from [dbo].[VoipOperationInstances] where [VoipOperationInstanceID]={id}");
    ExecuteQuery(string.Join(" UNION ", sql));
}

void RequestInOneOptimizedQuery()
{
    var sql = $"SELECT [VoipOperationInstanceID],[OperationName] from [dbo].[VoipOperationInstances] where [VoipOperationInstanceID] in ({string.Join(",", Enumerable.Range(1, 49))})";
    ExecuteQuery(sql);
}

output.Headers("Queries");

for (var i = 0; i < testNo; i++)
{
    var time1 = PerformanceTools.EstimateAction(RequestInMultipleQueries);
    var time2 = PerformanceTools.EstimateAction(RequestInOneQuery);
    var time3 = PerformanceTools.EstimateAction(RequestInOneOptimizedQuery);
    output.Data(time1, time2, time3);
}

output.Effectiveness();
output.WaitKey();

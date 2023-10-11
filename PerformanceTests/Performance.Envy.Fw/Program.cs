using System.Data.SqlClient;
using System.Linq;
using Performance.Tools;

namespace Performance.Envy.Fw
{
    internal class Program
    {
        private static Output _output = new Output("Separate queries","One query", "Optimized query");
        private const int TestNo = 20;

        private static void ExecuteQuery(string sql)
        {
            string connectionString = "Data Source=(LocalDb)\\OrionIntegrationTests;Initial Catalog=OrionIntegrationTests;MultipleActiveResultSets=true;";

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
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }


        private static void RequestInMultipleQueries()
        {
            for (var i = 1; i < 50; i++)
            {
                var sql = $"SELECT [VoipOperationInstanceID],[OperationName] from [dbo].[VoipOperationInstances] where [VoipOperationInstanceID]={i}";
                ExecuteQuery(sql);
            }
        }
        private static void RequestInOneQuery()
        {
            var sql = Enumerable.Range(1, 49)
                .Select(id => $"SELECT [VoipOperationInstanceID],[OperationName] from [dbo].[VoipOperationInstances] where [VoipOperationInstanceID]={id}");
            ExecuteQuery(string.Join(" UNION ", sql));
        }

        private static void RequestInOneOptimizedQuery()
        {
            var sql = $"SELECT [VoipOperationInstanceID],[OperationName] from [dbo].[VoipOperationInstances] where [VoipOperationInstanceID] in ({string.Join(",",Enumerable.Range(1, 49))})";
            ExecuteQuery(sql);
        }

        static void Main(string[] args)
        {
            _output.Headers("Queries");

            for (var i = 0; i < TestNo; i++)
            {
                var time1 = PerformanceTools.EstimateAction(RequestInMultipleQueries);
                var time2 = PerformanceTools.EstimateAction(RequestInOneQuery);
                var time3 = PerformanceTools.EstimateAction(RequestInOneOptimizedQuery);
                _output.Data(time1, time2, time3);
            }

            _output.Effectiveness();
            _output.WaitKey();
        }
    }
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolarisAICore.Properties;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.RollingFileAlternate;

namespace PolarisAICore {
	public class PolarisAICore {

        static readonly PolarisAIDatabaseConnection _database = new PolarisAIDatabaseConnection(
            Resources.ResourceManager.GetString("DBsource"),
            Resources.ResourceManager.GetString("DBname"),
            Resources.ResourceManager.GetString("DBlogin"),
            Resources.ResourceManager.GetString("DBpassword"));

        static void Main() {

            List<string> queries = new List <string>();

            ILogger logger = new LoggerConfiguration()
                .WriteTo.File("../../../logs.txt")
                .WriteTo.Seq("http://localhost:8081")
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
                })
                .WriteTo.RollingFileAlternate(".\\logs",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            Log.Logger = logger;


            while (true) {
                Log.Information("waiting for a request");
                Console.WriteLine("Enter a test query:");
                var query = Console.ReadLine();
                queries.Add(query);
                Log.Information("the number of times this query '{query}' has been asked for is : {count}",query,countQuery(queries, query));
                Console.WriteLine(CognizeDebug(query));
            }

            Log.CloseAndFlush();
        }

        public static JObject Cognize(String query){

            Utterance utterance = new Utterance(
                IntentClassificatorSingleton.Instance.Cognize(query));

            utterance.Response = Response.ResponseController.SetResponse(utterance);

            _database.InsertRequestDetails(utterance);

            return utterance.GetResponse();
        }

        public static String CognizeDebug(String query) {
            var start = DateTime.Now;
            Log.Information("query is {query}", query);
            Utterance utterance = new Utterance(
                IntentClassificatorSingleton.Instance.Cognize(query));
            Log.Information("the utternece is a questions? : {utterance}",utterance.IsQuestion.ToString());
            utterance.Response = Response.ResponseController.SetResponse(utterance);
            Log.Information("Utterence responce is {response}", utterance.Response);
            var end = DateTime.Now;
            Log.Information("Time to get a responce from starlight is {time}", (end - start));
            return utterance.GetDebugLog();
        }

        public static int countQuery(List<string> queries, string query)
        {
            int count = 0;
            for( int x=0; x<queries.Count; x++)
            {
                if (queries[x] == query) { 
                count++;
                }
            }
            return count;
        }
	}
}

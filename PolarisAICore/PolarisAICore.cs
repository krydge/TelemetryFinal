using System;
using Newtonsoft.Json.Linq;
using PolarisAICore.Properties;
using Serilog;

namespace PolarisAICore {
	public class PolarisAICore {

        static readonly PolarisAIDatabaseConnection _database = new PolarisAIDatabaseConnection(
            Resources.ResourceManager.GetString("DBsource"),
            Resources.ResourceManager.GetString("DBname"),
            Resources.ResourceManager.GetString("DBlogin"),
            Resources.ResourceManager.GetString("DBpassword"));

        static void Main() {
            ILogger logger = new LoggerConfiguration()
                .WriteTo.File("../../../logs.txt")
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger = logger;
            while (true) {
                Log.Information("waiting for a request");
                Console.WriteLine("Enter a test query:");
                Console.WriteLine(CognizeDebug(Console.ReadLine()));
            }
        }

        public static JObject Cognize(String query){
            Log.Information("query is {query}", query);
            Utterance utterance = new Utterance(
                IntentClassificatorSingleton.Instance.Cognize(query));
            utterance.Response = Response.ResponseController.SetResponse(utterance);

            _database.InsertRequestDetails(utterance);

            return utterance.GetResponse();
        }

        public static String CognizeDebug(String query) {
            Log.Information("query is {query}", query);
            Utterance utterance = new Utterance(
                IntentClassificatorSingleton.Instance.Cognize(query));
            utterance.Response = Response.ResponseController.SetResponse(utterance);

            return utterance.GetDebugLog();
        }
	}
}

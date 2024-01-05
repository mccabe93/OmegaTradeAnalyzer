using Newtonsoft.Json;
using OmegaPostProcessor.Configuration;
using OmegaPostProcessor.Models;
using System.Text.Json.Nodes;

namespace OmegaPostProcessor
{
    internal class Program
    {
        private static readonly string _defaultLog = "./Data/log.csv";
        private static readonly string _defaultConfig = "./Configuration/analysis.json";
        private static readonly string _defaultOutput = "./Data/analysis.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Starting OmegaPostProcessor . . .");
            Console.Write("\rLoading config . . .");
            var config = JsonConvert.DeserializeObject<AnalysisConfiguration>(File.ReadAllText(_defaultConfig)) ?? throw new FileLoadException("Could not load the configuration file.");
            Console.Write("\rStarting analysis . . .");
            string logToAnalyze = "";
            if (args.Length == 0)
            {
                logToAnalyze = _defaultLog;
            }
            else
            {
                logToAnalyze = args[0];
            }
            Console.Write($"\rLoading {logToAnalyze} . . .");
            var log = FileIO.ReadCsvFile<TradeLog>(logToAnalyze);
            Console.Write($"\rAnalyzing {logToAnalyze} . . .");
            var analysis = Analyzer.Analyze(log);
            Console.Write($"\rWriting analysis with columns: {string.Join(',', config.IncludeColumns)} to {_defaultOutput}");
            FileIO.WriteCsvFile<AnalyzerLog>(analysis, _defaultOutput, config.IncludeColumns);
        }
    }
}

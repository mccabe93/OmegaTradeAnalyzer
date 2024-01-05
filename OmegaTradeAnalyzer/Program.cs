using OmegaPostProcessor;

namespace OmegaTradeAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Load sample data
            var data = FileIO.ReadCsvFile();
            var sampleData = new OmegaAnalyzer.ModelInput()
            {
                MaxWidth = 87.35F,
                Premium = -5015F,
                OpeningVIX = 20.43F,
                OpeningShortLongRatio = 0.27F,
            };

            //Load model and predict output
            var result = OmegaAnalyzer.Predict(sampleData);

        }
    }
}

using OmegaPostProcessor.Models;
using System.Text.RegularExpressions;

namespace OmegaPostProcessor
{
    public class Parser
    {
        public static List<OptionInfo> ParseSpread(string spread)
        {
            var data = new List<OptionInfo>();
            var legs = spread.Split('|');
            foreach (var leg in legs)
            {
                OptionInfo optionInfo = ParseOptionSymbol(leg);
                data.Add(optionInfo);
            }
            return data;
        }

        public static OptionInfo ParseOptionSymbol(string optionSymbol)
        {
            // Define the regular expression pattern to extract information
            string pattern = @"(\w+)\s+(\w+\s+\d+)\s+(\d+)\s+(\w+)\s+(\w+)\s+(\d+\.\d+)";

            // Match the pattern using Regex
            Match match = Regex.Match(optionSymbol, pattern);

            // Check if the match was successful
            if (match.Success)
            {
                // Extract information from groups
                string ticker = match.Groups[1].Value;
                string date = match.Groups[2].Value;
                int strikePrice = int.Parse(match.Groups[3].Value);
                string optionType = match.Groups[4].Value;
                string action = match.Groups[5].Value;
                double price = double.Parse(match.Groups[6].Value);

                // Create and return OptionInfo object
                return new OptionInfo(ticker, date, strikePrice, optionType, action, price);
            }
            else
            {
                throw new ArgumentException("Invalid option symbol format");
            }
        }
    }
}

namespace OmegaPostProcessor.Models
{
    public class OptionInfo
    {
        public string Ticker { get; }
        public string Date { get; }
        public int StrikePrice { get; }
        public string OptionType { get; }
        public string Action { get; }
        public double Price { get; }

        public bool Long => Action == "BTO";

        public OptionInfo(string ticker, string date, int strikePrice, string optionType, string action, double price)
        {
            Ticker = ticker;
            Date = date;
            StrikePrice = strikePrice;
            OptionType = optionType;
            Action = action;
            Price = price;
        }
    }
}

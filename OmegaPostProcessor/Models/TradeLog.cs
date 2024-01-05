using CsvHelper.Configuration.Attributes;

namespace OmegaPostProcessor.Models
{
    public class TradeLog
    {
        [Name("Date Opened")]
        public DateTime DateOpened { get; set; }

        [Name("Time Opened")]
        public string TimeOpened { get; set; }

        [Name("Opening Price")]
        public decimal OpeningPrice { get; set; }

        [Name("Legs")]
        public string Legs { get; set; }

        [Name("Premium")]
        public int Premium { get; set; }

        [Name("Closing Price")]
        public decimal ClosingPrice { get; set; }

        [Name("Date Closed")]
        public DateTime? DateClosed { get; set; }

        [Name("Time Closed")]
        public string TimeClosed { get; set; }

        [Name("Avg. Closing Cost")]
        public int AvgClosingCost { get; set; }

        [Name("Reason For Close")]
        public string ReasonForClose { get; set; }

        [Name("P/L")]
        public decimal PL { get; set; }

        [Name("No. of Contracts")]
        public int NumberOfContracts { get; set; }

        [Name("Funds at Close")]
        public decimal FundsAtClose { get; set; }

        [Name("Margin Req.")]
        public int MarginRequirement { get; set; }

        [Name("Strategy")]
        public string Strategy { get; set; }

        [Name("Opening Commissions + Fees")]
        public decimal OpeningCommissionsFees { get; set; }

        [Name("Opening Short/Long Ratio")]
        public decimal OpeningShortLongRatio { get; set; }

        [Name("Closing Short/Long Ratio")]
        public decimal ClosingShortLongRatio { get; set; }

        [Name("Opening VIX")]
        public decimal OpeningVIX { get; set; }

        [Name("Closing VIX")]
        public decimal ClosingVIX { get; set; }

        [Name("Gap")]
        public decimal Gap { get; set; }

        [Name("Movement")]
        public decimal Movement { get; set; }

        [Name("Max Profit")]
        public decimal MaxProfit { get; set; }

        [Name("Max Loss")]
        public decimal MaxLoss { get; set; }
    }
}

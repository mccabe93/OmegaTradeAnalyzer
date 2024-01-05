using CsvHelper.Configuration.Attributes;

namespace OmegaPostProcessor.Models
{
    public class AnalyzerLog : TradeLog
    {
        [Name("Risk")]
        public double Risk { get; set; }

        [Name("Profitable")]
        public int Profitable { get; set; }

        [Name("MostOTM")]
        public double MostOTM { get; set; }

        [Name("LeastOTM")]
        public double LeastOTM { get; set; }

        [Name("AverageOTM")]
        public double AverageOTM { get; set; }

        public void Copy(TradeLog other)
        {
            // Copy values from 'other' to the current instance
            DateOpened = other.DateOpened;
            TimeOpened = other.TimeOpened;
            OpeningPrice = other.OpeningPrice;
            Legs = other.Legs;
            Premium = other.Premium;
            ClosingPrice = other.ClosingPrice;
            DateClosed = other.DateClosed;
            TimeClosed = other.TimeClosed;
            AvgClosingCost = other.AvgClosingCost;
            ReasonForClose = other.ReasonForClose;
            PL = other.PL;
            NumberOfContracts = other.NumberOfContracts;
            FundsAtClose = other.FundsAtClose;
            MarginRequirement = other.MarginRequirement;
            Strategy = other.Strategy;
            OpeningCommissionsFees = other.OpeningCommissionsFees;
            OpeningShortLongRatio = other.OpeningShortLongRatio;
            ClosingShortLongRatio = other.ClosingShortLongRatio;
            OpeningVIX = other.OpeningVIX;
            ClosingVIX = other.ClosingVIX;
            Gap = other.Gap;
            Movement = other.Movement;
            MaxProfit = other.MaxProfit;
            MaxLoss = other.MaxLoss;
        }
    }
}

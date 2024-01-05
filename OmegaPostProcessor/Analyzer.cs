using OmegaPostProcessor.Models;

namespace OmegaPostProcessor
{
    public class Analyzer
    {
        public static List<AnalyzerLog> Analyze(List<TradeLog> tradeLogs)
        {
            var analyzerLogs = new List<AnalyzerLog>();
            foreach(var tradeLog in tradeLogs)
            {
                var options = Parser.ParseSpread(tradeLog.Legs);
                double risk = CalculateRisk(options);
                var moneyness = GetMoneyness(tradeLog, options);
                var analyzerLog = new AnalyzerLog();
                analyzerLog.Copy(tradeLog);
                analyzerLog.Profitable = tradeLog.PL > 0 ? 1 : 0;
                analyzerLog.Risk = risk;
                analyzerLog.MostOTM = moneyness.mostOtm;
                analyzerLog.LeastOTM = moneyness.leastOtm;
                analyzerLog.AverageOTM = moneyness.avgOtm;
                analyzerLogs.Add(analyzerLog);
            }
            return analyzerLogs;
        }

        public static (double mostOtm, double leastOtm, double avgOtm) GetMoneyness(TradeLog trade, List<OptionInfo> tradeLegs)
        {
            double mostOtm = double.MinValue;
            double leastOtm = double.MaxValue;
            double avgOtm = 0d;

            foreach(var leg in tradeLegs)
            {
                var otm = (double)Math.Abs(trade.OpeningPrice - leg.StrikePrice);
                if(otm > mostOtm)
                {
                    mostOtm = otm;
                }
                if(otm < leastOtm)
                {
                    leastOtm = otm;
                }
                avgOtm += otm;
            }
            avgOtm /= tradeLegs.Count;

            return (mostOtm, leastOtm, avgOtm);
        }

        public static double CalculateRisk(List<OptionInfo> options)
        {
            double maxRisk = 0;
            double premium = 0;
            double maxCallWidth = 0;
            double maxPutWidth = 0;

            var shortCalls = new List<OptionInfo>();
            var longCalls = new List<OptionInfo>();

            var shortPuts = new List<OptionInfo>();
            var longPuts = new List<OptionInfo>();

            foreach (var option in options)
            {
                premium += option.Price * (option.Long ? 1d : -1d);
                if (option.OptionType == "C")
                {
                    if (option.Long)
                    {
                        longCalls.Add(option);
                    }
                    else
                    {
                        shortCalls.Add(option);
                    }
                }
                else
                {
                    if (option.Long)
                    {
                        longPuts.Add(option);
                    }
                    else
                    {
                        shortPuts.Add(option);
                    }
                }
            }
            // Any time short calls outnumber long calls you face unlimited risk.
            // Any time a short call has a longer expriation you face unlimited risk.
            if (shortCalls.Count > longCalls.Count)
            {
                return double.MaxValue;
            }

            foreach (var shortCall in shortCalls)
            {
                foreach (var longCall in longCalls)
                {
                    // The greatest risk is the most ITM short call to the most OTM long call
                    // +C100, -C80, -C50, +C20 => [+C100, -C50]
                    var width = shortCall.StrikePrice - longCall.StrikePrice;
                    if (width < maxCallWidth)
                    {
                        maxCallWidth = width;
                    }
                }
            }

            foreach (var shortPut in shortPuts)
            {
                foreach (var longPut in longPuts)
                {
                    var width = shortPut.StrikePrice - longPut.StrikePrice;
                    if (width > maxPutWidth)
                    {
                        maxPutWidth = width;
                    }
                }
            }

            if (shortPuts.Count > longPuts.Count)
            {
                // When there's a combination of short puts, the lowest strike risks 100 * strike loss.
                var lowestStrikeShortPutRisk = shortPuts.Min(o => o.StrikePrice);
                if (lowestStrikeShortPutRisk > maxPutWidth)
                {
                    maxPutWidth = lowestStrikeShortPutRisk;
                }
            }

            maxRisk = Math.Max(Math.Abs(maxCallWidth), maxPutWidth);

            return (maxRisk + premium) * 100;
        }
    }
}

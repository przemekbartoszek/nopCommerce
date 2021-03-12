using System;

namespace Nop.Services.Prices.CurrencyExchange
{
    public class CurrencyExchangeInfo
    {
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
    }
}

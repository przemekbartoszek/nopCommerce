using System.Collections.Generic;

namespace Nop.Services.Prices.CurrencyExchange
{
    public class CurrencyExchangeData
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public List<CurrencyExchangeInfo> Rates { get; set; }
    }
}

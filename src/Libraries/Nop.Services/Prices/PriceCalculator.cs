using System;
using Nop.Services.Prices.CurrencyExchange;

namespace Nop.Services.Prices
{
    public class PriceCalculator
    {
        private readonly decimal _exchangeRateUsdPln;
        private readonly decimal _exchangeRateEurPln;

        public PriceCalculator(decimal exchangeRateUsdPln, decimal exchangeRateEurPln)
        {
            _exchangeRateUsdPln = exchangeRateUsdPln;
            _exchangeRateEurPln = exchangeRateEurPln;
        }

        private decimal GetExchangeRate(string currency) => 
            currency switch
            {
                Currencies.EUR => _exchangeRateEurPln,
                Currencies.USD => _exchangeRateUsdPln,
                _ => 1
            };

        public decimal CalculateNetPrice(
            decimal supplierPriceNet, 
            string supplierPriceCurrency, 
            decimal? maxPriceNetPln, 
            decimal margin)
        {
            var price = supplierPriceNet * GetExchangeRate(supplierPriceCurrency);
            price += (price * margin / 100);
            if (price < maxPriceNetPln)
            {
                price = maxPriceNetPln.Value - 1;
            }

            return Math.Round(price, 2, MidpointRounding.AwayFromZero);
        }

        public decimal CalculateGrossPrice(decimal netPrice) => Math.Round(netPrice * 1.23M, 2, MidpointRounding.AwayFromZero);
    }
}

using System;
using System.Linq;
using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Services.Prices.CurrencyExchange;

namespace Nop.Services.Prices
{
    public class AutoPriceCalculationService : IAutoPriceCalculationService
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;
        private readonly IRepository<Product> _productRepository;

        public AutoPriceCalculationService(IRepository<Product> productRepository, ICurrencyExchangeService currencyExchangeService)
        {
            _productRepository = productRepository;
            _currencyExchangeService = currencyExchangeService;
        }

        public void CalculatePrices()
        {
            const decimal defaultMargin = 30M;
            var usdPln = _currencyExchangeService.GetCurrentExchangeForUsd();
            var eurPln = _currencyExchangeService.GetCurrentExchangeForEur();
            var products = _productRepository.Table.Where(x => x.AutoCalculatePrice).ToList();
            var calculator = new PriceCalculator(usdPln.Rates.First().Ask, eurPln.Rates.First().Ask);
            foreach (var product in products)
            {
                if (!product.SupplierPrice.HasValue) continue;

                product.Price = calculator.CalculateNetPrice(product.SupplierPrice.Value,
                    product.SupplierPriceCurrency, product.MaxPrice, product.Margin ?? defaultMargin);

                product.GrossPrice = calculator.CalculateGrossPrice(product.Price);

                product.LastPriceRefresh = DateTime.Now;
                _productRepository.Update(product);
            }
        }

    }
}

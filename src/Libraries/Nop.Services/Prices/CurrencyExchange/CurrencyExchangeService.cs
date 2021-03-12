using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Core.Http;

namespace Nop.Services.Prices.CurrencyExchange
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Dictionary<string, CurrencyExchangeData> _rates = new Dictionary<string, CurrencyExchangeData>();

        public CurrencyExchangeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public CurrencyExchangeData GetCurrentExchangeFor(string code)
        {
            var httpClient = _httpClientFactory.CreateClient(NopHttpDefaults.DefaultHttpClient);
            var res = httpClient.GetAsync($"http://api.nbp.pl/api/exchangerates/rates/c/{code}/today/?format=json").Result;
            res.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<CurrencyExchangeData>(res.Content.ReadAsStringAsync().Result);
        }

        public CurrencyExchangeData GetCurrentExchangeForEur()
        {
            if (_rates.ContainsKey(Currencies.EUR))
            {
                return _rates[Currencies.EUR];
            }
            var rate = GetCurrentExchangeFor(Currencies.EUR);
            _rates.Add(Currencies.EUR, rate);
            return rate;
        }

        public CurrencyExchangeData GetCurrentExchangeForUsd()
        {
            if (_rates.ContainsKey(Currencies.USD))
            {
                return _rates[Currencies.USD];
            }
            var rate = GetCurrentExchangeFor(Currencies.USD);
            _rates.Add(Currencies.USD, rate);
            return rate;
        }

    }
}

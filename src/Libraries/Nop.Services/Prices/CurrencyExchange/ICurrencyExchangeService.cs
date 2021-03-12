using System.Threading.Tasks;

namespace Nop.Services.Prices.CurrencyExchange
{
    public interface ICurrencyExchangeService
    {
        CurrencyExchangeData GetCurrentExchangeFor(string code);
        CurrencyExchangeData GetCurrentExchangeForEur();
        CurrencyExchangeData GetCurrentExchangeForUsd();
    }
}
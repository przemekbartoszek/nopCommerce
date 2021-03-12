using Nop.Services.Tasks;

namespace Nop.Services.Prices
{
    public class PriceCalculationTask : IScheduleTask
    {
        private readonly IAutoPriceCalculationService _priceCalculationService;

        public PriceCalculationTask(IAutoPriceCalculationService priceCalculationService)
        {
            _priceCalculationService = priceCalculationService;
        }
        public void Execute()
        {
            _priceCalculationService.CalculatePrices();
        }
    }
}

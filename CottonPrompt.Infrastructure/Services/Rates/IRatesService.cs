using CottonPrompt.Infrastructure.Models.Rates;

namespace CottonPrompt.Infrastructure.Services.Rates
{
    public interface IRatesService
    {
        Task<RatesModel> GetAsync();

        Task UpdateAsync(decimal qualityControlRate, decimal changeRequestRate, Guid updatedBy);
    }
}

using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.Rates;
using Microsoft.EntityFrameworkCore;
using System;
namespace CottonPrompt.Infrastructure.Services.Rates
{
    public class RatesService(CottonPromptContext dbContext) : IRatesService
    {
        public async Task<RatesModel> GetAsync()
        {
			try
			{
				var rates = await dbContext.Rates.FirstAsync();
				var result = rates.AsModel();
				return result;
			}
			catch (Exception)
			{
				throw;
			}
        }

        public async Task UpdateAsync(decimal qualityControlRate, decimal changeRequestRate, Guid updatedBy)
        {
			try
			{
                await dbContext.Rates
                    .Where(r => true)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(r => r.QualityControlRate, qualityControlRate)
                        .SetProperty(r => r.ChangeRequestRate, changeRequestRate)
                        .SetProperty(r => r.UpdatedBy, updatedBy)
                        .SetProperty(r => r.UpdatedOn, DateTime.UtcNow));
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}

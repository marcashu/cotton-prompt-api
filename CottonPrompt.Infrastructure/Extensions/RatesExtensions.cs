using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Rates;

namespace CottonPrompt.Infrastructure.Extensions
{
    public static class RatesExtensions
    {
        public static RatesModel AsModel(this Rate entity)
        {
            var result = new RatesModel(entity.QualityControlRate, entity.ChangeRequestRate);
            return result;
        }
    }
}

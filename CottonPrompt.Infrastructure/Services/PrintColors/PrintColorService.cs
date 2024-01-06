using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.PrintColors;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.PrintColors
{
    public class PrintColorService(CottonPromptContext dbContext) : IPrintColorService
    {
        public async Task CreateAsync(string value, Guid userId)
        {
            try
            {
                var sortOrder = await dbContext.OrderPrintColors
                    .OrderByDescending(db => db.SortOrder)
                    .Select(db => db.SortOrder + 1)
                    .FirstOrDefaultAsync();

                var designBracket = new OrderPrintColor
                {
                    Value = value,
                    CreatedBy = userId,
                    SortOrder = sortOrder,
                    Active = true,
                };

                await dbContext.OrderPrintColors.AddAsync(designBracket);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await dbContext.OrderPrintColors.Where(db => db.Id == id).ExecuteDeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DisableAsync(int id, Guid userId)
        {
            try
            {
                await dbContext.OrderPrintColors
                    .Where(db => db.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(db => db.Active, false)
                        .SetProperty(db => db.UpdatedBy, userId)
                        .SetProperty(db => db.UpdatedOn, DateTime.UtcNow));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EnableAsync(int id, Guid userId)
        {
            try
            {
                await dbContext.OrderPrintColors
                    .Where(db => db.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(db => db.Active, true)
                        .SetProperty(db => db.UpdatedBy, userId)
                        .SetProperty(db => db.UpdatedOn, DateTime.UtcNow));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<PrintColor>> GetAsync(bool hasActiveFilter, bool active)
        {
            try
            {
                var designBrackets = await dbContext.OrderPrintColors
                    .Where(db => !hasActiveFilter || hasActiveFilter && db.Active == active)
                    .OrderBy(db => db.SortOrder)
                    .ToListAsync();
                var result = designBrackets.AsModel();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetPrintColorOrdersCountModel> GetOrdersCountAsync(int id)
        {
            try
            {
                var result = new GetPrintColorOrdersCountModel
                {
                    Count = await dbContext.Orders.CountAsync(o => o.PrintColorId == id)
                };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SwapAsync(int id1, int id2, Guid userId)
        {
            try
            {
                var designBracket1 = await dbContext.OrderPrintColors.FindAsync(id1);
                var designBracket2 = await dbContext.OrderPrintColors.FindAsync(id2);

                if (designBracket1 is null || designBracket2 is null) return;

                (designBracket2.SortOrder, designBracket1.SortOrder) = (designBracket1.SortOrder, designBracket2.SortOrder);
                designBracket1.UpdatedBy = userId;
                designBracket1.UpdatedOn = DateTime.UtcNow;
                designBracket2.UpdatedBy = userId;
                designBracket2.UpdatedOn = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAsync(int id, string value, Guid userId)
        {
            try
            {
                await dbContext.OrderPrintColors
                    .Where(db => db.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(db => db.Value, value)
                        .SetProperty(db => db.UpdatedBy, userId)
                        .SetProperty(db => db.UpdatedOn, DateTime.UtcNow));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace CottonPrompt.Infrastructure.Services.Users
{
    public class UserService(CottonPromptContext dbContext, IServiceProvider serviceProvider) : IUserService
    {
        public async Task<CanDoModel> CanUpdateRoleAsync(Guid id, string role)
        {
			try
			{
				if (role != "Artist")
				{
					return new CanDoModel(true, string.Empty);
				}

				var count = await dbContext.Orders.CountAsync(o => o.CheckerId == id && o.CheckerStatus != OrderStatuses.Approved);

                if (count > 0)
                {
					return new CanDoModel(false, "Can't update role to Artist because the user still has orders for checking.");
                }
				else
                {
                    return new CanDoModel(true, string.Empty);
                }
            }
			catch (Exception)
			{
				throw;
			}
        }

        public async Task<IEnumerable<GetUsersModel>> GetUnregisteredAsync()
        {
			try
            {
                var result = Enumerable.Empty<GetUsersModel>();
                
				var graphClient = serviceProvider.GetRequiredService<GraphServiceClient>();
				var msUsersResponse = await graphClient.Users.GetAsync();

				if (msUsersResponse is null || msUsersResponse.Value is null) return result;

				var msUsers = msUsersResponse.Value;
                var dbUserIds = await dbContext.Users.OrderBy(u => u.Name).Select(u => u.Id.ToString().ToLower()).ToListAsync();

				result = msUsers.Where(u => !dbUserIds.Contains(u.Id?.ToLower() ?? string.Empty)).AsModel();

                return result;
			}
			catch (Exception)
			{
				throw;
			}
        }

        public async Task<IEnumerable<GetUsersModel>> GetRegisteredAsync()
        {
			try
			{
				var users = await dbContext.Users.OrderBy(u => u.Name).ToListAsync();
				var result = users.AsModel();
				return result;
			}
			catch (Exception)
			{
				throw;
			}
        }

        public async Task<GetUsersModel> LoginAsync(Guid id, string name, string email)
        {
			try
			{
				var user = await dbContext.Users.FindAsync(id);

				if (user == null)
				{
					return new GetUsersModel(id, name, email, string.Empty);
                }
				else
				{
					user.LastLoggedOn = DateTime.UtcNow;
					user.UpdatedOn = DateTime.UtcNow;
					user.UpdatedBy = id;
                    await dbContext.SaveChangesAsync();

                    var result = user.AsModel();
                    return result;
                }
			}
			catch (Exception)
			{
				throw;
			}
        }

        public async Task UpdateRoleAsync(Guid id, string role, Guid updatedBy)
        {
			try
			{
				await dbContext.Users.Where(u => u.Id == id).ExecuteUpdateAsync(setters => setters
					.SetProperty(u => u.Role, role)
					.SetProperty(u => u.UpdatedOn, DateTime.UtcNow)
					.SetProperty(u => u.UpdatedBy, updatedBy));
			}
			catch (Exception)
			{
				throw;
			}
        }

        public async Task AddAsync(Guid id, string name, string email, string? role, Guid createdBy)
        {
			try
			{
				var user = new User
				{
					Id = id,
					Name = name,
					Email = email,
					Role = role,
					CreatedBy = createdBy
				};

				await dbContext.Users.AddAsync(user);
				await dbContext.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}

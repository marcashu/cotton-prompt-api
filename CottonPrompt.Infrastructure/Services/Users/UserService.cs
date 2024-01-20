using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Users
{
    public class UserService(CottonPromptContext dbContext) : IUserService
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

        public async Task<IEnumerable<GetUsersModel>> GetAsync()
        {
			try
            {
				var result = Enumerable.Empty<GetUsersModel>();
                var dbUsers = await dbContext.Users.OrderBy(u => u.Name).ToListAsync();

				//if (registered)
				//{
				//	result = dbUsers.AsModel();
				//}
				//else
				//{
    //                var msUsersResponse = await graphServiceClient.Users.GetAsync();

    //                if (msUsersResponse?.Value is null) return result;

				//	var registeredUserIds = dbUsers.Select(u => u.Id.ToString()).ToList();
				//	var msUsers = msUsersResponse.Value.Where(u => !registeredUserIds.Contains(u.Id ?? string.Empty)).ToList();
				//	result = msUsers.AsModel();
    //            }

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
					var newUser = new User
					{
						Id = id,
						Name = name,
						Email = email,
						LastLoggedOn = DateTime.UtcNow,
						CreatedBy = id,
					};

					await dbContext.Users.AddAsync(newUser);
                    await dbContext.SaveChangesAsync();

                    var result = newUser.AsModel();
                    return result;
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
    }
}

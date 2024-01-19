using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;

namespace CottonPrompt.Infrastructure.Services.Users
{
    public class UserService(CottonPromptContext dbContext, GraphServiceClient graphServiceClient) : IUserService
    {
        public async Task<IEnumerable<GetUsersModel>> GetAsync()
        {
			try
            {
                var result = new List<GetUsersModel>();
                var msUsers = await graphServiceClient.Users.GetAsync();
				
				if (msUsers?.Value is null) return result;

				var dbUsers = await dbContext.Users.ToListAsync();

				foreach (var msUser in msUsers.Value) 
				{
					var role = dbUsers.SingleOrDefault(du => du.Id == Guid.Parse(msUser.Id ?? string.Empty))?.Role;
					var user = msUser.AsModel(role);
					result.Add(user);
				}

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
    }
}

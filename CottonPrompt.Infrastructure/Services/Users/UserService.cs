using CottonPrompt.Infrastructure.Entities;

namespace CottonPrompt.Infrastructure.Services.Users
{
    public class UserService(CottonPromptContext dbContext) : IUserService
    {
        public async Task LoginAsync(Guid id, string name, string email)
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
				}
				else
				{
					user.LastLoggedOn = DateTime.UtcNow;
					user.UpdatedOn = DateTime.UtcNow;
					user.UpdatedBy = id;
				}

				await dbContext.SaveChangesAsync();
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}

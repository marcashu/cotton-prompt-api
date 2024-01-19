using CottonPrompt.Infrastructure.Models.Users;

namespace CottonPrompt.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<GetUsersModel> LoginAsync(Guid id, string name, string email);

        Task<IEnumerable<GetUsersModel>> GetAsync();
    }
}

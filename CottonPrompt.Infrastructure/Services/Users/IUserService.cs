using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Users;

namespace CottonPrompt.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<GetUsersModel> LoginAsync(Guid id, string name, string email);

        Task<IEnumerable<GetUsersModel>> GetRegisteredAsync();

        Task<IEnumerable<GetUsersModel>> GetAsync();

        Task UpdateRoleAsync(Guid id, string role, Guid updatedBy);

        Task<CanDoModel> CanUpdateRoleAsync(Guid id, string role);
    }
}

using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Users;

namespace CottonPrompt.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task<GetUsersModel> LoginAsync(Guid id, string name, string email);

        Task AddAsync(Guid id, string name, string email, string? role, Guid createdBy);

        Task<IEnumerable<GetUsersModel>> GetRegisteredAsync();

        Task<IEnumerable<GetUsersModel>> GetUnregisteredAsync();

        Task UpdateRoleAsync(Guid id, string role, Guid updatedBy);

        Task<CanDoModel> CanUpdateRoleAsync(Guid id, string role);
    }
}

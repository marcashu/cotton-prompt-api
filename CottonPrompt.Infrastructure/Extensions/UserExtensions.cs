using GraphUser = Microsoft.Graph.Models.User;
using UserEntity = CottonPrompt.Infrastructure.Entities.User;
using CottonPrompt.Infrastructure.Models.Users;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class UserExtensions
    {
        internal static GetUsersModel AsModel(this GraphUser graphUser, string? role)
        {
            var result = new GetUsersModel(Guid.Parse(graphUser.Id ?? string.Empty), graphUser.DisplayName ?? string.Empty, graphUser.UserPrincipalName ?? string.Empty, role);
            return result;
        }
        
        internal static GetUsersModel AsModel(this UserEntity entity)
        {
            var result = new GetUsersModel(entity.Id, entity.Name, entity.Email, entity.Role);
            return result;
        }
    }
}

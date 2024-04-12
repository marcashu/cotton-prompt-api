using CottonPrompt.Infrastructure.Models.Users;

namespace CottonPrompt.Infrastructure.Models.UserGroups
{
    public record GetUserGroupModel(
        int Id, 
        string Name, 
        IEnumerable<GetUsersModel> Users
    );
}

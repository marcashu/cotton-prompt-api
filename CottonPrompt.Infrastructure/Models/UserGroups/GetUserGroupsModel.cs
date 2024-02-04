namespace CottonPrompt.Infrastructure.Models.UserGroups
{
    public record GetUserGroupsModel(
        int Id,
        string Name,
        int MembersCount
    );
}

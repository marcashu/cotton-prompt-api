using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.UserGroups;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class UserGroupExtensions
    {
        internal static GetUserGroupsModel AsGetUserGroupsModel(this UserGroup entity)
        {
            var result = new GetUserGroupsModel(entity.Id, entity.Name, entity.UserGroupUsers.Count());
            return result;
        }

        internal static IEnumerable<GetUserGroupsModel> AsModel(this IEnumerable<UserGroup> entities)
        {
            var result = entities.Select(AsGetUserGroupsModel);
            return result;
        }

        internal static GetUserGroupModel AsGetUserGroupModel(this UserGroup entity)
        {
            var result = new GetUserGroupModel(entity.Id, entity.Name, entity.UserGroupUsers.Select(ugu => ugu.User.AsModel()));
            return result;
        }
    }
}

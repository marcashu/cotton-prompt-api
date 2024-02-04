using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.UserGroups;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.UserGroups
{
    public class UserGroupService(CottonPromptContext dbContext) : IUserGroupService
    {
        public async Task CreateAsync(string name, IEnumerable<Guid> userIds, Guid createdBy)
        {
            try
            {
                var userGroup = new UserGroup
                {
                    Name = name,
                    CreatedBy = createdBy,
                };

                await dbContext.UserGroups.AddAsync(userGroup);
                await dbContext.SaveChangesAsync();

                if (!userIds.Any()) return;

                foreach (var userId in userIds)
                {
                    var userGroupUser = new UserGroupUser
                    {
                        UserGroupId = userGroup.Id,
                        UserId = userId,
                        CreatedBy = createdBy,
                    };

                    await dbContext.UserGroupUsers.AddAsync(userGroupUser);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetUserGroupsModel>> GetAsync()
        {
            try
            {
                var userGroups = await dbContext.UserGroups.Include(ug => ug.UserGroupUsers).ToListAsync();
                var result = userGroups.AsModel();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetUserGroupModel> GetByIdAsync(int id)
        {
            try
            {
                var userGroup = await dbContext.UserGroups
                    .Include(ug => ug.UserGroupUsers)
                    .ThenInclude(ugu => ugu.User)
                    .ThenInclude(u => u.UserRoles.Where(ur => ur.Active))
                    .SingleAsync(ug => ug.Id == id);
                var result = userGroup.AsGetUserGroupModel();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(int id, string name, IEnumerable<Guid> userIds, Guid updatedBy)
        {
            try
            {
                var userGroup = await dbContext.UserGroups.Include(ug => ug.UserGroupUsers).SingleOrDefaultAsync(ug => ug.Id == id);

                if (userGroup is null) return;

                userGroup.Name = name;
                userGroup.UpdatedBy = updatedBy;
                userGroup.UpdatedOn = DateTime.UtcNow;
                userGroup.UserGroupUsers.Clear();

                foreach (var userId in userIds)
                {
                    var userGroupUser = new UserGroupUser
                    {
                        UserGroupId = userGroup.Id,
                        UserId = userId,
                        CreatedBy = updatedBy,
                    };

                    userGroup.UserGroupUsers.Add(userGroupUser);
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

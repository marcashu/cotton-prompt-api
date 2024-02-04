using CottonPrompt.Infrastructure.Models.UserGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonPrompt.Infrastructure.Services.UserGroups
{
    public interface IUserGroupService
    {
        Task<IEnumerable<GetUserGroupsModel>> GetAsync();

        Task<GetUserGroupModel> GetByIdAsync(int id);

        Task CreateAsync(string name, IEnumerable<Guid> userIds, Guid createdBy);
    }
}

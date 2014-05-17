using System;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class GroupService : IGroupService
    {
        public IQueryable<IGroupModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task CreateGroupAsync(IGroupModel groupModel)
        {
            if (groupModel == null)
            {
                throw new ArgumentNullException("groupModel");
            }

            throw new NotImplementedException();
        }

        public Task UpdateGroupAsync(IGroupModel groupModel)
        {
            if (groupModel == null)
            {
                throw new ArgumentNullException("groupModel");
            }

            throw new NotImplementedException();
        }

        public Task DeleteGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }
    }
}
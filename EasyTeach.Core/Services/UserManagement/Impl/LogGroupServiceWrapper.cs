using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;
using NLog;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class LogGroupServiceWrapper : IGroupService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IGroupService _groupService;

        public LogGroupServiceWrapper(IGroupService groupService)
        {
            if (groupService == null)
            {
                throw new ArgumentNullException("groupService");
            }

            _groupService = groupService;
        }

        public IQueryable<IGroupModel> GetAll()
        {
            _logger.Debug("| Get groups");
            return _groupService.GetAll();
        }

        public Task CreateGroupAsync(IGroupModel groupModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGroupAsync(IGroupModel groupModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGroupAsync(int groupId)
        {
            try
            {
                return _groupService.DeleteGroupAsync(groupId);
            }
            catch (SecurityException e)
            {
                _logger.Info(" | User doesn't have enough permission for delete group ", e.ToString());
            }

            return Task.FromResult(0);
        }
    }
}

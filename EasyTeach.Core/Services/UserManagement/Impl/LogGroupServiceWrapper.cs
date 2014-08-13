using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Base.Exceptions;
using EasyTeach.Core.Services.Dashboard.Exceptions;
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
            try
            {
                var result = _groupService.GetAll();
                _logger.Debug("User received a list of groups");

                return result;
            }
            catch (Exception)
            {
                _logger.Error("User has not received a list of groups.");
                throw new Exception();
            }
        }

        public Task CreateGroupAsync(IGroupModel groupModel)
        {
            try
            {
                var result = _groupService.CreateGroupAsync(groupModel);
                _logger.Debug("User created group");

                return result;
            }
            catch (ArgumentNullException)
            {
                _logger.Info("Group is null");
                throw new ArgumentNullException();
            }
            catch (ModelValidationException)
            {
                _logger.Info("Model of group is not valid");
                throw new ModelValidationException();
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for creating group");
                throw new SecurityException();
            }
            catch (GroupNotFoundException)
            {
                _logger.Info("Group not found");
                throw new GroupNotFoundException();
            }
            catch (Exception)
            {
                _logger.Error("User has not created a group.");
                throw new Exception();
            }
        }

        public Task UpdateGroupAsync(IGroupModel groupModel)
        {
            try
            {
                var result = _groupService.UpdateGroupAsync(groupModel);
                _logger.Debug("User udated group");

                return result;
            }
            catch (ArgumentNullException)
            {
                _logger.Info("Group is null");
            }
            catch (ModelValidationException)
            {
                _logger.Info("Model of group is not valid");
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for update group");
            }
            catch (GroupNotFoundException)
            {
                _logger.Info("Group not found");
                throw new GroupNotFoundException();
            }
            catch (Exception)
            {
                _logger.Error("User has not updated a group.");
                throw new Exception();
            }

            return Task.FromResult(0);
        }

        public Task DeleteGroupAsync(int groupId)
        {
            try
            {
                var result = _groupService.DeleteGroupAsync(groupId);
                _logger.Debug("User deleted a group");

                return result;
            }
            catch (SecurityException)
            {
                _logger.Info("User doesn't have enough permission for delete group.");
            }
            catch (EntityNotFoundException)
            {
                _logger.Info("Group not found");
                throw new EntityNotFoundException("group", groupId);
            }

            return Task.FromResult(0);
        }
    }
}

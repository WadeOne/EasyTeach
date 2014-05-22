using System;
using System.Linq;
using System.Security;
using System.Security.Claims;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;
using Microsoft.AspNet.Identity;
using ClaimsAuthorizationManager = EasyTeach.Core.Security.ClaimsAuthorizationManager;

namespace EasyTeach.Core.Services.Dashboard.Impl
{
    public sealed class AuthLessonServiceWrapper : ILessonService
    {
        private readonly ILessonService _lessonService;
        private readonly ClaimsPrincipal _principal;
        private readonly EntityValidator _entityValidator;
        private readonly IUserStore<IUserDto, int> _userStore;
        private readonly ClaimsAuthorizationManager _authorizationManager;

        public AuthLessonServiceWrapper(
            ILessonService lessonService,
            ClaimsPrincipal principal,
            EntityValidator entityValidator,
            IUserStore<IUserDto, int> userStore,
            ClaimsAuthorizationManager authorizationManager)
        {
            if (lessonService == null)
            {
                throw new ArgumentNullException("lessonService");
            }

            if (principal == null)
            {
                throw new ArgumentNullException("principal");
            }

            if (principal.Identity == null)
            {
                throw new ArgumentException("Principal doesn't contian identity");
            }

            if (!principal.Identity.IsAuthenticated)
            {
                throw new ArgumentException("Identity is not authenticated");
            }

            if (entityValidator == null)
            {
                throw new ArgumentNullException("entityValidator");
            }

            if (userStore == null)
            {
                throw new ArgumentNullException("userStore");
            }

            if (authorizationManager == null)
            {
                throw new ArgumentNullException("authorizationManager");
            }

            _lessonService = lessonService;
            _principal = principal;
            _entityValidator = entityValidator;
            _userStore = userStore;
            _authorizationManager = authorizationManager;
        }

        public void CreateLesson(ILessonModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(lesson);
            if (result.IsValid == false)
            {
                throw new InvalidLessonException(result.ValidationResults);
            }

            IUserDto user =_userStore.FindByNameAsync(_principal.Identity.Name).Result;
            if (user.GroupId != lesson.Group.GroupId)
            {
                if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Lesson", "Create")))
                {
                    throw new SecurityException("User doesn't have enough permission for creating lesson");
                }
            }

           _lessonService.CreateLesson(lesson);
        }

        public void RemoveLesson(int lessonId)
        {
             ILessonModel lesson =_lessonService.GetLessonById(lessonId);

            if (lesson == null)
            {
               _lessonService.RemoveLesson(lessonId);
            }
            else
            {
                IUserDto user =_userStore.FindByNameAsync(_principal.Identity.Name).Result;
                if (user.GroupId != lesson.Group.GroupId)
                {
                    if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Lesson", "Delete")))
                    {
                        throw new SecurityException("User doesn't have enough permission for removing lesson");
                    }
                }

               _lessonService.RemoveLesson(lessonId);
            }
        }

        public void UpdateLesson(ILessonModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(lesson);
            if (result.IsValid == false)
            {
                throw new InvalidLessonException(result.ValidationResults);
            }

            IUserDto user =_userStore.FindByNameAsync(_principal.Identity.Name).Result;
            if (user.GroupId != lesson.Group.GroupId)
            {
                if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Lesson", "Update")))
                {
                    throw new SecurityException("User doesn't have enough permission for removing lesson");
                }
            }

           _lessonService.UpdateLesson(lesson);
        }

        public ILessonModel GetLessonById(int lessonId)
        {
            ILessonModel lesson =_lessonService.GetLessonById(lessonId);
            if (lesson == null)
            {
                return null;
            }

            IUserDto user =_userStore.FindByNameAsync(_principal.Identity.Name).Result;
            if (user.GroupId != lesson.Group.GroupId)
            {
                if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Lesson", "GetAll")))
                {
                    throw new SecurityException("User doesn't have enough permission for retrieving lesson");
                }
            }

            return lesson;
        }

        public IQueryable<ILessonModel> GetLessons()
        {
            if (_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Lesson", "GetAll")))
            {
                return _lessonService.GetLessons();
            }

            IUserDto user = _userStore.FindByNameAsync(_principal.Identity.Name).Result;

            return _lessonService.GetLessons().Where(l => l.Group.GroupId == user.GroupId);
        }
    }
}
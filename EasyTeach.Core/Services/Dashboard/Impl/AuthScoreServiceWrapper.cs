using System;
using System.Linq;
using System.Security;
using System.Security.Claims;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Dashboard.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.Dashboard.Impl
{
    public sealed class AuthScoreServiceWrapper : IScoreService
    {
        private readonly IScoreService _scoreService;
        private readonly ClaimsPrincipal _principal;
        private readonly EntityValidator _entityValidator;
        private readonly IUserStore<IUserDto, int> _userStore;
        private readonly ClaimsAuthorizationManager _authorizationManager;

        public AuthScoreServiceWrapper(
            IScoreService scoreService,
            ClaimsPrincipal principal,
            EntityValidator entityValidator,
            IUserStore<IUserDto, int> userStore,
            ClaimsAuthorizationManager authorizationManager)
        {
            if (scoreService == null)
            {
                throw new ArgumentNullException("scoreService");
            }

            if (principal == null)
            {
                throw new ArgumentNullException("principal");
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

            _scoreService = scoreService;
            _principal = principal;
            _entityValidator = entityValidator;
            _userStore = userStore;
            _authorizationManager = authorizationManager;
        }

        public void AddScore(IScoreModel score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

            if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Score", "Create")))
            {
                throw new SecurityException("User doesn't have enough permission for creating score");
            }

            _scoreService.AddScore(score);
        }

        public void DeleteScore(int scoreId)
        {
            if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Score", "Delete")))
            {
                throw new SecurityException("User doesn't have enough permission for delete score");
            }

            _scoreService.DeleteScore(scoreId);
        }

        public void UpdateScore(IScoreModel score)
        {
            if (score == null)
            {
                throw new ArgumentNullException("score");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(score);
            if (result.IsValid == false)
            {
                throw new InvalidLessonException(result.ValidationResults);
            }

            IUserDto user = _userStore.FindByNameAsync(_principal.Identity.Name).Result;
            if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Score", "Delete")))
            {
                throw new SecurityException("User doesn't have enough permission for update score");
            }

            _scoreService.UpdateScore(score);
        }

        public IQueryable<IScoreModel> GetScores()
        {
            if (!_authorizationManager.CheckAccess(new AuthorizationContext(_principal, "Score", "GetAll")))
            {
                throw new SecurityException("User doesn't have enough permission for retrieving score");
            }

            IUserDto user = _userStore.FindByNameAsync(_principal.Identity.Name).Result;
            if (user.GroupId != null)
            {
                return _scoreService.GetScores().Where(s => s.Visit.Visitor.Group.GroupId == user.GroupId);
            }

            return _scoreService.GetScores();
        }
    }
}

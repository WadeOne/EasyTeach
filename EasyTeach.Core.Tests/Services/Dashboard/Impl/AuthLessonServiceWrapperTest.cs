using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.Dashboard;
using EasyTeach.Core.Services.Dashboard.Impl;
using EasyTeach.Core.Validation.EntityValidator;
using FakeItEasy;
using Microsoft.AspNet.Identity;
using Xunit;
using ClaimsAuthorizationManager = EasyTeach.Core.Security.ClaimsAuthorizationManager;

namespace EasyTeach.Core.Tests.Services.Dashboard.Impl
{
    public sealed class AuthLessonServiceWrapperTest
    {
        private readonly ILessonService _lessonService;
        private readonly ClaimsPrincipal _principal;
        private readonly IIdentity _identity;
        private readonly EntityValidator _entityValidator;
        private readonly IUserStore<IUserDto, int> _userStore;
        private readonly ClaimsAuthorizationManager _authorizationManager;

        private readonly AuthLessonServiceWrapper _authLessonServiceWrapper;

        public AuthLessonServiceWrapperTest()
        {
            _lessonService = A.Fake<ILessonService>();
            _principal = A.Fake<ClaimsPrincipal>();
            _identity = A.Fake<IIdentity>();
            A.CallTo(() => _principal.Identity).Returns(_identity);
            A.CallTo(() => _identity.IsAuthenticated).Returns(true);

            _entityValidator = A.Fake<EntityValidator>();
            _userStore = A.Fake<IUserStore<IUserDto, int>>();
            _authorizationManager = new ClaimsAuthorizationManager();

            _authLessonServiceWrapper = new AuthLessonServiceWrapper(
                _lessonService,
                _principal,
                _entityValidator,
                _userStore,
                _authorizationManager);
        }

        [Fact]
        public void GetLessons_PrinciaplWithGetAllClaim_GetAll()
        {
            var group1 = A.Fake<IGroupModel>();
            A.CallTo(() => group1.GroupId).Returns(1);

            var lesson1 = A.Fake<ILessonModel>();
            A.CallTo(() => lesson1.Group).Returns(group1);

            var group2 = A.Fake<IGroupModel>();
            A.CallTo(() => group2.GroupId).Returns(2);

            var lesson2 = A.Fake<ILessonModel>();
            A.CallTo(() => lesson2.Group).Returns(group2);

            A.CallTo(() => _lessonService.GetLessons()).Returns(new[]
            {
                lesson1, lesson2
            }.AsQueryable());

            A.CallTo(() => _principal.HasClaim("Lesson", "GetAll")).Returns(true);

            IQueryable<ILessonModel> lessons = _authLessonServiceWrapper.GetLessons();

            Assert.Equal(2, lessons.Count());
        }
    }
}
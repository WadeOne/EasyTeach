using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Messaging;
using EasyTeach.Core.Services.Messaging.Impl;
using FakeItEasy;
using Microsoft.AspNet.Identity;
using Xunit;

namespace EasyTeach.Core.Tests.Services.Messaging.Impl
{
    public sealed class EmailServiceTest
    {
        private readonly UserManager<IUserDto, int> _userManager;
        private readonly EmailService _emailService;
        private readonly IEmailBuilder _emailBuilder;

        public EmailServiceTest()
        {
            _userManager = A.Fake<UserManager<IUserDto, int>>();
            _emailBuilder = A.Fake<IEmailBuilder>();
            _emailService = new EmailService(_userManager, _emailBuilder);
        }

        [Fact]
        public void SendUserRegistrationConfirmationEmailAsync_GenerateTokenAndSendEmail()
        {
            _emailService.SendUserRegistrationConfirmationEmailAsync(A.Dummy<IUserDto>()).Wait();

            A.CallTo(() => _userManager.GenerateEmailConfirmationTokenAsync(A<int>.Ignored)).MustHaveHappened();
            A.CallTo(() => _emailBuilder.BuildRegsitrationConfirmationEmailAsync(A<IUserDto>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }
    }
}
using System.IO;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Services.Messaging;
using EasyTeach.Web.Services.Messaging.Impl;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Web.Tests.Services.Messaging.Impl
{
    public sealed class EmailBuilderTest
    {
        private readonly EmailBuilder _emailBuilder;
        private readonly UrlHelper _urlHelper;
        private readonly TemplateProvider _templateProvider;

        public EmailBuilderTest()
        {
            _urlHelper = A.Fake<UrlHelper>();
            _templateProvider = A.Fake<TemplateProvider>();
            _emailBuilder = new EmailBuilder(_urlHelper, _templateProvider);
        }

        [Fact]
        public void BuildRegsitrationConfirmationEmailAsync_EmailTemplateAndUserData_EmailWithUserData()
        {
            var user = A.Fake<IUserDto>();
            A.CallTo(() => user.FirstName).Returns("John");
            A.CallTo(() => user.LastName).Returns("Doe");

            var reader = A.Fake<StreamReader>(x => x.WithArgumentsForConstructor(new object[] { new MemoryStream() }));
            A.CallTo(() => reader.ReadLineAsync())
                .ReturnsNextFromSequence(
                    Task.FromResult("Hi {{FirstName}}!"),
                    Task.FromResult("Your LastName is '{{LastName}}'."),
                    Task.FromResult((string)null));

            A.CallTo(() => _templateProvider.GetTemplate(TemplateType.EmailConfirmation))
                .Returns(reader);

            Email email = _emailBuilder.BuildRegsitrationConfirmationEmailAsync(user, "token").Result;

            Assert.Equal("Hi John!", email.Subject);
            Assert.Equal("Your LastName is 'Doe'.\r\n", email.Body);
        }

        [Fact]
        public void BuildRegsitrationConfirmationEmailAsync_EmailTemplateAndUserData_EmailWithConfirmationUrl()
        {
            A.CallTo(() => _urlHelper.Link("DefaultApi", A<object>.Ignored)).Returns("/token");

            var reader = A.Fake<StreamReader>(x => x.WithArgumentsForConstructor(new object[] { new MemoryStream() }));
            A.CallTo(() => reader.ReadLineAsync())
                .ReturnsNextFromSequence(
                    Task.FromResult("Subject"),
                    Task.FromResult("Url: {{ConfirmationUrl}}"),
                    Task.FromResult((string)null));

            A.CallTo(() => _templateProvider.GetTemplate(TemplateType.EmailConfirmation))
                .Returns(reader);

            Email email = _emailBuilder.BuildRegsitrationConfirmationEmailAsync(A.Fake<IUserDto>(), "token").Result;

            Assert.Equal("Subject", email.Subject);
            Assert.Equal("Url: /token\r\n", email.Body);
            A.CallTo(() => _urlHelper.Link("DefaultApi", A<object>.Ignored)).MustHaveHappened();
        }
    }
}
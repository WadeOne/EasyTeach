using System;
using System.IO;
using System.Threading.Tasks;
using EasyTeach.Core.Entities;
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
        private readonly TemplateProvider _templateProvider;

        public EmailBuilderTest()
        {
            _templateProvider = A.Fake<TemplateProvider>();
            _emailBuilder = new EmailBuilder(_templateProvider);
        }

        [Fact]
        public void BuildRegsitrationConfirmationEmailAsync_EmailTemplateAndUserData_EmailWithUserData()
        {
            var user = A.Fake<IUserDto>();
            A.CallTo(() => user.FirstName).Returns("John");
            A.CallTo(() => user.LastName).Returns("Doe");
            A.CallTo(() => user.Email).Returns("doe@example.org");
            A.CallTo(() => user.UserId).Returns(42);
            A.CallTo(() => user.Group).Returns(new Group
            {
                GroupNumber = 5,
                Year = 2010
            });

            var reader = A.Fake<StreamReader>(x => x.WithArgumentsForConstructor(new object[] { new MemoryStream() }));
            A.CallTo(() => reader.ReadLineAsync())
                .ReturnsNextFromSequence(
                    Task.FromResult("Hi {{FirstName}}!"),
                    Task.FromResult("Your LastName is '{{LastName}}'."),
                    Task.FromResult("Did you finished {{GroupNumber}} in {{GroupYear}}?"),
                    Task.FromResult("Visit this link http://example.com?id={{UserId}}&token={{Token}} and check your email {{Email}} after!"),
                    Task.FromResult("Enjoy!"),
                    Task.FromResult((string)null));

            A.CallTo(() => _templateProvider.GetTemplate(TemplateType.EmailConfirmation))
                .Returns(reader);

            Email email = _emailBuilder.BuildRegsitrationConfirmationEmailAsync(user, "ffdd13q").Result;

            Assert.Equal("Hi John!", email.Subject);

            var bodyLines = new[]
            {
                "Your LastName is 'Doe'.",
                "Did you finished 5 in 2010?",
                "Visit this link http://example.com?id=42&token=ffdd13q and check your email doe@example.org after!",
                "Enjoy!"
            };

            string body = String.Join(Environment.NewLine, bodyLines) + Environment.NewLine;

            Assert.Equal(body, email.Body);
        }
    }
}
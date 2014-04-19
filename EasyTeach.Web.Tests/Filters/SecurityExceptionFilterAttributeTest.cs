using System;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using EasyTeach.Web.Filters;
using Xunit;

namespace EasyTeach.Web.Tests.Filters
{
    public sealed class SecurityExceptionFilterAttributeTest
    {
        private readonly SecurityExceptionFilterAttribute _securityExceptionFilterAttribute = new SecurityExceptionFilterAttribute();

        [Fact]
        public void OnException_SecurityException_SetForbiddenResponse()
        {
            var context = new HttpActionExecutedContext
            {
                ActionContext = new HttpActionContext(),
                Exception = new SecurityException()
            };

            _securityExceptionFilterAttribute.OnException(context);

            var response = Assert.IsAssignableFrom<HttpResponseMessage>(context.Response);
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public void OnException_Exception_ResponseNotChanged()
        {
            var intialResponse = new HttpResponseMessage();

            var context = new HttpActionExecutedContext
            {
                ActionContext = new HttpActionContext(),
                Exception = new Exception(),
                Response = intialResponse
            };

            _securityExceptionFilterAttribute.OnException(context);

            Assert.Same(intialResponse, context.Response);
        }
    }
}
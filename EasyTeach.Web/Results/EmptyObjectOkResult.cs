using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace EasyTeach.Web.Results
{
    public sealed class EmptyObjectOkResult : OkResult
    {
        private readonly ApiController _controller;

        public EmptyObjectOkResult(ApiController controller)
            : base(controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            _controller = controller;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            ContentNegotiationResult result = GetContentNegotiationResult();

            HttpResponseMessage response = null;
            try
            {
                response = new HttpResponseMessage();

                if (result == null)
                {
                    //TODO: find condtions when this case can be reached
                    // A null result from content negotiation indicates that the response should be a 406.
                    response.StatusCode = HttpStatusCode.NotAcceptable;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new ObjectContent<object>(new object(), result.Formatter, result.MediaType);
                }

                response.RequestMessage = Request;
            }
            catch
            {
                if (response != null)
                {
                    response.Dispose();
                }

                throw;
            }

            return Task.FromResult(response);
        }

        private ContentNegotiationResult GetContentNegotiationResult()
        {
            HttpConfiguration configuration = _controller.Configuration;

            if (configuration == null)
            {
                throw new InvalidOperationException("Configuration Must Not Be Null");
            }

            ServicesContainer services = configuration.Services;

            IContentNegotiator contentNegotiator = services.GetContentNegotiator();

            if (contentNegotiator == null)
            {
                throw new InvalidOperationException("No Content Negotiator");
            }

            HttpRequestMessage request = _controller.Request;

            if (request == null)
            {
                throw new InvalidOperationException("Request Must Not Be Null");
            }

            IEnumerable<MediaTypeFormatter> formatters = configuration.Formatters;

            return contentNegotiator.Negotiate(typeof(object), request, formatters);
        }
    }
}
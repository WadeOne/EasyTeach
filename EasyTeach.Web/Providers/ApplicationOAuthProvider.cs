using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Services.UserManagement;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace EasyTeach.Web.Providers
{
    public sealed class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IUserService> _userServiceFactory;

        public ApplicationOAuthProvider(Func<IUserService> userServiceFactory)
        {
            if (userServiceFactory == null)
            {
                throw new ArgumentNullException("userServiceFactory");
            }

            _userServiceFactory = userServiceFactory;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IUserService userService = _userServiceFactory();

            IUserIdentityModel user = await userService.FindUserByCredentialsAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await userService.CreateUserIdentityClaimsAsync(user, context.Options.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(user.Email);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);

            ClaimsIdentity cookiesIdentity = await userService.CreateUserIdentityClaimsAsync(user, CookieAuthenticationDefaults.AuthenticationType);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult(0);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == "self")
            {
                var expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult(0);
        }

        private static AuthenticationProperties CreateProperties(string email)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "email", email }
            };

            return new AuthenticationProperties(data);
        }
    }
}
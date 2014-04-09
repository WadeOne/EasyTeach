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
        private readonly IUserService _userService;

        public ApplicationOAuthProvider(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            _userService = userService;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            IUserIdentityModel user = await _userService.FindUserByCredentialsAsync(context.UserName, context.Password);
            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await _userService.CreateUserIdentityClaimsAsync(user, context.Options.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(user.Email);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);

            ClaimsIdentity cookiesIdentity = await _userService.CreateUserIdentityClaimsAsync(user, CookieAuthenticationDefaults.AuthenticationType);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
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

            return Task.FromResult<object>(null);
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
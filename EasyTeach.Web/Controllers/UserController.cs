using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Web.Models.ViewModels;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace EasyTeach.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public sealed class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly Func<IAuthenticationManager> _authenticationManagerFactory;

        public UserController(IUserService userService, Func<IAuthenticationManager> authenticationManagerFactory)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }

            if (authenticationManagerFactory == null)
            {
                throw new ArgumentNullException("authenticationManagerFactory");
            }
            
            _userService = userService;
            _authenticationManagerFactory = authenticationManagerFactory;
        }

        // POST api/User/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(CreateUserViewModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            try
            {
                await _userService.CreateUserAsync(user.ToUser());
            }
            catch (InvalidUserDataException exception)
            {
                foreach (var validationResult in exception.ValidationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        // POST api/User/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            _authenticationManagerFactory().SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
    }
}
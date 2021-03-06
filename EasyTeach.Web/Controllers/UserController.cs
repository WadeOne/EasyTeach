﻿using System;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web.Http;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Web.Models.ViewModels;
using EasyTeach.Web.Models.ViewModels.UserManagement;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace EasyTeach.Web.Controllers
{
    [RoutePrefix("api/User")]
    public sealed class UserController : ApiControllerBase
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
        /// <summary>
        /// Register new User. Available only for teachers
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("Register")]
        [ClaimsPrincipalPermission(SecurityAction.Demand, Operation = "Register", Resource = "User")]
        public async Task<IHttpActionResult> Register(UserViewModel user)
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
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? String.Empty, validationResult.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }

        // POST api/User/ConfirmEmail
        [AllowAnonymous]
        [Route("ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(ConfirmActionViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string resetPassowrdToken = await _userService.ConfirmUserEmailAsync(model.UserId, model.ConfirmEmailToken);

                return Ok(new ResetPasswordTokenViewModel { ResetPasswordToken = resetPassowrdToken });
            }
            catch (InvalidEmailConfirmationOperationException ex)
            {
                foreach (var validationResult in ex.ValidationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? "email", validationResult.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
        }

        // POST api/User/SetPassword
        [Route("SetPassword")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.SetUserPasswordAsync(model.UserId, model.ResetPasswordToken, model.NewPassword);
                return Ok();
            }
            catch (InvalidSetPasswordOperationException ex)
            {
                foreach (var validationResult in ex.ValidationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? "email", validationResult.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
        }

        // POST api/User/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                ModelState.AddModelError("email", "Email is required");
                return BadRequest(ModelState);
            }

            try
            {
                await _userService.ResetUserPasswordAsync(email);
                return Ok();
            }
            catch (InvalidResetPasswordOperationException ex)
            {
                foreach (var validationResult in ex.ValidationResults)
                {
                    ModelState.AddModelError(validationResult.MemberNames.FirstOrDefault() ?? "email", validationResult.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
        }

        // POST api/User/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            IAuthenticationManager authenticationManager = _authenticationManagerFactory();
            authenticationManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }
    }
}
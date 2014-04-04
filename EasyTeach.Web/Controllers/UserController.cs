using System;
using System.Linq;
using System.Web.Http;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Data.Context;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Repostitories.Mappers;
using EasyTeach.Web.Models;
using EasyTeach.Web.Models.Results;

namespace EasyTeach.Web.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            if (userService == null)
            {
                throw new ArgumentNullException("userService");
            }
            _userService = userService;
        }

        public UserController()
        {
            _userService = new UserService(new UserRepository(new EasyTeachContext()), new UserDtoMapper() );
        }

        public UserCreationResult Post(User id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            try
            {
                _userService.CreateUser(id);
            }
            catch (InvalidUserDataException exception)
            {
                return new UserCreationResult
                {
                    Success = false,
                    Message = "Some fields are not correct",
                    Errors =
                        exception.ValidationResults.Select(
                            vr => new ErrorItem { PropertyName = vr.MemberNames.First(), Message = vr.ErrorMessage })
                            .ToList()
                };
                //ModelState.AddModelError("FirstName", "required");
                //return BadRequest(ModelState);
            }

            return new UserCreationResult
            {
                Success = true,
                Message = "User have been successfully created",
                Errors = null
            };

            //return Ok();
        }

    }
}
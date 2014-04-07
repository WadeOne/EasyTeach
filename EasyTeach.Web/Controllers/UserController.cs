using System;
using System.Linq;
using System.Web.Http;
using EasyTeach.Core.Services.UserManagement;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Core.Services.UserManagement.Impl;
using EasyTeach.Data.Context;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Repostitories.Mappers;
using EasyTeach.Web.Models.ViewModels;

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

        public IHttpActionResult Post(CreateUserViewModel id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            try
            {
                _userService.CreateUser(id.ToUser());
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

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserService : IUserService
    {
        private readonly IUserDtoMapper _userDtoMapper;
        private readonly UserManager<IUserDto, int> _userManager;

        public UserService(UserManager<IUserDto, int> userManager, IUserDtoMapper userDtoMapper)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (userDtoMapper == null)
            {
                throw new ArgumentNullException("userDtoMapper");
            }

            _userDtoMapper = userDtoMapper;
            _userManager = userManager;
        }

        public async Task CreateUserAsync(IUserModel newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            var validationResults = new List<ValidationResult>();
            bool userIsValid = Validator.TryValidateObject(newUser,
                                                        new ValidationContext(newUser, null, null),
                                                        validationResults, true);

            if (!String.IsNullOrWhiteSpace(newUser.Email))
            {
                IUserDto user = await _userManager.FindByEmailAsync(newUser.Email);
                if (user != null)
                {
                    validationResults.Add(new ValidationResult(String.Format("This email '{0}' has taken by another user", newUser.Email), new[] { "Email" }));
                    userIsValid = false;
                }
            }

            if (userIsValid == false)
            {
                throw new InvalidUserDataException(validationResults);
            }

            await _userManager.CreateAsync(_userDtoMapper.Map(newUser));
        }
    }
}

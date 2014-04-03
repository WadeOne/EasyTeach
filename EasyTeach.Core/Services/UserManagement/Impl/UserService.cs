using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.UserManagement.Exceptions;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IUserDtoMapper _userDtoMapper;

        public UserService(IUserRepository userRepository, IUserDtoMapper userDtoMapper)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (userDtoMapper == null)
            {
                throw new ArgumentNullException("userDtoMapper");
            }

            _userRepository = userRepository;
            _userDtoMapper = userDtoMapper;
        }

        public void CreateUser(IUserModel newUser)
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
                IUserDto user = _userRepository.GetUserByEmail(newUser.Email);
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
            
            _userRepository.SaveUser(_userDtoMapper.Map(newUser));
        }
    }
}

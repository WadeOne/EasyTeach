using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Services.UserManagement.Exceptions;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            _userRepository = userRepository;
        }

        public void CreateUser(User newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            var validationResults = new List<ValidationResult>();
            bool userIsValid = Validator.TryValidateObject(newUser,
                                                        new ValidationContext(newUser, null, null),
                                                        validationResults);

            if (userIsValid == false)
            {
                throw new InvalidUserException(validationResults);
            }

            _userRepository.SaveUser(newUser);
        }
    }
}

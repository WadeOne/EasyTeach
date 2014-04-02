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

            if (!String.IsNullOrWhiteSpace(newUser.Email))
            {
                User user = _userRepository.GetUserByEmail(newUser.Email);
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

            _userRepository.SaveUser(newUser);
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Repositories;
using EasyTeach.Core.Services.UserManagement.Exceptions;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public class UserService : IUserService
    {
        public IUserRepository UserRepository { get; set; }

        public void CreateUser(User newUser)
        {
            var validationResults = new List<ValidationResult>();
            var userIsValid = Validator.TryValidateObject(newUser,
                                                        new ValidationContext(newUser, null, null),
                                                        validationResults);

            if (userIsValid == false)
            {
                throw new InvalidUserException(validationResults);
            }
            UserRepository.SaveUser(newUser);
        }
    }
}

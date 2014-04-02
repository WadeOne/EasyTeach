using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Exceptions;
using EasyTeach.Core.Interfaces.Repositories;
using EasyTeach.Core.Interfaces.Services;

namespace EasyTeach.Domain.Services
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

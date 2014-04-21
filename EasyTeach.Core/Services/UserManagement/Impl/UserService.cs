using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers;
using EasyTeach.Core.Services.Messaging;
using EasyTeach.Core.Services.UserManagement.Exceptions;
using EasyTeach.Core.Validation.EntityValidator;

using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserService : IUserService
    {
        private readonly IUserDtoMapper _userDtoMapper;
        private readonly IEmailService _emailService;

        private readonly Func<object, ValidationContext> _validationContextFactory;

        private readonly UserManager<IUserDto, int> _userManager;

        private readonly EntityValidator _entityValidator;

        public UserService(UserManager<IUserDto, int> userManager, IUserDtoMapper userDtoMapper, IEmailService emailService, EntityValidator entityValidator, Func<object, ValidationContext> validationContextFactory = null)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            if (userDtoMapper == null)
            {
                throw new ArgumentNullException("userDtoMapper");
            }

            if (emailService == null)
            {
                throw new ArgumentNullException("emailService");
            }

            _userDtoMapper = userDtoMapper;
            _emailService = emailService;
            _entityValidator = entityValidator;
            _validationContextFactory = validationContextFactory ?? (o => new ValidationContext(o, null, null));
            _userManager = userManager;
        }

        public async Task CreateUserAsync(IUserModel newUser)
        {
            if (newUser == null)
            {
                throw new ArgumentNullException("newUser");
            }

            EntityValidationResult result = _entityValidator.ValidateEntity(newUser, _validationContextFactory(newUser));
            
            if (result.IsValid == false)
            {
                throw new InvalidUserDataException(result.ValidationResults);
            }

            IdentityResult identityResult = await _userManager.CreateAsync(_userDtoMapper.Map(newUser));
            if (!identityResult.Succeeded)
            {
                var validationResults = new List<ValidationResult>(result.ValidationResults);
                validationResults.AddRange(identityResult.Errors.Select(e => new ValidationResult(e)));
                throw new InvalidUserDataException(validationResults);
            }

            IUserDto createdUser = await _userManager.FindByEmailAsync(newUser.Email);

            await _emailService.SendUserRegistrationConfirmationEmailAsync(createdUser);
        }

        public async Task<IUserIdentityModel> FindUserByCredentialsAsync(string email, string password)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            IUserDto userDto = await _userManager.FindAsync(email, password);
            if (userDto == null)
            {
                return null;
            }

            return CreateIdentityFromDto(userDto);
        }

        public Task<ClaimsIdentity> CreateUserIdentityClaimsAsync(IUserIdentityModel userIdentity, string authenicationType)
        {
            if (userIdentity == null)
            {
                throw new ArgumentNullException("userIdentity");
            }

            if (String.IsNullOrWhiteSpace(authenicationType))
            {
                throw new ArgumentNullException("authenicationType");
            }

            return _userManager.CreateIdentityAsync(_userDtoMapper.Map(userIdentity), authenicationType);
        }

        public async Task<string> ConfirmUserEmailAsync(int userId, string token)
        {
            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            try
            {
                IdentityResult identityResult = await _userManager.ConfirmEmailAsync(userId, token);
                if (!identityResult.Succeeded)
                {
                    throw new InvalidEmailConfirmationOperationException(identityResult.Errors.Select(e => new ValidationResult(e)));
                }

                return await _userManager.GeneratePasswordResetTokenAsync(userId);
            }
            catch (AggregateException ex)
            {
                if (ex.GetBaseException() is InvalidOperationException)
                {
                    throw new InvalidEmailConfirmationOperationException(
                        new ValidationResult(String.Format("Cannot find user by id '{0}'", userId)));
                }

                throw;
            }
        }

        public async Task SetUserPasswordAsync(int userId, string resetPasswordToken, string password)
        {
            if (resetPasswordToken == null)
            {
                throw new ArgumentNullException("resetPasswordToken");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            try
            {
                IdentityResult identityResult = await _userManager.ResetPasswordAsync(userId, resetPasswordToken, password);
                if (!identityResult.Succeeded)
                {
                    throw new InvalidSetPasswordOperationException(identityResult.Errors.Select(e => new ValidationResult(e)));
                }
            }
            catch (AggregateException ex)
            {
                if (ex.GetBaseException() is InvalidOperationException)
                {
                    throw new InvalidSetPasswordOperationException(
                        new ValidationResult(String.Format("Cannot find user by id '{0}'", userId)));
                }

                throw;
            }
        }

        public async Task ResetUserPasswordAsync(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            IUserDto userDto = await _userManager.FindByEmailAsync(email);
            if (userDto == null)
            {
                throw new InvalidResetPasswordOperationException(new ValidationResult(String.Format("Cannot find user by email '{0}'", email)));
            }

            if (!userDto.EmailIsValidated)
            {
                throw new InvalidResetPasswordOperationException(new ValidationResult(String.Format("User email address '{0}' has not been validated", email)));
            }

            await _emailService.SendResetUserPasswordEmailAsync(userDto);
        }

        private static IUserIdentityModel CreateIdentityFromDto(IUserDto userDto)
        {
            return new User
            {
                Email = userDto.Email,
                UserId = userDto.UserId,
            };
        }
    }
}

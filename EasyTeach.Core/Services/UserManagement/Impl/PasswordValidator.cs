using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class PasswordValidator : IIdentityValidator<string>
    {
        private const int MinRequiredPasswordLength = 8;
        private const int MinRequiredNonAlphanumericCharacters = 2;

        public Task<IdentityResult> ValidateAsync(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return Task.FromResult(new IdentityResult("Password cannot be empty"));
            }

            if (password.Length < MinRequiredPasswordLength)
            {
                return Task.FromResult(new IdentityResult(String.Format("Password cannot shorter than {0} symbols", MinRequiredPasswordLength)));
            }

            int nonAlphaNumericCharactersCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i))
                {
                    nonAlphaNumericCharactersCount++;
                }
            }

            if (nonAlphaNumericCharactersCount < MinRequiredNonAlphanumericCharacters)
            {
                return Task.FromResult(new IdentityResult(String.Format("Password must contain at least {0} non alpha numeric characters", MinRequiredPasswordLength)));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Helpers;
using EasyTeach.Core.Repositories;

namespace EasyTeach.Core.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return String.Format("This email '{0}' has taken by another user", name);
        }

        public IUserRepository UserRepository { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as String;
            if (email == null)
            {
                throw new ArgumentException("Property type is not String");
            }
            var repository = validationContext.GetService(typeof(IUserRepository)) as IUserRepository;
            if (repository == null)
            {
                throw new InvalidOperationException("Can't access repository");
            }
            var user = AsyncHelper.RunSynchronously(() => repository.GetUserByEmail(email));
            if (user != null)
            {
                return new ValidationResult(String.Format("This email '{0}' has taken by another user", email), new [] {validationContext.DisplayName});
            }
            return null;
        }
    }
}

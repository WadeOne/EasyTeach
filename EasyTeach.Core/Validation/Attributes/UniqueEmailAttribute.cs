using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;

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
            var user = repository.GetUserByEmail(email);
            user.RunSynchronously();
            if (user.Result != null)
            {
                return new ValidationResult(String.Format("This email '{0}' has taken by another user", email));
            }
            return null;
        }
    }
}

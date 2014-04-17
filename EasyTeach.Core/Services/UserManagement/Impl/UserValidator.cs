using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.UserManagement.Impl
{
    public sealed class UserValidator : IIdentityValidator<IUserDto>
    {
        public Task<IdentityResult> ValidateAsync(IUserDto item)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
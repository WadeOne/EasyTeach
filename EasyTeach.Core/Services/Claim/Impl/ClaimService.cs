using System;
using System.Collections.Generic;
using System.Security.Principal;
using EasyTeach.Core.Entities.Data.User;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Core.Services.Claim.Impl
{
    using System.Security.Claims;

    public sealed class ClaimService : IClaimService
    {
        private readonly IUserClaimStore<IUserDto, int> _userClaimStore;
        private readonly IUserStore<IUserDto, int> _userStore;

        public ClaimService(IUserStore<IUserDto, int> userStore, IUserClaimStore<IUserDto, int> userClaimStore)
        {
            if (userStore == null)
            {
                throw new ArgumentNullException("userStore");
            }

            if (userClaimStore == null)
            {
                throw new ArgumentNullException("userClaimStore");
            }

            _userClaimStore = userClaimStore;
            _userStore = userStore;
        }

        public IEnumerable<Claim> GetUserClaims(IIdentity user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            IUserDto userDto = _userStore.FindByNameAsync(user.Name).Result;
            if (userDto == null)
            {
                throw new InvalidOperationException(String.Format("Cannot find user with name '{0}'", user.Name));
            }

            return _userClaimStore.GetClaimsAsync(userDto).Result;
        }
    }
}
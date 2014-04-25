using System.Collections.Generic;
using System.Security.Principal;

namespace EasyTeach.Core.Services.Claim
{
    using System.Security.Claims;

    public interface IClaimService
    {
        IEnumerable<Claim> GetUserClaims(IIdentity user);
    }
}
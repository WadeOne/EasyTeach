using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EasyTeach.Core.Security
{
    public sealed class ClaimsAuthorizationManager : System.Security.Claims.ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            if (HasTeacherRoleClaimForResourceAction(context, "User", "Register"))
            {
                return true;
            }

            return false;
        }

        private static bool HasTeacherRoleClaim(Claim claim)
        {
            return claim.Value == "Teacher" && claim.Type == ClaimTypes.Role &&
                   claim.ValueType == ClaimValueTypes.String;
        }

        private bool HasTeacherRoleClaimForResourceAction(AuthorizationContext context, string resource, string operation)
        {
            return HasClaimValueInCollection(context.Action, operation) &&
                   HasClaimValueInCollection(context.Resource, resource) &&
                   context.Principal.HasClaim(HasTeacherRoleClaim);
        }

        private bool HasClaimValueInCollection(IEnumerable<Claim> claims, string value)
        {
            if (claims == null)
            {
                return false;
            }

            return claims.Any(claim => claim.Value == value);
        }
    }
}
using System.Linq;
using System.Security.Claims;

namespace EasyTeach.Core.Security
{
    public sealed class ClaimsAuthorizationManager : System.Security.Claims.ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            string action = context.Action.Single().Value;
            string resource = context.Resource.Single().Value;

            return context.Principal.HasClaim(c => c.Type == resource && c.Value == action);
        }
    }
}
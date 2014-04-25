using System;
using System.Collections.Generic;
using System.Linq;

using EasyTeach.Core.Services.Claim;
using EasyTeach.Web.Models.ViewModels.Claims;

namespace EasyTeach.Web.Controllers
{
    public sealed class ClaimController : ApiControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            if (claimService == null)
            {
                throw new ArgumentNullException("claimService");
            }

            _claimService = claimService;
        }

        public IEnumerable<ClaimViewModel> Get()
        {
            return _claimService.GetUserClaims(User.Identity).Select(c => new ClaimViewModel
            {
                Resource = c.Type,
                Operation = c.Value
            });
        }
    }
}
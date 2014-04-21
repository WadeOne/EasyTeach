using System;
using System.Linq;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories
{
    public sealed class UserClaimRepository : IUserClaimRepository
    {
        private readonly EasyTeachContext _context;

        public UserClaimRepository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public IQueryable<IUserClaimDto> GetUserClaims(int userId)
        {
            return _context.UserClaims.Where(c => c.User != null && c.User.UserId == userId);
        }

        public Task AddClaimAsync(IUserClaimDto claim)
        {
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }

            _context.UserClaims.Add((UserClaimDto)claim);
            return _context.SaveChangesAsync();
        }

        public Task RemoveClaimAsync(IUserClaimDto userClaimDto)
        {
            if (userClaimDto == null)
            {
                throw new ArgumentNullException("userClaimDto");
            }

            var claim = (UserClaimDto) userClaimDto;

            var claimsToDelete = _context.UserClaims.Where(c =>
                c.User != null &&
                c.User.UserId == claim.User.UserId &&
                c.Type == claim.Type &&
                c.Value == claim.Value &&
                c.ValueType == claim.ValueType);

            foreach (UserClaimDto claimDto in claimsToDelete)
            {
                _context.UserClaims.Remove(claimDto);
            }
            
            return _context.SaveChangesAsync();
        }
    }
}
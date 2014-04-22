using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Data.Entities
{
    public class UserClaimDto : IUserClaimDto
    {
        [Key]
        public int UserClaimId { get; set; }

        public virtual UserDto User { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

        public string ValueType { get; set; }
    }
}
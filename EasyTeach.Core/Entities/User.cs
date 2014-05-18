using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Validation.Attributes;

namespace EasyTeach.Core.Entities
{
    public sealed class User : IUserModel
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public IGroupModel Group { get; set; }

        [Required]  
        [RegularExpression(@"[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})", ErrorMessage = "Not valid Email address")]
        [UniqueEmail]
        public string Email { get; set; }
    }
}
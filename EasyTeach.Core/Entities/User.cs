using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Entities
{
    public class User
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Group Group { get; set; }

        [Required]
        [RegularExpression("@", ErrorMessage = "Not valid Email address")]
        public string Email { get; set; }

        public bool EmailIsValidated { get; set; }
    }
}
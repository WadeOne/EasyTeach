using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Web.Models.ViewModels
{
    public sealed class SetPasswordViewModel
    {
        public int UserId { get; set; }

        [Required]
        public string ResetPasswordToken { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
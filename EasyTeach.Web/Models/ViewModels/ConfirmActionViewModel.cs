using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Web.Models.ViewModels
{
    public sealed class ConfirmActionViewModel
    {
        [Required]
        public string ConfirmEmailToken { get; set; }

        public int UserId { get; set; }
    }
}
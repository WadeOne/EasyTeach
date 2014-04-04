using System.Collections.Generic;

namespace EasyTeach.Web.Models.Results
{
    public class UserCreationResult
    {
        public bool Success { get; set; }
 
        public string Message { get; set; }

        public ICollection<ErrorItem> Errors { get; set; }
    }
}
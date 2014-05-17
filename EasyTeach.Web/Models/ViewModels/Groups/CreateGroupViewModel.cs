using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Groups
{
    public class CreateGroupViewModel
    {
        public int GroupNumber { get; set; }

        public int Year { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string ContactName { get; set; }

        public virtual IGroupModel ToGroup()
        {
            return new Group
            {
                GroupNumber = GroupNumber,
                Year = Year,
                ContactEmail = ContactEmail,
                ContactName = ContactName,
                ContactPhone = ContactPhone
            };
        }
    }
}
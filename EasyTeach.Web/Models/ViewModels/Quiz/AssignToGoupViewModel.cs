using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Quiz
{
    public class AssignToGoupViewModel
    {
        public virtual int Year { get; set; }

        public virtual int GroupNumber { get; set; }

        public virtual IGroupModel ToGroup()
        {
            return new Group {GroupNumber = GroupNumber, Year = Year};
        }
    }
}
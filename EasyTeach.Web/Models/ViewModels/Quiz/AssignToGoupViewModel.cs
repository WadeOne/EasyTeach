using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Quiz
{
    public class AssignToGoupViewModel
    {
        public int Year { get; set; }

        public int GroupNumber { get; set; }

        public virtual IGroupModel ToGroup()
        {
            throw new System.NotImplementedException();
        }
    }
}
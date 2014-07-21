using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Data.User;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Data.Entities
{
    public sealed class ScoreDto : IScoreDto
    {
        [Key]
        public int ScoreId { get; set; }
        public int Score { get; set; }

        IUserDto IScoreDto.AssignedTo
        {
            get { return AssignedTo; }
        }

        [ForeignKey("AssignedTo")]
        public int AssignedToId { get; set; }

        public UserDto AssignedTo { get; set; }

        public UserDto AssignedBy { get; set; }

        IUserDto IScoreDto.AssignedBy
        {
            get { return AssignedBy; }
        }


        [ForeignKey("AssignedBy")]
        public int AssignedById { get; set; }

        public IVariantProgressModel Task { get; set; }

        [ForeignKey("Visit")]
        public int? VisitId { get; set; }

        IVisitDto IScoreDto.Visit
        {
            get { return Visit; }
        }

        public VisitDto Visit { get; set; }
    }
}

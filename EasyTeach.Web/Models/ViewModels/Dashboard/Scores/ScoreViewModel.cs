using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Scores
{
    public sealed class ScoreViewModel
    {
        [Key]
        public int ScoreId { get; set; }

        public int Score { get; set; }

        public int AssignedToId { get; set; }

        public int AssignedById { get; set; }

        public IVariantProgressModel Task { get; set; }

        public int? VisitId { get; set; }
    }
}
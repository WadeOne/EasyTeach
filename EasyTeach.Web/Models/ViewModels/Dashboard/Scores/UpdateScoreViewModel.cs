using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Scores
{
    public class UpdateScoreViewModel
    {
        public int ScoreId { get; set; }

        public int Score { get; set; }

        public int AssignedToId { get; set; }

        public int AssignedById { get; set; }

        public IVariantProgressModel Task { get; set; }

        public int? VisitId { get; set; }

        public virtual IScoreModel ToScore()
        {
            return new ScoreModel
            {
                ScoreId = ScoreId,
                Score = Score,

                AssignedTo = new User
                {
                    UserId = AssignedToId
                },

                AssignedBy = new User
                {
                    UserId = AssignedById
                },
                Task = Task,
                Visit = VisitId.HasValue ? new Visit { VisitId = VisitId.Value } : null
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Scores
{
    public class CreateScoreViewModel
    {
        public int Score { get; set; }

        public IUserIdentityModel AssignedTo { get; set; }

        public IUserIdentityModel AssignedBy { get; set; }

        public IVariantProgressModel Task { get; set; }

        public IVisitModel Visit { get; set; }

        public virtual IScoreModel ToScore()
        {
            return new ScoreModel
            {
                Score = Score,
                AssignedTo = AssignedTo,
                AssignedBy = AssignedBy,
                Task = Task,
                Visit = Visit
            };
        }
    }
}
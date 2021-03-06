﻿using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public sealed class ScoreModel : IScoreModel
    {
        public int ScoreId { get; set; }

        public int Score { get; set; }

        [Required]
        public IUserModel AssignedTo { get; set; }

        [Required]
        public IUserModel AssignedBy { get; set; }

        public IVariantProgressModel Task { get; set; }

        public IVisitModel Visit { get; set; }
    }
}

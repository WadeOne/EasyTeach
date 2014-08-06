using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Web.Models.ViewModels.Dashboard.Scores
{
    public class ScoreViewModel
    {
        [Key]
        public int ScoreId { get; set; }

        public int Score { get; set; }

        public int AssignedToId { get; set; }

        public string AssignedTo { get; set; }

        public int AssignedById { get; set; }

        public string AssignedBy { get; set; }

        public IVariantProgressModel Task { get; set; }

        public int? VisitId { get; set; }

        public int LessonId { get; set; }

        public DateTime DisplayDate { get; set; }

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
                Visit = VisitId.HasValue ? new Visit 
                {
                    VisitId = VisitId.Value,
                    Lesson = new Lesson
                    {
                        LessonId = LessonId,
                        Date = DisplayDate
                    },
                    Visitor = new User
                    {
                        UserId = AssignedToId
                    }
                } : null
            };
        }
    }
}
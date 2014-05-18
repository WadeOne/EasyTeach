using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data.Group;
using EasyTeach.Core.Entities.Data.Quiz;

namespace EasyTeach.Data.Entities
{
    public class AssignedQuizDto : IAssignedQuizDto
    {
        [Key]
        public int AssignmentId { get; set; }

        public QuizDto Quiz { get; set; }

        public GroupDto Group { get; set; }

        IQuizDto IAssignedQuizDto.Quiz 
        {
            get { return Quiz; }
        }

        IGroupDto IAssignedQuizDto.Group
        {
            get { return Group; }
        }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int NumberOfQuestions { get; set; }
    }
}
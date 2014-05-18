using System;
using EasyTeach.Core.Entities.Data.Group;

namespace EasyTeach.Core.Entities.Data.Quiz
{
    public interface IAssignedQuizDto
    {
        int AssignmentId { get; }
        IQuizDto Quiz { get; }

        IGroupDto Group { get; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }

        int NumberOfQuestions { get; }
    }
}
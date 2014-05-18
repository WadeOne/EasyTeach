using System;

namespace EasyTeach.Core.Entities.Services
{
    public interface IAssignedQuizModel
    {
        IQuizModel Quiz { get; }

        IGroupModel Group { get; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }

        int NumberOfQuestions { get; }
    }
}
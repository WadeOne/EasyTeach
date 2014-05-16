using System;

namespace EasyTeach.Core.Entities.Services
{
    public interface IAssignedTestModel
    {
        IQuizModel Quiz { get; }

        IGroupModel Group { get; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }
    }
}
using System;

namespace EasyTeach.Core.Entities.Services
{
    public interface IAssignedTestModel
    {
        IQuizModel Quiz { get; }

        Group Group { get; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }
    }
}
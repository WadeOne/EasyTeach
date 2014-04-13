using System;

namespace EasyTeach.Core.Entities.Services
{
    public interface IAssignedTestModel
    {
        ITestModel Test { get; }

        Group Group { get; }

        DateTime? StartDate { get; }

        DateTime? EndDate { get; }
    }
}
using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface ITestModel
    {
        string Name { get; }

        string Description { get; }

        IEnumerable<IQuestionModel> Questions { get; }
    }
}
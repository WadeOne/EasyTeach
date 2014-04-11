using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface ITestModel
    {
        string Name { get; set; }

        string Description { get; set; }

        IEnumerable<IQuestionModel> Questions { get; set; }
    }
}
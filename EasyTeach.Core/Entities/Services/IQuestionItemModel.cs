namespace EasyTeach.Core.Entities.Services
{
    public interface IQuestionItemModel
    {
        int QuestionItemId { get; }

        string Text { get; }

        bool IsSolution { get; }
    }
}
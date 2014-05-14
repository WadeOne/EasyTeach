namespace EasyTeach.Core.Entities.Data.Quiz
{
    public interface IQuestionItemDto
    {
        int QuestionItemId { get; }

        string Text { get; }

        bool IsSolution { get; }
    }
}
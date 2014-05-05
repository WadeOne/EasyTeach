using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Repositories;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly EasyTeachContext _context;

        public QuizRepository(EasyTeachContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public Task<IQuizDto> GetQuiz(int quizId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateQuizAsync(IQuizDto quiz)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException("quiz");
            }

            quiz.Questions = new List<QuestionModel>();
            _context.Quizes.Add((QuizDto) quiz);
            await _context.SaveChangesAsync();
        }

        public Task AssignQuizAsync(IAssignedQuizDto assignedQuiz)
        {
            throw new System.NotImplementedException();
        }

        public Task AddQuestionToQuiz(int quizId, IQuestionDto questionDto)
        {
            throw new NotImplementedException();
        }
    }
}

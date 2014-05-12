using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

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

        public async Task<IQuizDto> GetQuiz(int quizId)
        {
            return await _context.Quizes.FirstOrDefaultAsync(x => x.QuizId == quizId);
        }

        public async Task CreateQuizAsync(IQuizDto quiz)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException("quiz");
            }

            quiz.Questions = new List<QuestionDto>();
            _context.Quizes.Add((QuizDto) quiz);
            await _context.SaveChangesAsync();
        }

        public Task AssignQuizAsync(IAssignedQuizDto assignedQuiz)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddQuestionToQuiz(int quizId, IQuestionDto question)
        {
            if (question == null)
            {
                throw new ArgumentNullException("question");
            }

            var quiz = await _context.Quizes.FirstOrDefaultAsync(x => x.QuizId == quizId);
            if (quiz != null)
            {
                quiz.Questions.Add((QuestionDto)question);
                await _context.SaveChangesAsync();
            }
        }
    }
}

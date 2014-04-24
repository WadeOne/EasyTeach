using System;
using System.Threading.Tasks;
using EasyTeach.Core.Entities.Data;
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

        public async Task CreateQuizAsync(IQuizDto quiz)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException("quiz");
            }

            _context.Quizes.Add((QuizDto) quiz);
            await _context.SaveChangesAsync();
        }

        public Task AssignQuizAsync(IAssignedTestDto assignedTest)
        {
            throw new System.NotImplementedException();
        }
    }
}

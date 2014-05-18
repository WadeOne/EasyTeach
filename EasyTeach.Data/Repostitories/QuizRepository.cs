using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

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

        public async Task AssignQuizAsync(IAssignedQuizDto assignedQuiz)
        {
            if (assignedQuiz == null)
            {
                throw new ArgumentNullException("assignedQuiz");
            }

            var group = await _context.Groups.FirstOrDefaultAsync(x => x.GroupNumber == assignedQuiz.Group.GroupNumber && x.Year == assignedQuiz.Group.Year);
            var assignedQuizDto = (AssignedQuizDto)assignedQuiz;
            assignedQuizDto.Group = group;
            _context.AssignedQuizes.Add(assignedQuizDto);
            await _context.SaveChangesAsync();
        }

        public async Task AddQuestionToQuiz(int quizId, IQuestionDto question)
        {
            if (question == null)
            {
                throw new ArgumentNullException("question");
            }

            var quiz = await _context.Quizes.Include(x => x.Questions).FirstOrDefaultAsync(x => x.QuizId == quizId);
            if (quiz != null)
            {
                if (quiz.Questions == null)
                {
                    quiz.Questions = new List<QuestionDto>();
                }
                quiz.Questions.Add((QuestionDto)question);
                question.QuestionItems.Each(x => _context.QuestionItems.Add((QuestionItemDto)x));
                question.QuestionItems.Each(x => ((QuestionItemDto)x).Question = (QuestionDto)question);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<IQuizDto>> GetAllQuizes()
        {
            var result = new List<IQuizDto>();
            _context.Quizes.Each(result.Add);
            return Task.FromResult((IEnumerable<IQuizDto>)result);
        }
    }
}

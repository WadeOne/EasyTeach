using System;
using System.Collections.Generic;
using System.Data.Entity;

using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Tests.Context;

using FakeItEasy;
using Xunit;

namespace EasyTeach.Data.Tests.Repostitories
{
    public class QuizRepositoryTest
    {
        private readonly QuizRepository _quizRepository;
        private readonly EasyTeachContext _context;
        private readonly QuizDto _quizDto;
        private const int QuizId = 1;

        public QuizRepositoryTest()
        {
            _context = A.Fake<EasyTeachContext>();
            _quizRepository = new QuizRepository(_context);
            _quizDto = new QuizDto { QuizId = QuizId, Questions = new List<QuestionDto>()};
        }

        [Fact]
        public void CreateQuizAsync_NullQuiz_ArgumentNullExceptionThrown()
        {
            var aggregateException = Assert.Throws<AggregateException>(() => _quizRepository.CreateQuizAsync(null).Wait());
            var baseException = aggregateException.GetBaseException() as ArgumentNullException;

            Assert.NotNull(baseException);
        }

        [Fact]
        public void CreateQuizAsync_NotNullQuiz_QuizAdded()
        {
            IDbSet<QuizDto> quizes = A.Fake<IDbSet<QuizDto>>();
            A.CallTo(() => _context.Quizes).Returns(quizes);
            A.CallTo(() => quizes.Add(_quizDto)).Returns(_quizDto);

            _quizRepository.CreateQuizAsync(_quizDto);

            A.CallTo(() => quizes.Add(_quizDto)).MustHaveHappened();
            A.CallTo(() => _context.SaveChangesAsync()).MustHaveHappened();
        }

        [Fact]
        public void GetQuiz_QuizExists_ReturnedQuiz()
        {
            IDbSet<QuizDto> quizes = new FakeDbSet<QuizDto>(new []{_quizDto});
            A.CallTo(() => _context.Quizes).Returns(quizes);

            var quiz = _quizRepository.GetQuiz(QuizId).Result;

            Assert.IsAssignableFrom<QuizDto>(quiz);
            Assert.Equal(quiz.QuizId, _quizDto.QuizId);
        }

        [Fact]
        public void GetQuiz_QuizDoesntExist_ReturnedNull()
        {
            IDbSet<QuizDto> quizes = new FakeDbSet<QuizDto>();
            A.CallTo(() => _context.Quizes).Returns(quizes);

            var quiz = _quizRepository.GetQuiz(QuizId).Result;

            Assert.Null(quiz);
        }

      
        //[Fact]
        //public void AddQuestionToQuiz_ValidParams_QuestionAdded()
        //{
        //    QuestionDto question = A.Fake<QuestionDto>();
        //    A.CallTo(() => _quizRepository.GetQuiz(QuizId)).Returns(_quizDto);
        //    A.CallTo(() => _quizDto.Questions)
        //}
    }
}

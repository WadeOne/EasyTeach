using System;
using System.Data.Entity;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Data.Tests.Repostitories
{
    public class QuizRepositoryTest
    {
        private readonly QuizRepository _quizRepository;
        private readonly EasyTeachContext _context;
        private readonly QuizDto _quizDto;

        public QuizRepositoryTest()
        {
            _context = A.Fake<EasyTeachContext>();
            _quizRepository = new QuizRepository(_context);
            _quizDto = new QuizDto();
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
    }
}

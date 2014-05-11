using System;
using System.Data.Entity;
using System.Linq;
using EasyTeach.Data.Context;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories;
using EasyTeach.Data.Tests.Context;
using FakeItEasy;
using Xunit;

namespace EasyTeach.Data.Tests.Repostitories
{
    public sealed class LessonRepositoryTest
    {
        private readonly LessonRepository _repository;
        private readonly EasyTeachContext _context;

        public LessonRepositoryTest()
        {
            _context = A.Fake<EasyTeachContext>();
            _repository = new LessonRepository(_context);
        }

        [Fact]
        public void CreateLessonAsync_LessonDto_LessonAdd()
        {
            IDbSet<LessonDto> lessons = A.Fake<IDbSet<LessonDto>>();
            A.CallTo(() => _context.Lessons).Returns(lessons);

            _repository.CreateLessonAsync(new LessonDto()).Wait();

            A.CallTo(() => lessons.Add(A<LessonDto>.Ignored)).MustHaveHappened();
            A.CallTo(() => _context.SaveChangesAsync()).MustHaveHappened();
        }

        [Fact]
        public void RemoveLessonAsync_ExistingId_LessonRemove()
        {
            IDbSet<LessonDto> lessons = new FakeDbSet<LessonDto>(new[]
            {
                new LessonDto
                {
                    LessonId = 42
                }
            });

            A.CallTo(() => _context.Lessons).Returns(lessons);

            _repository.RemoveLessonAsync(42).Wait();

            Assert.Empty(lessons);
            A.CallTo(() => _context.SaveChangesAsync()).MustHaveHappened();
        }

        [Fact]
        public void UpdateLessonAsync_ExistingLesson_LessonUpdate()
        {
            IDbSet<LessonDto> lessons = new FakeDbSet<LessonDto>(new[]
            {
                new LessonDto
                {
                    LessonId = 42,
                    Date = new DateTime(2010, 1, 1),
                    GroupId = 1
                }
            });

            A.CallTo(() => _context.Lessons).Returns(lessons);

            _repository.UpdateLessonAsync(new LessonDto
            {
                LessonId = 42,
                Date = new DateTime(2013, 10, 10),
                GroupId = 3
            }).Wait();

            Assert.Equal(new DateTime(2013, 10, 10), lessons.First().Date);
            Assert.Equal(3, lessons.First().GroupId);
            A.CallTo(() => _context.SaveChangesAsync()).MustHaveHappened();
        }

    }
}
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
        public void CreateLesson_LessonDto_LessonAdd()
        {
            IDbSet<LessonDto> lessons = A.Fake<IDbSet<LessonDto>>();
            A.CallTo(() => _context.Lessons).Returns(lessons);

            _repository.CreateLesson(new LessonDto());

            A.CallTo(() => lessons.Add(A<LessonDto>.Ignored)).MustHaveHappened();
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened();
        }

        [Fact]
        public void RemoveLesson_ExistingId_LessonRemove()
        {
            IDbSet<LessonDto> lessons = new FakeDbSet<LessonDto>(new[]
            {
                new LessonDto
                {
                    LessonId = 42
                }
            });

            A.CallTo(() => _context.Lessons).Returns(lessons);

            _repository.RemoveLesson(42);

            Assert.Empty(lessons);
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened();
        }

        [Fact]
        public void UpdateLesson_ExistingLesson_LessonUpdate()
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

            _repository.UpdateLesson(new LessonDto
            {
                LessonId = 42,
                Date = new DateTime(2013, 10, 10),
                GroupId = 3
            });

            Assert.Equal(new DateTime(2013, 10, 10), lessons.First().Date);
            Assert.Equal(3, lessons.First().GroupId);
            A.CallTo(() => _context.SaveChanges()).MustHaveHappened();
        }

    }
}
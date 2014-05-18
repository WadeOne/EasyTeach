using System;
using EasyTeach.Core.Entities;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories.Mappers.QuizManagement;
using Xunit;

namespace EasyTeach.Data.Tests.Repostitories.Mappers
{
    public class AssignedQuizDtoMapperTest
    {
        private readonly AssignedQuizDtoMapper _mapper = new AssignedQuizDtoMapper();

        [Fact]
        public void Map_DtoToModel_Mapped()
        {
            var dto = new AssignedQuizDto
            {
                AssignmentId = 1,
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now,
                Group = new GroupDto
                {
                    GroupNumber = 1,
                    Year = 2009
                },
                NumberOfQuestions = 10,
                Quiz = new QuizDto
                {
                    QuizId = 1
                }
            };

            var model = _mapper.Map(dto);

            Assert.NotNull(model);
            Assert.Equal(dto.StartDate, model.StartDate);
            Assert.Equal(dto.EndDate, model.EndDate);
            Assert.Equal(dto.NumberOfQuestions, model.NumberOfQuestions);
            Assert.Equal(dto.Group.GroupNumber, model.Group.GroupNumber);
            Assert.Equal(dto.Group.Year, model.Group.Year);
            Assert.Equal(dto.Quiz.QuizId, model.Quiz.QuizId);
        }

        [Fact]
        public void Map_ModelToDto_Mapped()
        {
            var model = new AssignedQuiz
            {
                EndDate = DateTime.Now.AddDays(1),
                StartDate = DateTime.Now,
                Group = new Group
                {
                    GroupNumber = 1,
                    Year = 2009
                },
                NumberOfQuestions = 10,
                Quiz = new Quiz
                {
                    QuizId = 1
                }
            };

            var dto = _mapper.Map(model);

            Assert.NotNull(model);
            Assert.Equal(dto.StartDate, model.StartDate);
            Assert.Equal(dto.EndDate, model.EndDate);
            Assert.Equal(dto.NumberOfQuestions, model.NumberOfQuestions);
            Assert.Equal(dto.Group.GroupNumber, model.Group.GroupNumber);
            Assert.Equal(dto.Group.Year, model.Group.Year);
            Assert.Equal(dto.Quiz.QuizId, model.Quiz.QuizId);
        }
    }
}

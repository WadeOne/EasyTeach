using System.Collections.Generic;
using System.Linq;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;
using EasyTeach.Data.Entities;
using EasyTeach.Data.Repostitories.Mappers.QuizManagement;

using FakeItEasy;

using Xunit;

namespace EasyTeach.Data.Tests.Repostitories.Mappers
{
    public class QuizDtoMapperTest
    {
        private readonly QuizDtoMapper _mapper;

        public QuizDtoMapperTest()
        {
            _mapper = new QuizDtoMapper();
        }

        [Fact]
        public void Map_FromModelToDto_Mapped()
        {
            var model = A.Fake<IQuizModel>();
            A.CallTo(() => model.Name).Returns("Quiz");
            A.CallTo(() => model.Description).Returns("QuizDescription");
            A.CallTo(() => model.QuizId).Returns(1);
            A.CallTo(() => model.Questions)
                .Returns(
                    new List<IQuestionModel>
                    {
                        new Question
                        {
                            QuestionId = 1,
                            QuestionType = QuestionType.Select,
                            QuestionItems =
                                new List<QuestionItem>
                                {
                                    new QuestionItem
                                    {
                                        Text = "Question",
                                        IsSolution = true,
                                        QuestionItemId = 1
                                    }
                                },
                            QuestionText = "Text"
                        }
                    });

            IQuizDto quizDto = _mapper.Map(model);

            Assert.Equal(model.Name, quizDto.Name);
            Assert.Equal(model.Description, quizDto.Description);
            Assert.Equal(model.QuizId, quizDto.QuizId);
            Assert.True(quizDto.Questions.FirstOrDefault().QuestionId == model.Questions.FirstOrDefault().QuestionId);
            Assert.True(quizDto.Questions.FirstOrDefault().QuestionItems.FirstOrDefault().QuestionItemId == model.Questions.FirstOrDefault().QuestionItems.FirstOrDefault().QuestionItemId);
        }

        [Fact]
        public void Map_FromDtoToModel_Mapped()
        {
            var quizDto = A.Fake<IQuizDto>();
            A.CallTo(() => quizDto.Name).Returns("Quiz");
            A.CallTo(() => quizDto.Description).Returns("QuizDescription");
            A.CallTo(() => quizDto.QuizId).Returns(1);
            A.CallTo(() => quizDto.Questions)
                .Returns(
                    new List<IQuestionDto>
                    {
                        new QuestionDto
                        {
                            QuestionId = 1,
                            QuestionType = QuestionType.Select,
                            QuestionItems =
                                new List<QuestionItemDto>
                                {
                                    new QuestionItemDto
                                    {
                                        Text = "Question",
                                        IsSolution = true,
                                        QuestionItemId = 1
                                    }
                                },
                            QuestionText = "Text"
                        }
                    });

            IQuizModel model = _mapper.Map(quizDto);

            Assert.Equal(quizDto.Name, model.Name);
            Assert.Equal(quizDto.Description, model.Description);
            Assert.Equal(quizDto.QuizId, model.QuizId);
            Assert.True(model.Questions.FirstOrDefault().QuestionId == quizDto.Questions.FirstOrDefault().QuestionId);
            Assert.True(model.Questions.FirstOrDefault().QuestionItems.FirstOrDefault().QuestionItemId == quizDto.Questions.FirstOrDefault().QuestionItems.FirstOrDefault().QuestionItemId);
        }
    }
}

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
    public class QuestionDtoMapperTest
    {
        private readonly QuestionDtoMapper _mapper;

        public QuestionDtoMapperTest()
        {
            _mapper = new QuestionDtoMapper();
        }

        [Fact]
        public void Map_FromModelToDto_Mapped()
        {
            var model = A.Fake<IQuestionModel>();
            A.CallTo(() => model.QuestionId).Returns(1);
            A.CallTo(() => model.QuestionText).Returns("Text");
            A.CallTo(() => model.QuestionType).Returns(QuestionType.Select);
            A.CallTo(() => model.QuestionItems)
                .Returns(new List<IQuestionItemModel> { new QuestionItemModel { Text = "text" } });

            var result = _mapper.Map(model);

            Assert.Equal(model.QuestionId, result.QuestionId);
            Assert.Equal(model.QuestionText, result.QuestionText);
            Assert.Equal(model.QuestionType, result.QuestionType);
            Assert.True(model.QuestionItems.FirstOrDefault().Text.Equals("text"));
        }

        [Fact]
        public void Map_FromDtoToModel_Mapped()
        {
            var dto = A.Fake<IQuestionDto>();
            A.CallTo(() => dto.QuestionId).Returns(1);
            A.CallTo(() => dto.QuestionText).Returns("Text");
            A.CallTo(() => dto.QuestionType).Returns(QuestionType.Select);
            A.CallTo(() => dto.QuestionItems)
                .Returns(new List<IQuestionItemDto> { new QuestionItemDto() { Text = "text" } });

            var result = _mapper.Map(dto);

            Assert.Equal(dto.QuestionId, result.QuestionId);
            Assert.Equal(dto.QuestionText, result.QuestionText);
            Assert.Equal(dto.QuestionType, result.QuestionType);
            Assert.True(dto.QuestionItems.FirstOrDefault().Text.Equals("text"));
        }
    }
}
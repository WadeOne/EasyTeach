using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.QuizManagement;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.QuizManagement
{
    public class QuizDtoMapper : IQuizDtoMapper
    {
        public QuizDtoMapper()
        {
            Mapper.CreateMap<IQuizDto, QuizModel>();
            Mapper.CreateMap<IQuizModel, QuizDto>();
            Mapper.CreateMap<IQuestionDto, QuestionModel>();
            Mapper.CreateMap<IQuestionModel, QuestionDto>();
            Mapper.CreateMap<ICollection<IQuestionModel>, ICollection<QuestionDto>>().ConvertUsing(new DtoToModelCollectionConverter());
            Mapper.CreateMap<ICollection<IQuestionDto>, ICollection<QuestionModel>>().ConvertUsing(new ModelToDtoCollectionConverter());
        }

        public IQuizDto Map(IQuizModel quiz)
        {
            return Mapper.Map<QuizDto>(quiz);
        }

        public IQuizModel Map(IQuizDto quizDto)
        {
            return Mapper.Map<QuizModel>(quizDto);
        }

        public IAssignedQuizDto Map(IAssignedTestModel assignedTest)
        {
            throw new System.NotImplementedException();
        }

        private class DtoToModelCollectionConverter : TypeConverter<ICollection<IQuestionModel>, ICollection<QuestionDto>>
        {
            protected override ICollection<QuestionDto> ConvertCore(ICollection<IQuestionModel> source)
            {
                return source.Select(Mapper.Map<QuestionDto>).ToList();
            }
        }

        private class ModelToDtoCollectionConverter : TypeConverter<ICollection<IQuestionDto>, ICollection<QuestionModel>>
        {
            protected override ICollection<QuestionModel> ConvertCore(ICollection<IQuestionDto> source)
            {
                return source.Select(Mapper.Map<QuestionModel>).ToList();
            }
        }
    }
}

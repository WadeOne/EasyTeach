using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Quiz;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers
{
    internal static class MappingConfiguration
    {
        private static bool _isConfigured;

        public static void Configure()
        {
            if (_isConfigured == false)
            {
                Mapper.CreateMap<IQuizDto, QuizModel>();
                Mapper.CreateMap<IQuizModel, QuizDto>();
                Mapper.CreateMap<IQuestionDto, QuestionModel>();
                Mapper.CreateMap<IQuestionModel, QuestionDto>();
                Mapper.CreateMap<QuestionItemModel, QuestionItemDto>();
                Mapper.CreateMap<QuestionItemDto, QuestionItemModel>();
                Mapper.CreateMap<ICollection<IQuestionModel>, ICollection<QuestionDto>>()
                    .ConvertUsing(new ModelToDtoQuestionCollectionConverter());
                Mapper.CreateMap<IEnumerable<IQuestionDto>, ICollection<QuestionModel>>()
                    .ConvertUsing(new DtoToModelQuestionCollectionConverter());
                Mapper.CreateMap<IEnumerable<IQuestionItemDto>, ICollection<QuestionItemModel>>()
                    .ConvertUsing(new DtoToModelQuestionItemCollectionConverter());
                
                _isConfigured = true;
            }
        }

        private class ModelToDtoQuestionCollectionConverter : TypeConverter<ICollection<IQuestionModel>, ICollection<QuestionDto>>
        {
            protected override ICollection<QuestionDto> ConvertCore(ICollection<IQuestionModel> source)
            {
                return source.Select(Mapper.Map<QuestionDto>).ToList();
            }
        }

        private class DtoToModelQuestionCollectionConverter : TypeConverter<IEnumerable<IQuestionDto>, ICollection<QuestionModel>>
        {
            protected override ICollection<QuestionModel> ConvertCore(IEnumerable<IQuestionDto> source)
            {
                return source.Select(Mapper.Map<QuestionModel>).ToList();
            }
        }

        private class DtoToModelQuestionItemCollectionConverter : TypeConverter<IEnumerable<IQuestionItemDto>, ICollection<QuestionItemModel>>
        {
            protected override ICollection<QuestionItemModel> ConvertCore(IEnumerable<IQuestionItemDto> source)
            {
                return source.Select(Mapper.Map<QuestionItemModel>).ToList();
            }
        }
    }
}

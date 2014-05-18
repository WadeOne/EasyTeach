using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data.Group;
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
                Mapper.CreateMap<IQuizDto, Quiz>();
                Mapper.CreateMap<IQuizModel, QuizDto>();
                Mapper.CreateMap<IQuestionDto, Question>();
                Mapper.CreateMap<IQuestionModel, QuestionDto>();
                Mapper.CreateMap<IAssignedQuizDto, AssignedQuiz>();
                Mapper.CreateMap<IAssignedQuizModel, AssignedQuizDto>();
                Mapper.CreateMap<IGroupModel, GroupDto>();
                Mapper.CreateMap<IGroupDto, Group>();
                Mapper.CreateMap<QuestionItem, QuestionItemDto>();
                Mapper.CreateMap<QuestionItemDto, QuestionItem>();

                Mapper.CreateMap<ICollection<IQuestionModel>, ICollection<QuestionDto>>().ConvertUsing(new ModelToDtoCollectionConverter<IQuestionModel, QuestionDto>());
                Mapper.CreateMap<IEnumerable<IQuestionDto>, ICollection<Question>>().ConvertUsing(new DtoToModelCollectionConverter<IQuestionDto, Question>());
                Mapper.CreateMap<IEnumerable<IQuestionItemDto>, ICollection<QuestionItem>>().ConvertUsing(new DtoToModelCollectionConverter<IQuestionItemDto, QuestionItem>());
                
                _isConfigured = true;
            }
        }

        private class DtoToModelCollectionConverter<T1, T2> : TypeConverter<IEnumerable<T1>, ICollection<T2>>
        {
            protected override ICollection<T2> ConvertCore(IEnumerable<T1> source)
            {
                return source == null ? null : source.Select(Mapper.DynamicMap<T1, T2>).ToList();
            }
        }

        private class ModelToDtoCollectionConverter<T1, T2> : TypeConverter<ICollection<T1>, ICollection<T2>>
        {
            protected override ICollection<T2> ConvertCore(ICollection<T1> source)
            {
                return source == null ? null : source.Select(Mapper.DynamicMap<T1, T2>).ToList();
            }
        }
    }
}

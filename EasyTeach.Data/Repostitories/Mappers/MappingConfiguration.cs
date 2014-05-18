﻿using System.Collections.Generic;
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
                Mapper.CreateMap<IQuizDto, Quiz>();
                Mapper.CreateMap<IQuizModel, QuizDto>();
                Mapper.CreateMap<IQuestionDto, Question>();
                Mapper.CreateMap<IQuestionModel, QuestionDto>();
                Mapper.CreateMap<QuestionItem, QuestionItemDto>();
                Mapper.CreateMap<QuestionItemDto, QuestionItem>();
                Mapper.CreateMap<ICollection<IQuestionModel>, ICollection<QuestionDto>>().ConvertUsing(new ModelToDtoQuestionCollectionConverter());
                Mapper.CreateMap<IEnumerable<IQuestionDto>, ICollection<Question>>().ConvertUsing(new DtoToModelQuestionCollectionConverter());
                Mapper.CreateMap<IEnumerable<IQuestionItemDto>, ICollection<QuestionItem>>().ConvertUsing(new DtoToModelQuestionItemCollectionConverter());
                
                _isConfigured = true;
            }
        }

        private class ModelToDtoQuestionCollectionConverter : TypeConverter<ICollection<IQuestionModel>, ICollection<QuestionDto>>
        {
            protected override ICollection<QuestionDto> ConvertCore(ICollection<IQuestionModel> source)
            {
                return source == null ? null : source.Select(Mapper.Map<QuestionDto>).ToList();
            }
        }

        private class DtoToModelQuestionCollectionConverter : TypeConverter<IEnumerable<IQuestionDto>, ICollection<Question>>
        {
            protected override ICollection<Question> ConvertCore(IEnumerable<IQuestionDto> source)
            {
                return source == null ? null : source.Select(Mapper.Map<Question>).ToList();
            }
        }

        private class DtoToModelQuestionItemCollectionConverter : TypeConverter<IEnumerable<IQuestionItemDto>, ICollection<QuestionItem>>
        {
            protected override ICollection<QuestionItem> ConvertCore(IEnumerable<IQuestionItemDto> source)
            {
                return source == null ? null : source.Select(Mapper.Map<QuestionItem>).ToList();
            }
        }
    }
}
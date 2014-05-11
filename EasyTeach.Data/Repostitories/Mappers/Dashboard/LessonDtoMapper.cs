using System;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.Dashboard
{
    public sealed class LessonDtoMapper : ILessonDtoMapper
    {
        public ILessonDto Map(ILessonModel lesson)
        {
            if (lesson == null)
            {
                throw new ArgumentNullException("lesson");
            }

            return new LessonDto
            {
                Date = lesson.Date,
                LessonId = lesson.LessonId,
                GroupId = lesson.Group.GroupId
            };
        }
    }
}
using System;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Repositories.Mappers.Dashboard;
using EasyTeach.Data.Entities;

namespace EasyTeach.Data.Repostitories.Mappers.Dashboard
{
    public sealed class VisitDtoMapper : IVisitDtoMapper
    {
        public IVisitDto Map(IVisitModel visit)
        {
            if (visit == null)
            {
                throw new ArgumentNullException("visit");
            }

            return new VisitDto
            {
                LessonId = visit.Lesson.LessonId,
                Note = visit.Note,
                Status = visit.Status,
                UserId = visit.Visitor.UserId
            };
        }
    }
}
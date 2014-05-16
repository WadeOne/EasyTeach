using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Repositories.Mappers.Dashboard
{
    public interface ILessonDtoMapper
    {
        ILessonDto Map(ILessonModel lesson);
    }
}
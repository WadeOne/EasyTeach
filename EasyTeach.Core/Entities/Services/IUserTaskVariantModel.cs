using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IUserTaskVariantModel
    {
        IUserModel User { get; }

        ITaskVariantModel TaskVariant { get; }

        ProgressStatus Progress { get; }
    }
}
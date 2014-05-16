using EasyTeach.Core.Enums;

namespace EasyTeach.Core.Entities.Services
{
    public interface IVariantProgressModel
    {
        IUserModel User { get; }

        ITaskVariantModel Variant { get; }

        ProgressStatus Progress { get; }
    }
}
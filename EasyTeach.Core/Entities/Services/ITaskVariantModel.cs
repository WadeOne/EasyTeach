using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface ITaskVariantModel
    {
        IEnumerable<IUserModel> Students { get; }

        ITaskModel Task { get; }

        int Id { get; }
    }
}
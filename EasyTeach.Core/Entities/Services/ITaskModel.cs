using System;
using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface ITaskModel
    {
        string Subject { get; }

        DateTime Due { get; }

        DateTime Announced { get; }

        IEnumerable<ITaskVariantModel> Variants { get; }

        IEnumerable<ITaskAssetModel> Assets { get; }
    }
}
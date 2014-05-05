using System;
using System.Collections.Generic;

namespace EasyTeach.Core.Entities.Services
{
    public interface ITaskModel
    {
        string Name { get; }

        DateTime Expired { get; }

        DateTime Announced { get; }

        IEnumerable<ITaskVariantModel> Variants { get; }
    }
}
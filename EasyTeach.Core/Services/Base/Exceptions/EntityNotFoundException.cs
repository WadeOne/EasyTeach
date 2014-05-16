using System;
using System.Globalization;

namespace EasyTeach.Core.Services.Base.Exceptions
{
    public sealed class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity, string searchAttribute, string value)
            : base(String.Format("Cannot found {0} by {1} equals '{2}'", entity, searchAttribute, value))
        {
        }

        public EntityNotFoundException(string entity, int id)
            : this(entity, "id", id.ToString(CultureInfo.InvariantCulture))
        {
        }
    }
}
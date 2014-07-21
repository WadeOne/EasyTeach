using System;

namespace EasyTeach.Core.Services.Dashboard.Exceptions
{
    public sealed class ScoreNotFoundException : Exception
    {
        public ScoreNotFoundException()
        {
        }

        public ScoreNotFoundException(string message)
            : base(message)
        {
        }
    }
}

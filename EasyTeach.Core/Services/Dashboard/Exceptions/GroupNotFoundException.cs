using System;

namespace EasyTeach.Core.Services.Dashboard.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException()
        {
        }

        public GroupNotFoundException(string message) : base(message)
        {
        }
    }
}
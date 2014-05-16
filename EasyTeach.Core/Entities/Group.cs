using System.Collections.Generic;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public sealed class Group : IGroupModel
    {
        public int GroupId { get; set; }

        /// <summary>
        /// Group number
        /// </summary>
        public int GroupNumber { get; set; }

        /// <summary>
        /// University entering year
        /// </summary>
        public int Year { get; set; }

        public ICollection<IUserModel> Students { get; set; }
    }
}
using System.Collections.Generic;
using EasyTeach.Core.Entities.Services;

namespace EasyTeach.Core.Entities
{
    public class Group
    {
        public int GroupId { get; set; }

        /// <summary>
        /// Group number
        /// </summary>
        public int GroupNumber { get; set; }

        /// <summary>
        /// University entering year
        /// </summary>
        public uint Year { get; set; }

        public virtual ICollection<IUserModel> Students { get; set; }
    }
}
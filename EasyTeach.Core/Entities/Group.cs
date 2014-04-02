using System.Collections.Generic;

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
        public int Year { get; set; }

        public virtual ICollection<User> Students { get; set; }
    }
}
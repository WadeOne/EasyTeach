﻿using System.Collections.Generic;

namespace EasyTeach.Core.Entities
{
    public sealed class Group
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

        public ICollection<User> Students { get; set; }
    }
}
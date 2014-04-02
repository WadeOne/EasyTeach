using System;
using System.ComponentModel.DataAnnotations;

namespace EasyTeach.Core.Entities
{
    public class Group
    {
        /// <summary>
        /// Group number
        /// </summary>
        public int GroupNumber { get; set; }

        /// <summary>
        /// University entering year
        /// </summary>
        public int Year { get; set; }
    }
}
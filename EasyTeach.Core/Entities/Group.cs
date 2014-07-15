using System.Collections.Generic;
using EasyTeach.Core.Entities.Services;
using System.ComponentModel.DataAnnotations;
using System;

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
        [Range(1900, Int32.MaxValue)]
        public int Year { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string ContactName { get; set; }

        public ICollection<IUserModel> Students { get; set; }
    }
}
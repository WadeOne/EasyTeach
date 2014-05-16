using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data.Group;

namespace EasyTeach.Data.Entities
{
    public class GroupDto : IGroupDto
    {
        [Key]
        public int GroupId { get; set; }

        public int GroupNumber { get; set; }

        public int Year { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string ContactName { get; set; }
    }
}
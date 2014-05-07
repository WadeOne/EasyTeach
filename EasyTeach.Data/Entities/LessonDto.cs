using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyTeach.Core.Entities.Data.Dashboard;
using EasyTeach.Core.Entities.Data.Group;

namespace EasyTeach.Data.Entities
{
    public sealed class LessonDto : ILessonDto
    {
        [Key]
        public int LessonId { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }

        IGroupDto ILessonDto.Group
        {
            get { return Group; }
        }

        public GroupDto Group { get; set; }
    }
}
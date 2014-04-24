using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Data.Entities
{
    //TODO implement DTO
    public class QuizDto : IQuizDto
    {
        [Key]
        public int QuizId { get; set; }
    }
}
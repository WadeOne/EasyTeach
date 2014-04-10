using System;
using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities.Data;

namespace EasyTeach.Data.Entities
{
    public sealed class UserTokenDto : IUserTokenDto
    {
        [Key]
        public int UserTokenId { get; set; }

        public string Puprose { get; set; }

        public string Token { get; set; }

        public int UserId { get; set; }

        public DateTime Created { get; set; }
    }
}
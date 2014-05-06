﻿using System.ComponentModel.DataAnnotations;

using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Validation.Attributes;

namespace EasyTeach.Core.Entities
{
    public sealed class User : IUserModel
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public IGroupModel Group { get; set; }

        [Required]  
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Not valid Email address")]
        [UniqueEmail]
        public string Email { get; set; }
    }
}
﻿using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Services;
using EasyTeach.Core.Enums;

namespace EasyTeach.Web.Models
{
    public sealed class User : IUserModel
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Group Group { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Not valid Email address")]
        public string Email { get; set; }

        public bool EmailIsValidated { get; set; }

        [EnumDataType(typeof(UserType))]
        public UserType UserType { get; set; }
    }
}
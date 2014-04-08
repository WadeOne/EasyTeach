﻿using System.ComponentModel.DataAnnotations;
using EasyTeach.Core.Entities;
using EasyTeach.Core.Entities.Data;
using EasyTeach.Core.Enums;
using Microsoft.AspNet.Identity;

namespace EasyTeach.Data.Entities
{
    public sealed class UserDto : IUserDto
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Group Group { get; set; }

        public string Email { get; set; }

        public bool EmailIsValidated { get; set; }

        public UserType UserType { get; set; }

        public string PasswordHash { get; set; }

        int IUser<int>.Id
        {
            get { return UserId; }
        }

        string IUser<int>.UserName
        {
            get { return Email; }
            set { Email = value; }
        }
    }
}

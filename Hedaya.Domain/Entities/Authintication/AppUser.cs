﻿using Hedaya.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Hedaya.Domain.Entities.Authintication
{
    public class AppUser : IdentityUser
    {
        public required string Name { get; set; }
        public UserType UserType { get; set; }
        public string? SecurityCode { get; set; }
        public bool Deleted { get; set; }
      

    } 
}

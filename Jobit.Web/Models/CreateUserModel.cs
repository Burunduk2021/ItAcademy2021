using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Jobit.Web.Models
{
    public class CreateUserModel
    {
        [Required]
        public string UserFirstName { get; set; }
        //[Required]
        //public string UserLastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class LoginModel 
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }

    public class RoleEditModel
    { 
    public IdentityRole Role { get; set; }
    public IEnumerable<AppUser> Members { get; set; }
    public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    { 
    public string RoleName { get; set; }
    public string RoleId { get; set; }

    public string[] IdsToAdd { get; set; }

    public string[] IdsToDelete { get; set; }

    }


}

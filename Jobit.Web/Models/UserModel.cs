using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Jobit.Web.Models
{

   public class UserModel
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public Genders? Gender { get; set; }
        public Regions? Region { get; set; }
        public int? Age { get; set; }
        public int? Experience { get; set; }

    }
 
}

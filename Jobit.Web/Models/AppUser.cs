using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Jobit.Web.Models
{
    public class AppUser : IdentityUser
    {
        public string LastName { get; set; }
        public Genders? Gender { get; set; }
        public Regions? Region { get; set; }
        public int? Age { get; set; }
        public int? Experience { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}

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
        [StringLength(20)]
        [RegularExpression(@"^[а-яА-ЯёЁa-zA-Z ]+$", ErrorMessage = "Use only Latin and Cyrillic characters in nikname, first names and surnames!")]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
             
        public string Password { get; set; }

        [RegularExpression(@"(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}", ErrorMessage = "Password must have at least 6 characters, include upper and lower case Latin letters, numbers, special characters!")]
        public string NewPassword { get; set; }

        [StringLength(100)]
        [RegularExpression(@"^[а-яА-ЯёЁa-zA-Z ]+$", ErrorMessage = "Use only Latin and Cyrillic characters in nikname, first names and surnames!")]
        public string UserLastName { get; set; }

        [RangeAttribute(10,70, ErrorMessage= "Age can be specified in the range [10 - 70] full years")]
        public int? Age { get; set; }

        [RangeAttribute(0, 50, ErrorMessage = "Work experience can be specified in the range [0 - 50] full years")]
        public int? Experience { get; set; }

        public Genders? Gender { get; set; }

        public Regions? Region { get; set; }

    }
 
}

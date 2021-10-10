using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobit.DAL.Entities.Identity
{
    public static class AppUserRole
    {
        public const string admin = "администратор";
        public const string user = "пользователь";

        public static readonly string[] rolesArray = new string[] { admin, user };
    }
}

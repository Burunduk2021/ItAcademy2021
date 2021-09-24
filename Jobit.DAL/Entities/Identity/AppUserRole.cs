using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobit.DAL.Entities.Identity
{
    public static class AppUserRole
    {
        public static readonly string admin = "администратор";
        public static readonly string user = "пользователь";

        public static readonly string[] rolesArray = new string[] { admin, user };
    }
}

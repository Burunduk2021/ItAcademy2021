using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Jobit.Web.Models;
using Jobit.DAL.Entities.Identity;

namespace Jobit.Web.Infrastructure
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class UsersTagHelper : TagHelper
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;

        public UsersTagHelper(UserManager<AppUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            userManager = userMgr;
            roleManager = roleMgr;
        }

        [HtmlAttributeName("user-role")]
        public string userId { get; set; }

        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> roleNames = new List<string>();
            AppUser user = await userManager.FindByIdAsync(userId);
            RolesViewModel usersRoles = new RolesViewModel();
            usersRoles.Roles = await userManager.GetRolesAsync(user);
            foreach (var x in usersRoles.Roles)
            {
                roleNames.Add((string)x);
            }

            output.Content.SetContent(roleNames.Count == 0 ? "Нет роли" : string.Join(", ", roleNames));
        }
    }
}

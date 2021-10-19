using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Jobit.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Jobit.BLL.Models.Identity;

namespace Jobit.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> usrMgr, RoleManager<IdentityRole> roleMgr, SignInManager<AppUser> signinMrg)
        {
            userManager = usrMgr;
            roleManager = roleMgr;
            signInManager = signinMrg;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl) 
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(details.Email);
                if (user != null) {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, details.Password, false, false);
                    if (result.Succeeded) 
                    {
                        string userRole = (await userManager.GetRolesAsync(user)).First();
                        if (userRole == UserRoles.Admin)
                            return RedirectToAction("Index",  "Admin");
                        else // userRole = UserRoles.User
                            return RedirectToAction("UserProps", "User");
                    }
                }
            ModelState.AddModelError(nameof(LoginModel.Email), "Неверный логин или пароль.");
            }
            return View(details);
        }


        [AllowAnonymous]
        public ViewResult Registration()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserModel model) 
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.UserFirstName,
                    LastName = model.UserLastName,
                    Email = model.Email,
                    Gender = model.Gender,
                    Region = model.Region,
                    Age = model.Age,
                    Experience = model.Experience
                };

                IdentityResult result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, DAL.Entities.Identity.AppUserRole.user);
                    return RedirectToAction("Index", "Home");  
                }
                else
                {
                foreach (IdentityError error in result.Errors)
                    { ModelState.AddModelError("", error.Description); }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

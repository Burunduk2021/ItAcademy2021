using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Jobit.BLL.Models;
using Jobit.Web.Models;
using Jobit.BLL.Models.Identity;
using Jobit.Web.Infrastructure.FileLogger;
using ElmahCore;



namespace Jobit.Web.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;

        public AdminController(ILogger<HomeController> logger, 
            UserManager<AppUser> usrMgr, 
            RoleManager<IdentityRole> roleMgr,
            IUserValidator<AppUser> userValid,
            IPasswordValidator<AppUser> passwordValid,
            IPasswordHasher<AppUser> passwordHash)
        {
            _logger = logger;
            userManager = usrMgr;
            roleManager = roleMgr;
            userValidator = userValid;
            passwordValidator = passwordValid;
            passwordHasher = passwordHash;
        }

        public async Task<IActionResult> Index()
        {
            AppUser curUser = await userManager.GetUserAsync(User);
             _logger.LogWarning(JobitLogEvents.UserManagement, $"Пользователь [e-mail: {curUser.Email}] зашел в аккаунт {DateTime.Now.ToLocalTime()}");
             this.LogParams((nameof(curUser.Email), curUser.Email));
             ElmahExtensions.RiseError(new Exception("Действия администратора"));

            return View(); 
        }

        public ViewResult UserManagement()
        {

            return View(userManager.Users);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(string id)
        {
            //как вариант можно сделать изменение ролей через System.Web.Security
            IdentityResult result;
            string deletedRole = string.Empty;
            string addedRole = string.Empty;
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (await userManager.IsInRoleAsync(user, UserRoles.User))
                {
                    deletedRole = UserRoles.User;
                    addedRole = UserRoles.Admin;
                }
                else
                {
                    deletedRole = UserRoles.Admin;
                    addedRole = UserRoles.User;
                }
                result = await userManager.RemoveFromRoleAsync(user, deletedRole);
                result = await userManager.AddToRoleAsync(user, addedRole);
            }        
            else
            {
                ModelState.AddModelError("", "Пользователь не найден!");
            }
            return View("UserManagement", userManager.Users);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement");
                }
                else { AddErrorsFromResult(result); }
            }
            else 
            {
                ModelState.AddModelError("", "Пользователь не найден!");
            }
            return View("UserManagement", userManager.Users);
        }

        private Task<AppUser> CurrentUser => userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public async Task<IActionResult> AdminProps()
        {
            string temp = TempData["PassError"] as string;
            if (!string.IsNullOrEmpty(temp))
            {
                ModelState.AddModelError("", temp);
            }

            //второй вариант получения текущего пользователя
            AppUser curUser = await userManager.GetUserAsync(User);
            //AppUser curUser = await CurrentUser;
            UserModel editableUser = new UserModel();
            editableUser.UserFirstName = curUser.UserName;
            editableUser.UserLastName = curUser.LastName;
            editableUser.Email = curUser.Email;
            editableUser.Age = curUser.Age;
            editableUser.Experience = curUser.Experience;
            editableUser.Region = curUser.Region;
            editableUser.Gender = curUser.Gender;
            return View(editableUser);
        }


        [HttpPost]
        public async Task<RedirectToActionResult> AdminProps(UserModel user)
        {
            if (ModelState.IsValid)
            {
                AppUser updateUser = await CurrentUser;
                updateUser.UserName = user.UserFirstName;
                updateUser.LastName = user.UserLastName;
                updateUser.Gender = user.Gender;
                updateUser.Age = user.Age;
                updateUser.Experience = user.Experience;
                updateUser.Region = user.Region;

                updateUser.Email = user.Email;
                IdentityResult validAdmin = await userValidator.ValidateAsync(userManager, updateUser);
                if (!validAdmin.Succeeded)
                {
                    AddErrorsFromResult(validAdmin);
                }

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(user.NewPassword))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, updateUser, user.NewPassword);

                    //Версия 1. Для сменя пароля не надо подтверждать старый пароль
                    if (validPass.Succeeded)
                    {
                        updateUser.PasswordHash = passwordHasher.HashPassword(updateUser, user.NewPassword);
                    }
                    else
                    {
                        string msg = "Не удалось сменить пароль. Новый пароль не соответствует требованиям!";
                        TempData["PassError"] = msg;
                        AddErrorsFromResult(validPass);
                    }
                }

                if (validAdmin.Succeeded)
                {
                    IdentityResult result = await userManager.UpdateAsync(updateUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AdminProps");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            return RedirectToAction(nameof(AdminProps));
        }

        private void AddErrorsFromResult(IdentityResult result) {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

    }
}

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
using Jobit.Web.Models;
using Jobit.BLL.Models.Identity;
using System.Security.Claims;

namespace Jobit.Web.Controllers
{
    [Authorize(Roles = UserRoles.User)]
    public class UserController : Controller
    {
        private UserManager<AppUser> userManager;
        private IUserValidator<AppUser> userValidator;
        private IPasswordValidator<AppUser> passwordValidator;
        private IPasswordHasher<AppUser> passwordHasher;


        public UserController(UserManager<AppUser> usrMgr, 
               IUserValidator<AppUser> userValid, 
               IPasswordValidator<AppUser> passwordValid,
               IPasswordHasher<AppUser> passwordHash)
        {
            userManager = usrMgr;
            userValidator = userValid;
            passwordValidator = passwordValid;
            passwordHasher = passwordHash;
        }

        private Task<AppUser> CurrentUser => userManager.FindByNameAsync(HttpContext.User.Identity.Name);

        public async Task<IActionResult> UserProps()
        {
            string temp = TempData["PassError"] as string;
            if (!string.IsNullOrEmpty(temp))
            { 
                ModelState.AddModelError("", temp); 
            }
            //второй вариант получения текущего пользователя
            //AppUser curUser1 = await userManager.GetUserAsync(User);
            AppUser curUser = await CurrentUser;
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
        public async Task<RedirectToActionResult> UserProps(UserModel user)
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
                IdentityResult validUser = await userValidator.ValidateAsync(userManager, updateUser);
                if (!validUser.Succeeded)
                { 
                    AddErrorsFromResult(validUser); 
                }

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(user.NewPassword))
                { 
                    validPass = await passwordValidator.ValidateAsync(userManager, updateUser, user.NewPassword);

                    //Версия 2. Использование старого пароля при назначении нового
                    if (validPass.Succeeded)
                    {
                        if (!userManager.CheckPasswordAsync(updateUser, user.Password).Result)
                        {
                            string msg = "Не удалось сменить пароль. Старый пароль введен неверно!";
                            TempData["PassError"] = msg;

                        }
                        else
                        { 
                            IdentityResult result = await userManager.ChangePasswordAsync(updateUser, user.Password, user.NewPassword);
                            if (!result.Succeeded)
                            {
                                AddErrorsFromResult(result);
                            }
                        }
                    }
                    else
                    {
                        string msg = "Не удалось сменить пароль. Новый пароль не соответствует требованиям!";
                        TempData["PassError"] = msg;
                        AddErrorsFromResult(validPass);
                    }
                }

                    if (validUser.Succeeded)
                    {
                    IdentityResult result = await userManager.UpdateAsync(updateUser);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserProps");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            return RedirectToAction(nameof(UserProps));
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}

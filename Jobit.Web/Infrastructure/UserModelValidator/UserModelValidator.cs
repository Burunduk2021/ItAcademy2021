using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Jobit.Web.Models;

namespace Jobit.Web.Infrastructure.UserModelValidator
{
    public class UserModelValidator : AbstractValidator<UserViewModel>
    {
        public UserModelValidator() 
            {
            RuleFor(u => u.UserName).NotEmpty().Length(1, 20).WithMessage("Псевдоним не должен превышать 20 символов"); 
            RuleFor(u => u.Email).EmailAddress().WithMessage("Ошибка в написании e-mail!");
            RuleFor(u => u.Password).Matches("(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}").WithMessage("Пароль должен иметь не меньше 6 знаков, включать латинские буквы верхнего и нижнего регистра, числа, специальные знаки");
            RuleFor(u => u.NewPassword).Matches("(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}").WithMessage("Пароль должен иметь не меньше 6 знаков, включать латинские буквы верхнего и нижнего регистра, числа, специальные знаки");
            RuleFor(u => u.UserLastName).Matches("^[а-яА-ЯёЁa-zA-Z ]+$").WithMessage("Используйте латиницу / кириллицу для написания имени");
            RuleFor(u => u.Age).InclusiveBetween(10, 70).WithMessage("Возраст должен быть задан в интервале [10 - 70]");
            RuleFor(u => u.Experience).InclusiveBetween(0, 50).WithMessage("Опыт работы должен быть задан в интервале [0 - 50]"); ;
            }

    }
}

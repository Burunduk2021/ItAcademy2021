using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Jobit.Web.Models
{
    public enum Regions
    {
        [Display(Name = "г.Минск")]
        cityMinsk,
        [Display(Name = "Минская область")]
        regMinsk,
        [Display(Name = "Витебская область")]
        regVitebsk,
        [Display(Name = "Брестская область")]
        regBrest,
        [Display(Name = "Гродненская область")]
        regGrodno,
        [Display(Name = "Гомельская область")]
        regGomel,
        [Display(Name = "Могилевская область")]
        regMogilev
    }
}

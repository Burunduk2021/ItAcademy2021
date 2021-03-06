using Jobit.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Jobit.Web.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None, Duration = 0)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Search()
        {
            List<Vacancy> vacancies = GetVacanciesList();
            return View(vacancies);
        }

        private List<Vacancy> GetVacanciesList()
        {
            List<Vacancy> vac = new List<Vacancy>();
            vac.Add(new Vacancy(){Profession="Верстальщик", Region=Regions.cityMinsk.ToString() });
            vac.Add(new Vacancy() { Profession = "Тестировщик", Region = Regions.regBrest.ToString() });
            vac.Add(new Vacancy() { Profession = "Dev ops", Region = Regions.regGrodno.ToString() });
            return vac;
        }


    }
}

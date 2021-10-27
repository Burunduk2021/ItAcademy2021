using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobit.BLL.DTO
{
    class VacancyDTO
    {
        public string Profession { get; set; }
        public string Region { get; set; }
        public string Technologies { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string LinkToOriginalSource { get; set; }
        public double? Salary { get; set; }
        public int? Experience { get; set; }
    }
}

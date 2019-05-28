using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class IncomesOverviewVm
    {
        public IEnumerable<Income> Incomes { get; set; }

        public int Total { get; set; }
        public int Variable { get; set; }
        public int WorkHours { get; set; }
    }
}

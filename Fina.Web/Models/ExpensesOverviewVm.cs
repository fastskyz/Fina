using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class ExpensesOverviewVm
    {
        public IEnumerable<single_expense> Expenses { get; set; }

        public int Total {
            get { return Total; }
            set { Expenses.Count(); }
            }
    }
}

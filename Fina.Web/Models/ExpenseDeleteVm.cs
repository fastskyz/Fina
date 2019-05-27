using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class ExpenseDeleteVm
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public bool Life { get; set; }
        public Expense.ExpenseType Type { get; set; }
        public bool Variable { get; set; }
        public decimal Cost { get; set; }
    }
}

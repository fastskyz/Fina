﻿using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class ExpensesOverviewVm
    {
        public IEnumerable<Expense> Expenses { get; set; }

        public int Total { get; set; }
        public int Variable { get; set; }
        public int Life { get; set; }
    }
}

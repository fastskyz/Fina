using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SavingsOverviewVm
    {
        public IEnumerable<Saving> Savings { get; set; }

        public decimal Total { get; set; }
        public decimal Monthly { get; set; }
        public int nSavings { get; set; }
    }
}

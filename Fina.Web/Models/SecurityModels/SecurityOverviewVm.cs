using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SecurityOverviewVm
    {
        public IEnumerable<Security> Securities { get; set; }

        public decimal Total { get; set; }
        public decimal Monthly { get; set; }
        public int nSecurities { get; set; }
    }
}

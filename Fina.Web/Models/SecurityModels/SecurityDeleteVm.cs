using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SecuritiesDeleteVm
    {
        public long Id { get; set; }
        
        public decimal Amount { get; set; }
        public bool Longterm { get; set; }
        public decimal Monthly { get; set; }
        public string Name { get; set; }
        public Security.securitiesType Type { get; set; }
    }
}

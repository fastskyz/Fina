using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SecurityDeleteVm
    {
        public long Id { get; set; }
        
        public decimal Amount { get; set; }
        public Security.secure_type Type { get; set; }
        public decimal Monthly { get; set; }
        public DateTime StartDate { get; set; }

    }
}

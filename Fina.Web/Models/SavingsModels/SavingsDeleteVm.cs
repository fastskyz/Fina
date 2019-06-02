using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SavingsDeleteVm
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public bool Variable { get; set; }
        [Required]
        public int WorkHours { get; set; }
    }
}

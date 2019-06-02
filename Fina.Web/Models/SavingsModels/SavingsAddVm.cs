using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SavingsAddVm
    {
        [Required]
        public bool Longterm { get; set; }
        [Required]
        public decimal Monthly { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Saving.savingsType Type { get; set; }

        public string Description { get; set; }
        public string StartDate { get; set; }
        public string AccountNumber { get; set; }

        public string[] savingsType = Enum.GetNames(typeof(Saving.savingsType));

    }
}

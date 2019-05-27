using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fina.Lib.Database
{
    public class Incomes : baseEntity<User>
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public bool Variable { get; set; }
        [Required]
        public int WorkHours { get; set; }

        // optional
        public string Function { get; set; }
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class jobs : baseEntityFK<incomes>
    {
        [Required]
        public int Income { get; set; }
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

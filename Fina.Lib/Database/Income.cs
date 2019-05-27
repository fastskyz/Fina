using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fina.Lib.Database
{
    public class Income : baseEntity<User>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Amount { get; set; }
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

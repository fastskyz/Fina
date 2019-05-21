using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class jobs
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public int Income { get; set; }
        [Required]
        public bool Variable { get; set; }
        [Required]
        public int WorkHours { get; set; }
        
        public string Function { get; set; }
        public string Company { get; set; } 
        public DateTime StartDate { get; set; }
    }
}

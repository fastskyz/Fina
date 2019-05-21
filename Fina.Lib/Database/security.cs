using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class security
    {
        public enum secure_type { Guard, Runway }

        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public int Amount { get; set; }
        [Required]
        public secure_type Type { get; set; }
        [Required]
        public int Monthly { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        
        public string Description { get; set; }
        public string AccountNumber { get; set; }
    }
}

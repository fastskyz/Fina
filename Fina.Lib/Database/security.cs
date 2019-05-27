using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fina.Lib.Database
{
    public class Security : baseEntity<User>
    {

        public enum secure_type { Guard, Runway }

        [Required]
        public decimal Amount { get; set; }
        [Required]
        public secure_type Type { get; set; }
        [Required]
        public decimal Monthly { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        
        public string Description { get; set; }
        public string AccountNumber { get; set; }
    }
}

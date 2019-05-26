using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class users
    { 
        public enum Currencies { Euro, Dollar, Pounds, Yen }
        public enum Countries { Belgium, Netherlands, France, Germany, Engeland, USA, Japan}

        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Countries Country { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public byte Age { get; set; }
        [Required]
        public Currencies Currency { get; set; }
    }
}

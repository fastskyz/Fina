using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SignUpVm
    {
        public enum Currencies { Euro, Dollar, Pounds, Yen }
        public enum Countries { Belgium, Netherlands, France, Germany, Engeland, USA, Japan }

        [Required]
        public string Name { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public byte Age { get; set; }
        [Required]
        public Currencies Currency { get; set; }
        [Required]
        public Countries Country { get; set; }

        public string[] countries = Enum.GetNames(typeof(Countries));
        public string[] currencies = Enum.GetNames(typeof(Currencies));
    }
}

using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class SecurityAddVm
    {
        [Required]
        public Security.secure_type Type { get; set; }
        [Required]
        public decimal Monthly { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public string Description { get; set; }
        public string AccountNumber { get; set; }

        public string[] securityTypes = Enum.GetNames(typeof(Security.secure_type));

    }
}

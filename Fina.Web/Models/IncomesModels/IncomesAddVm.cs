﻿using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class IncomesAddVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Life { get; set; }
        [Required]
        public Incomes.IncomesType Type { get; set; }
        [Required]
        public bool Variable { get; set; }
        [Required]
        public decimal Cost { get; set; }

        public string AccountNumber { get; set; }
        public string Creditor { get; set; }

        public string[] expenseTypes = Enum.GetNames(typeof(Incomes.IncomesType));
    }
}

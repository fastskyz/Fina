﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class User
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



        [Required]
        public decimal Total { get; set; }
        [Required]
        public decimal LifeFunds { get; set; }
        [Required]
        public decimal Positive { get; set; }
        [Required]
        public decimal Negative { get; set; }


        public ICollection<Saving> Savings { get; set; }
        public ICollection<Security> Securities { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}

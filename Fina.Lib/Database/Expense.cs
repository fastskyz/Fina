using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fina.Lib.Database
{
    public class Expense : baseEntity<User>
    {
        public enum ExpenseType { Consumables, Rent, Car, Loan, Resources, Service, Sports, Subscription, Other }

        [Required]
        public string Name { get; set; }
        [Required]
        public bool Life { get; set; }
        [Required]
        public ExpenseType Type { get; set; }
        [Required]
        public bool Variable { get; set; }
        [Required]
        public decimal Cost { get; set; }

        public string AccountNumber { get; set; }
        public string Creditor { get; set; }
    }
}

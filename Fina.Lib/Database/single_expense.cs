using System;
using System.Collections.Generic;
using System.Text;

namespace Fina.Lib.Database
{
    public class single_expense
    {
        public enum ExpenseType { Consumables, Rent, Car, Loan, Resources, Service, Sports, Subscription, Other }

        public string Name { get; set; }
        public bool Life { get; set; }
        public ExpenseType Type { get; set; }
        public bool Variable { get; set; }
        public decimal Cost { get; set; }

        public string AccountNumber { get; set; }
        public string Creditor { get; set; }
    }
}

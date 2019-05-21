using System;
using System.Collections.Generic;
using System.Text;

namespace Fina.Lib.Database
{
    public class savings
    {
        public enum savingsType { Travel, Hobby, Childeren, Retirement, Other}

        public long Id { get; set; }

        public int Amount { get; set; }
        public bool Longterm { get; set; }
        public int Monthly { get; set; }
        public string Name { get; set; }
        public savingsType Type { get; set; }

        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string AccountNumber { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Fina.Lib.Database
{
    public class security
    {
        public enum secure_type { Guard, Runway }

        public long Id { get; set; }
        
        public int Amount { get; set; }
        public secure_type Type { get; set; }
        public int Monthly { get; set; }
        public DateTime StartDate { get; set; }
        
        public string Description { get; set; }
        public string AccountNumber { get; set; }
    }
}

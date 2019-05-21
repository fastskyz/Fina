using System;
using System.Collections.Generic;
using System.Text;

namespace Fina.Lib.Database
{
    public class jobs
    {
        public long Id { get; set; }

        public int Income { get; set; }
        public bool Variable { get; set; }
        public int WorkHours { get; set; }
        
        public string Function { get; set; }
        public string Company { get; set; } 
        public DateTime StartDate { get; set; }
    }
}

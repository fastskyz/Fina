using System;
using System.Collections.Generic;
using System.Text;

namespace Fina.Lib.Database
{
    public class users
    {
        public enum Currencies { Euro, Dollar, Pounds, Yen }
        public enum Countries { Belgium, Netherlands, France, Germany, Engeland, USA, Japan}

        public string name { get; set; }
        public string firstname { get; set; }
        public Countries country { get; set; }
        public string password { get; set; }
        public byte age { get; set; }
        public Currencies currency { get; set; }
    }
}

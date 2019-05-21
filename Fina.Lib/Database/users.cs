using System;
using System.Collections.Generic;
using System.Text;

namespace Fina.Lib.Database
{
    public class users
    { 
        public enum Currencies { Euro, Dollar, Pounds, Yen }
        public enum Countries { Belgium, Netherlands, France, Germany, Engeland, USA, Japan}

        public long Id { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public Countries Country { get; set; }
        public string Password { get; set; }
        public byte Age { get; set; }
        public Currencies Currency { get; set; }
    }
}

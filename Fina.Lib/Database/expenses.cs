using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class expenses : baseEntityFK<users>
    {

        [Required]
        public int Total { get; set; }
        [Required]
        public int LifeFunds { get; set; }
    }
}

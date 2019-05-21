using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public abstract class baseEntityFK<FKey>
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public FKey FK { get; set; }
    }
}

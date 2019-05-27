using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fina.Lib.Database
{
    public abstract class baseEntityFK<FKey>
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        [ForeignKey("users")]
        public FKey FK { get; set; }
    }
}

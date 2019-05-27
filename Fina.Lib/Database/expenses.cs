using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fina.Lib.Database
{
    public class expenses
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        [ForeignKey("users")]
        public users FK { get; set; }

        [Required]
        public int Total { get; set; }
        [Required]
        public int LifeFunds { get; set; }

        public ICollection<single_expense> Singles { get; set; }
    }
}

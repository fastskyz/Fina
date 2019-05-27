using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Fina.Lib.Database
{
    public class incomes
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
        public int TotalWorkHours { get; set; }

        public ICollection<jobs> Jobs { get; set; }
    }
}

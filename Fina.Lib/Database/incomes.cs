using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class incomes
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public int Total { get; set; }
        [Required]
        public int TotalWorkHours { get; set; }
    }
}

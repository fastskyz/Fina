﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class incomes : baseEntityFK<users>
    { 
        [Required]
        public int Total { get; set; }
        [Required]
        public int TotalWorkHours { get; set; }
    }
}

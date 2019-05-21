﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fina.Lib.Database
{
    public class savings : baseEntityFK<users>
    {
        public enum savingsType { Travel, Hobby, Childeren, Retirement, Other}

        [Required]
        public int Amount { get; set; }
        [Required]
        public bool Longterm { get; set; }
        [Required]
        public int Monthly { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public savingsType Type { get; set; }

        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string AccountNumber { get; set; }
    }
}

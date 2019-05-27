using Fina.Lib.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Web.Models
{
    public class DetailsVm
    {
        public users user { get; set; }
        public expenses negative { get; set; }
        public incomes positive { get; set; }
    }
}

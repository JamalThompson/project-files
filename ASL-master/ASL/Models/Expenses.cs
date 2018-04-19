using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Total {get; set;}
        public int SvcId { get; set; }
        public string SvcType { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Models
{
    public class InvoiceLineItem
    {
        public int Id { get; set; }
        public int InvNum { get; set; }
        public int CustId { get; set; }
        public int Description { get; set; }
        public decimal Total { get; set; }
        public int SvcId { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ASL.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace ASL.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvNum { get; set; }
        public int CustId { get; set; }
        public string Customer { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        
        //uses tinyint in MySQL 0-no payment, 1-partial Payment, 2-paid in full
        public int Paid { get; set; }
        public List<InvoiceLineItem> LineItems {get;set;}
        public DateTime Bill_Date { get; set; }
        public DateTime Due_Date { get; set; }
        public DateTime Paid_Date { get; set; }
        public bool IsActive { get; set; }

    }
}

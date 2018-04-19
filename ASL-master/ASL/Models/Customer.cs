using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ASL.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ASL.Models
{
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public int BillCycle { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_Address { get; set; }
        [Display(Name = "Phone Number")]
        public string Phone_Number { get; set; }
        public bool IsActive { get; set; }

    }
}

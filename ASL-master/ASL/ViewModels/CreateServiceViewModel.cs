using ASL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.ViewModels
{
    public class CreateServiceViewModel
    {
        public List<SelectListItem> Cust_Id { get; set;  }
        public Service ServiceCreate { get; set; }

        public CreateServiceViewModel()
        {
            Cust_Id = new List<SelectListItem>();
            ServiceCreate = new Service();
        }

       // public Details details { get; set; }

      

        public void PopulateCustomers (IEnumerable<Customer> customer)
        {
            foreach(Customer cust in customer)
            {
                Cust_Id.Add(new SelectListItem { Value = cust.Id.ToString(), Text = cust.Name });
            } 
        }

    }
}

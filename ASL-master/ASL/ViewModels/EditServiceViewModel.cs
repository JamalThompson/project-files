using ASL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.ViewModels
{
    public class EditServiceViewModel
    {
            [Display(Name = "Customer Name")]
            public string Cust_Id { get; set; }

            public Service ServiceEdit { get; set; }

            public EditServiceViewModel()
            {
              //  string Cust_Id;
                ServiceEdit = new Service();
            }
        

            public void setName(string name)
            {
                Cust_Id = name;
            }

    }

}


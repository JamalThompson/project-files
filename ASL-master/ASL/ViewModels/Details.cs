using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.ViewModels
{
    public class Details
    {
        public int Id { get; set; }

        [Display(Name = "Customer Name")]
        public int Cust_Id { get; set; }

        [Display(Name = "Service Type")]
        public string SvcType { get; set; }
        public string Address1 { get; set; }

        [Display(Name = "Billing Address")]
        public string Address2 { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone_Number { get; set; }

        [Display(Name = "Service Description")]
        public string SvcDescription { get; set; }

        [Display(Name = "Scheduled Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Sched_Date { get; set; }


        [Display(Name = "Completion Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime comp_date { get; set; }

        public decimal BillSubtotal { get; set; }
        public bool IsActive { get; set; }


    }




}

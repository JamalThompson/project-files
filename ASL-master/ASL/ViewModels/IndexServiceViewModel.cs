using ASL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.ViewModels
{
    public class IndexServiceViewModel
    {
       

        public  Customer CustInfo { get; set; }
        public Service ServiceInfo { get; set; }
      

        public IndexServiceViewModel()
        {
            CustInfo = new Customer();
            ServiceInfo = new Service();
        }

    }
}

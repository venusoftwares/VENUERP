using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VENUERP.ViewModels.ERP
{
    public  class CustomerMasterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo { get; set; }
        public string GSTNo { get; set; }
    }
}

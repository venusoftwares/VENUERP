using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;
using VENUERP.Repository.Interface;
using VENUERP.ViewModels.ERP;

namespace VENUERP.Repository.Repository
{
    public class CustomerMastersRepository : ICustomerMasters
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<CustomerMasterViewModel> GetCustomerrMasterDetails()
        {
            var result = db.CustomerMasters.Select(x => new CustomerMasterViewModel()
            {
                Id = x.CustomerId,
                Name = x.Name,
                Address = x.Address,
                ContactPerson = x.ContactPerson,
                MobileNo = x.ContactNo,
                GSTNo = x.GSTNo
            });
            return result;
        }
    }
}

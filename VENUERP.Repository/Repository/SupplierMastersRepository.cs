using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;
using VENUERP.Repository.Interface;
using VENUERP.ViewModels.ERP;
using VENUERP.ViewModels.JQUERYDATATABLES;

namespace VENUERP.Repository.Repository
{
    public  class SupplierMastersRepository : ISupplierMasters
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<SupplierMasterViewModel> GetSupplierMasterDetails()
        {
            
            var result = db.SupplierMasters.Select(x => new SupplierMasterViewModel()
            {
                Id = x.SupplierId,
                Name = x.SupplierName,
                Address = x.SupplierAddress,
                ContactPerson = x.ContactPerson,
                MobileNo = x.ContactNo,
                GSTNo=x.GSTNo
            });

            return result;
        }
    }
}

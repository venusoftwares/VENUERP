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
    public class BrandMasterRepository : IBrandMaster
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<BrandMasterViewModel> GetBrandMasterDetails()
        {
            var result = db.BrandMasters.Select(x => new BrandMasterViewModel() {
                Id = x.BrandId,
                Brand = x.BrandName
            });
            return result;
        }
    }
}

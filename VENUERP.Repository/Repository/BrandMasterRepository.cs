using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;
using VENUERP.Repository.Interface;

namespace VENUERP.Repository.Repository
{
    public class BrandMasterRepository : IBrandMaster
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<BrandMaster> GetBrandMasterDetails()
        {
            var result = db.BrandMasters.ToList();
            return result;
        }
    }
}

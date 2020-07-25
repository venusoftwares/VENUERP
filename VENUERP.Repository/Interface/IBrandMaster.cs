using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.Models;

namespace VENUERP.Repository.Interface
{
    public interface IBrandMaster
    {
        IEnumerable<BrandMaster> GetBrandMasterDetails();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.ViewModels.ERP;
using VENUERP.ViewModels.JQUERYDATATABLES;

namespace VENUERP.Repository.Interface
{
    public interface ISupplierMasters
    {
        IEnumerable<SupplierMasterViewModel> GetSupplierMasterDetails();
    }
}

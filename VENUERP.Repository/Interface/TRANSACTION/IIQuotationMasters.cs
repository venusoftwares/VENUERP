using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.ViewModels.TRANSACTION;

namespace VENUERP.Repository.Interface.TRANSACTION
{
    public  interface IIQuotationMasters
    {
        IEnumerable<QuoataionViewModel> GetIQuotationMasterDetails();
    }
}

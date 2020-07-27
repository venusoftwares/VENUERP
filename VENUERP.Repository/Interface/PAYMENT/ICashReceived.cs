using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VENUERP.ViewModels.PAYMENT;

namespace VENUERP.Repository.Interface.PAYMENT
{
    public interface ICashReceived
    {
        IEnumerable<CashReceivedViewModel> GetCashReceivedDetails();
    }
}

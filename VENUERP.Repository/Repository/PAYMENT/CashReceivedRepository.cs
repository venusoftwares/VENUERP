using System.Data;
using System.Collections.Generic;
using VENUERP.Models;
using VENUERP.Repository.Interface.TRANSACTION;
using VENUERP.ViewModels.TRANSACTION;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Mail;
using VENUERP.ViewModels.PAYMENT;
using VENUERP.Repository.Interface.PAYMENT;

namespace VENUERP.Repository.Repository.PAYMENT
{
    public class CashReceivedRepository : ICashReceived
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<CashReceivedViewModel> GetCashReceivedDetails()
        {
            var list = new List<CashReceivedViewModel>();
            var QuotationMasters = db.CashMasters.Include(p => p.CustomerMaster).Where(x=>x.Nature== "Receipt");
            var result = QuotationMasters.OrderByDescending(x => x.CashId).ToList();
            foreach (var x in result)
            {
                var b = new CashReceivedViewModel()
                {
                    Id = x.CashId,
                    Date = Convert.ToDateTime(x.Date).ToString("dd-MM-yyyy"),
                    No = x.VoucherNo,
                    Name = x.CustomerMaster.Name,
                    Amount = Convert.ToString(x.Amount)
                };
                list.Add(b);
            }
            return list;
        }
    }
}

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
    public class CashPaymentRepository : ICashPayment
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<CashPaymentViewModel> GetCashPaymentDetails()
        {
            var list = new List<CashPaymentViewModel>();
            var QuotationMasters = db.CashMasters.Include(p=>p.SupplierMaster).Where(x => x.Nature == "Payment");
            var result = QuotationMasters.OrderByDescending(x => x.CashId).ToList();
            foreach (var x in result)
            {
                var b = new CashPaymentViewModel()
                {
                    Id = x.CashId,
                    Date = Convert.ToDateTime(x.Date).ToString("dd-MM-yyyy"),
                    No = x.VoucherNo,
                    Name = x.SupplierMaster.SupplierName,
                    Amount = Convert.ToString(x.Amount)
                };
                list.Add(b);
            }
            return list;
        }
    }
}

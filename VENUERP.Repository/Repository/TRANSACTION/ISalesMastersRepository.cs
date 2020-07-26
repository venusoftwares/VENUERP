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

namespace VENUERP.Repository.Repository.TRANSACTION
{
    public class ISalesMastersRepository : IISalesMaster
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<SalesViewModel> GetISalesMasterDetails()
        {
            var list = new List<SalesViewModel>();
            var QuotationMasters = db.ISalesMaster.Include(p => p.CustomerMaster);
            var result = QuotationMasters.OrderByDescending(x => x.ISalesID).ToList();
            foreach (var x in result)
            {
                var b = new SalesViewModel()
                {
                    Id = x.ISalesID,
                    Date = Convert.ToDateTime(x.ISalesDate).ToString("dd-MM-yyyy"),
                    No = x.InvoiceNo,
                    Name = x.CustomerMaster.Name,
                    Amount = Convert.ToString(x.GrandTotal)
                };
                list.Add(b);
            }
            return list;
        }
    }
}

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
    public class PurchaseMastersRepository : IPurchaseMasters
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<PurchaseViewModel> GetPurchaseMasterDetails()
        {
            var list = new List<PurchaseViewModel>();
            var purchaseMasters  = db.PurchaseMasters.Include(p => p.SupplierMaster);
            var result = purchaseMasters.OrderByDescending(x => x.PurchaseID).ToList();
            foreach (var x in result)
            {
                var b = new PurchaseViewModel()
                {
                    Id = x.PurchaseID,
                    Date = Convert.ToDateTime(x.PurchaseDate).ToString("dd-MM-yyyy"),
                    No = x.InvoiceNo,
                    Name = x.SupplierMaster.SupplierName,
                    Amount = Convert.ToString(x.GrandTotal)
                };
                list.Add(b);
            }
            return list;
        }
    }
}

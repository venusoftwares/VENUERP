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
    public class IQuotationMastersRepository : IIQuotationMasters
    {
        public DatabaseContext db = new DatabaseContext();
        public IEnumerable<QuoataionViewModel> GetIQuotationMasterDetails()
        {
            var list = new List<QuoataionViewModel>();
            var QuotationMasters = db.IQuotationMaster.Include(p => p.CustomerMaster);
            var result = QuotationMasters.OrderByDescending(x => x.IQuotationID).ToList();
            foreach(var x in result)
            {
                var b = new QuoataionViewModel()
                {
                    Id = x.IQuotationID,
                    Date = Convert.ToDateTime(x.IQuotationDate).ToString("dd-MM-yyyy"),
                    No = Convert.ToString( x.IQuotationNo),
                    Name = x.CustomerMaster.Name,
                    Amount = Convert.ToString(x.GrandTotal)
                };
                list.Add(b);
            }            
            return list;
        }

        public IEnumerable<QuoataionViewModel> GetQuotationMasterDetails()
        {
            var list = new List<QuoataionViewModel>();
            var QuotationMasters = db.QuotationMasters.Include(p => p.CustomerMaster);
            var result = QuotationMasters.OrderByDescending(x => x.QuotationID).ToList();
            foreach (var x in result)
            {
                var b = new QuoataionViewModel()
                {
                    Id = x.QuotationID,
                    Date = Convert.ToDateTime(x.QuotationDate).ToString("dd-MM-yyyy"),
                    No = Convert.ToString(x.QuotationNo),
                    Name = x.CustomerMaster.Name,
                    Amount = Convert.ToString(x.GrandTotal)
                };
                list.Add(b);
            }
            return list;
        }
    }
}

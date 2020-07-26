using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VENUERP.Models;
 

namespace VENUERP.Controllers
{
    public class CascadingController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Cascading
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getCategory(int? BrandID)
        {
            if(BrandID == null) { BrandID = 0; }
            DatabaseContext db = new DatabaseContext();
            return Json(db.CategoryMasters.Where(x=>x.BrandId==BrandID).Select(x => new
            {
                CategoryID = x.CategoryId,
                CategoryName = x.CategoryName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getItem(int? CategoryId, int? BrandID)
        {
            if (CategoryId == null || BrandID==null) { CategoryId = 0; BrandID = 0; }
          
            return Json(db.ItemMasters.Where(x => x.CategoryId == CategoryId && x.BrandId == BrandID).Select(x => new
            {
                ItemId = x.ItemId,
                Description = x.Description 
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseTotal()
        {
            decimal?  PurchaseTot = 0;
            var tot = db.PurchaseItemDetails.Where(x => x.PurchaseID == 0).Select(x =>  x.Amount );
            foreach(var a in tot)
            {
                PurchaseTot = PurchaseTot + a;
            }
            return Json(PurchaseTot, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetItemRate(int? ID)
        {
            if(ID==null)            {                ID = 0;            }
            var tot = db.ItemMasters.Find(ID);
            return Json(tot.Rate,JsonRequestBehavior.AllowGet);
        }

    }
}
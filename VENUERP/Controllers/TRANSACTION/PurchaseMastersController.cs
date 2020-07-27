using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
 
using VENUERP.Models;
using VENUERP.ViewModels;
using VENUERP.Repository.Interface.TRANSACTION;
 
using VENUERP.Repository.Repository.TRANSACTION;
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.ViewModels.TRANSACTION;
using VENUERP.Providers;
using VENUERP.Repositoy.TRANSACTION;

namespace VENUERP.Controllers
{
    [Authentication]
    public class PurchaseMastersController : Controller
    {
        private readonly DatabaseContext db = new DatabaseContext();
        private readonly ISQLStored _sQLStored;
        private readonly IPurchaseMasters _purchaseMasters;
        public PurchaseMastersController()
        {
            this._sQLStored = new SQLStoredProcedure();
            this._purchaseMasters = new PurchaseMastersRepository();
        }
        // GET: PurchaseMasters
        public  ActionResult Index()
        {           
            return View();
        }

        // GET: PurchaseMasters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseMaster purchaseMaster = await db.PurchaseMasters.FindAsync(id);
            if (purchaseMaster == null)
            {
                return HttpNotFound();
            }
            return View(purchaseMaster);
        }

        // GET: PurchaseMasters/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName");
            return View();
        }

        public ActionResult DeletePartial(int? id, PurchaseMasterViewModel purchaseMasterViewModel)
        {
            var De = db.PurchaseItemDetails.Find(id);
            db.PurchaseItemDetails.Remove(De);
            db.SaveChanges();
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", purchaseMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", purchaseMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", purchaseMasterViewModel.ItemID);

            if (id == null) { id = 0; }
            var List = db.PurchaseItemDetails.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.PurchaseID == 0).ToList();
            return  RedirectToAction("Create");
        }

        public ActionResult PurchaseItem(int? id, PurchaseMasterViewModel purchaseMasterViewModel)
        {
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", purchaseMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", purchaseMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", purchaseMasterViewModel.ItemID);
            if (id == null) { id = 0; }
            var List = db.PurchaseItemDetails.Include(i=>i.ItemMaster).Include(i=>i.CategoryMaster).Include(i=>i.brandMaster)
                .Where(x => x.PurchaseID == id).ToList();
            return PartialView("_PurchaseItem", List);
        }
        // POST: PurchaseMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PurchaseID,PurchaseDate,InvoiceNo" +
            ",SupplierId,IsCash,CGSTAmt,CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt" +
            ",TotalGST,GrandTotal")] PurchaseMaster purchaseMaster,PurchaseMasterViewModel purchaseMasterViewModel,string Select)
        {
           
            if(Select == "ADD")
            {
                if (purchaseMasterViewModel.Qty != null &&
               purchaseMasterViewModel.Rate != null &&
               purchaseMasterViewModel.Amount != null && purchaseMasterViewModel.ItemID!=0)
                {
                    PurchaseItemDetail purchaseItemDetail = new PurchaseItemDetail();
                    purchaseItemDetail.PurchaseID = 0;
                    purchaseItemDetail.BrandID = purchaseMasterViewModel.BrandID;
                    purchaseItemDetail.CategoryID = purchaseMasterViewModel.CategoryID;
                    purchaseItemDetail.ItemID = purchaseMasterViewModel.ItemID;
                    purchaseItemDetail.Dimension = db.ItemMasters.Where(x => x.ItemId == purchaseMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    purchaseItemDetail.Quantity = purchaseMasterViewModel.Qty;
                    purchaseItemDetail.Rate = purchaseMasterViewModel.Rate;
                    purchaseItemDetail.Amount = purchaseMasterViewModel.Amount;
                    purchaseItemDetail.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.PurchaseItemDetails.Add(purchaseItemDetail);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                if (ModelState.IsValid)
                {
                    purchaseMaster.SupplierId = purchaseMasterViewModel.SupplierId;
                    purchaseMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.PurchaseMasters.Add(purchaseMaster);
                    await db.SaveChangesAsync();
                    var list = db.PurchaseItemDetails.Where(x => x.PurchaseID == 0).ToList();
                    foreach(var i in list)
                    {
                        PurchaseItemDetail purchaseItemDetail = db.PurchaseItemDetails.Find(i.PurchaseDetailID);
                        purchaseItemDetail.PurchaseID = purchaseMaster.PurchaseID;
                        purchaseItemDetail.ComCode = Convert.ToInt32(Session["ComCode"]);
                        db.Entry(purchaseItemDetail).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    } 
                    return RedirectToAction("Index");
                }
            }
            if (Select == "Reset")
            {

            }
            if (Select == "Delete")
            {
                    var a = db.PurchaseItemDetails.Where(x => x.PurchaseID == 0).ToList();
                    db.PurchaseItemDetails.RemoveRange(a);
                    await db.SaveChangesAsync();
               
                return RedirectToAction("Index");
            }

           
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", purchaseMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", purchaseMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", purchaseMasterViewModel.ItemID);


            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName");
            return View(purchaseMasterViewModel);
        }

        // GET: PurchaseMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            PurchaseMasterViewModel purchaseMasterViewModel = new PurchaseMasterViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseMaster purchaseMaster = await db.PurchaseMasters.FindAsync(id);
            if (purchaseMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName" );
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName" );
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName" );
            purchaseMasterViewModel.purchaseMaster = purchaseMaster;
            return View(purchaseMasterViewModel);
        }

        // POST: PurchaseMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PurchaseID,PurchaseDate,InvoiceNo,SupplierId,IsCash,CGSTAmt,CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] PurchaseMaster purchaseMaster, PurchaseMasterViewModel purchaseMasterViewModel, string Select)
        {
            if (Select == "ADD")
            {
                if (purchaseMasterViewModel.Qty != null &&
               purchaseMasterViewModel.Rate != null &&
               purchaseMasterViewModel.Amount != null && purchaseMasterViewModel.ItemID != 0)
                {
                    PurchaseItemDetail purchaseItemDetail = new PurchaseItemDetail();
                    purchaseItemDetail.PurchaseID = purchaseMasterViewModel.purchaseMaster.PurchaseID;
                    purchaseItemDetail.BrandID = purchaseMasterViewModel.BrandID;
                    purchaseItemDetail.CategoryID = purchaseMasterViewModel.CategoryID;
                    purchaseItemDetail.ItemID = purchaseMasterViewModel.ItemID;
                    purchaseItemDetail.Dimension = db.ItemMasters.Where(x => x.ItemId == purchaseMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    purchaseItemDetail.Quantity = purchaseMasterViewModel.Qty;
                    purchaseItemDetail.Rate = purchaseMasterViewModel.Rate;
                    purchaseItemDetail.Amount = purchaseMasterViewModel.Amount;
                    purchaseItemDetail.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.PurchaseItemDetails.Add(purchaseItemDetail);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                try
                {
                    purchaseMaster.SupplierId = purchaseMasterViewModel.SupplierId;
                    purchaseMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.Entry(purchaseMaster).State = EntityState.Modified;
          
                    await db.SaveChangesAsync();
               
                    return RedirectToAction("Index");
                }
                catch
                {

                }

                   
                
            }
            if (Select == "Reset")
            {

            }
            //if (Select == "Delete")
            //{
            //    var a = db.PurchaseItemDetails.Where(x => x.PurchaseID == 0).ToList();
            //    db.PurchaseItemDetails.RemoveRange(a);
            //    await db.SaveChangesAsync();

            //    return RedirectToAction("Index");
            //}
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.SupplierId = new SelectList(db.SupplierMasters, "SupplierId", "SupplierName", purchaseMaster.SupplierId);
            return View(purchaseMasterViewModel);
        }

        // GET: PurchaseMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseMaster purchaseMaster = await db.PurchaseMasters.FindAsync(id);
            if (purchaseMaster == null)
            {
                return HttpNotFound();
            }
            return View(purchaseMaster);
        }

        // POST: PurchaseMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            
            PurchaseMaster purchaseMaster = await db.PurchaseMasters.FindAsync(id);
            var de = db.PurchaseItemDetails.Where(x => x.PurchaseID == purchaseMaster.PurchaseID).ToList();
            db.PurchaseItemDetails.RemoveRange(de);
            db.PurchaseMasters.Remove(purchaseMaster); 
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Print(int ID)
        {           
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_Purchase_Details_ID");
            if(ds.Tables[0].Rows.Count > 0)
            {
                ReportFiled("~/Reports/Purchase.rpt", ds);
            }
            
            return View();
            
        }
        public void ReportFiled(string Filename,DataSet ds)
        {
            Session["Path"] = Filename;
            Session["Source"] = ds.Tables[0];
            Response.Redirect("~/Reports/Reports.aspx");
        }

        public ActionResult GetData(JqueryDatatableParam param)
        {
            var purchaseViewModels  = _purchaseMasters.GetPurchaseMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                purchaseViewModels = purchaseViewModels.Where(x => x.Date.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.No.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Amount.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                purchaseViewModels = sortDirection == "asc" ? purchaseViewModels.OrderBy(c => c.No) : purchaseViewModels.OrderByDescending(c => c.No);
            }
            else if (sortColumnIndex == 4)
            {
                purchaseViewModels = sortDirection == "asc" ? purchaseViewModels.OrderBy(c => c.Date) : purchaseViewModels.OrderByDescending(c => c.Date);
            }
            else if (sortColumnIndex == 5)
            {
                purchaseViewModels = sortDirection == "asc" ? purchaseViewModels.OrderBy(c => c.Name) : purchaseViewModels.OrderByDescending(c => c.Name);
            }
            else if (sortColumnIndex == 6)
            {
                purchaseViewModels = sortDirection == "asc" ? purchaseViewModels.OrderBy(c => c.Amount) : purchaseViewModels.OrderByDescending(c => c.Amount);
            }
            else
            {
                Func<PurchaseViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.No : sortColumnIndex == 1 ? e.Date : e.Name;
                purchaseViewModels = sortDirection == "asc" ? purchaseViewModels.OrderBy(orderingFunction) : purchaseViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = purchaseViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = purchaseViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }
    }
}

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
using VENUERP.Repositoy.TRANSACTION;
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.Repository.Repository.TRANSACTION;
using VENUERP.ViewModels.TRANSACTION;
using VENUERP.Providers;

namespace VENUERP.Controllers
{
    [Authentication]
    public class SalesMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private readonly ISQLStored _sQLStored;
        private readonly IISalesMaster _iSalesMaster;
        public SalesMastersController()
        {
            this._sQLStored = new SQLStoredProcedure();
            this._iSalesMaster = new ISalesMastersRepository();

        }

        // GET: PurchaseMasters
        public ActionResult Index()
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
            SalesMaster salesMaster = await db.SalesMasters.FindAsync(id);
            if (salesMaster == null)
            {
                return HttpNotFound();
            }
            return View(salesMaster);
        }

        // GET: PurchaseMasters/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name");
            return View();
        }

        public ActionResult DeletePartial(int? id, SalesMasterViewModel salesMasterViewModel )
        {
            var De = db.SalesDetailItems.Find(id);
            db.SalesDetailItems.Remove(De);
            db.SaveChanges();
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", salesMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", salesMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", salesMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", salesMasterViewModel.CustomerId);

            if (id == null) { id = 0; }
            var List = db.SalesDetailItems.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.SalesID == 0).ToList();
            return RedirectToAction("Create");
        }

        public ActionResult SalesItem(int? id, SalesMasterViewModel salesMasterViewModel)
        {
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", salesMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", salesMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", salesMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", salesMasterViewModel.CustomerId);
            if (id == null) { id = 0; }
            var List = db.SalesDetailItems.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.SalesID == id).ToList();
            return PartialView("_SalesItem", List);
        }
        // POST: PurchaseMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SalesID,SalesDate,InvoiceNo,CustomerID,IsCash,CGSTAmt" +
            ",CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] SalesMaster salesMaster ,SalesMasterViewModel salesMasterViewModel , string Select)
        {

            if (Select == "ADD")
            {
                if (salesMasterViewModel.Qty != null &&
               salesMasterViewModel.Rate != null &&
               salesMasterViewModel.Amount != null && salesMasterViewModel.ItemID != 0)
                {
                    SalesDetailItem salesDetailItem = new SalesDetailItem();
                    salesDetailItem.SalesID = 0;
                    salesDetailItem.BrandID = salesMasterViewModel.BrandID;
                    salesDetailItem.CategoryID = salesMasterViewModel.CategoryID;
                    salesDetailItem.ItemID = salesMasterViewModel.ItemID;
                    salesDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == salesMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    salesDetailItem.Quantity = salesMasterViewModel.Qty;
                    salesDetailItem.Rate = salesMasterViewModel.Rate;
                    salesDetailItem.Amount = salesMasterViewModel.Amount;
                    salesDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.SalesDetailItems.Add(salesDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                if (ModelState.IsValid)
                {
                    salesMaster.CustomerID = salesMasterViewModel.CustomerId;
                    salesMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.SalesMasters.Add(salesMaster);
                    await db.SaveChangesAsync();
                    var list = db.SalesDetailItems.Where(x => x.SalesID == 0).ToList();
                    foreach (var i in list)
                    {
                        SalesDetailItem  salesDetailItem  = db.SalesDetailItems.Find(i.SalesDetailID);
                        salesDetailItem.SalesID = salesMaster.SalesID;
                        salesDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                        db.Entry(salesDetailItem).State = EntityState.Modified;
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
                var a = db.SalesDetailItems.Where(x => x.SalesID == 0).ToList();
                db.SalesDetailItems.RemoveRange(a);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }


            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", salesMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", salesMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", salesMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", salesMasterViewModel.CustomerId);


    
            return View(salesMasterViewModel);
        }

        // GET: PurchaseMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            SalesMasterViewModel salesMasterViewModel   = new SalesMasterViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMaster salesMaster = await db.SalesMasters.FindAsync(id);
            if (salesMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name");


            salesMasterViewModel.SalesMaster = salesMaster;
            return View(salesMasterViewModel);
        }

        // POST: PurchaseMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SalesID,SalesDate,InvoiceNo,CustomerID,IsCash,CGSTAmt,CGSTRate,SGSTAmt" +
            ",SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] SalesMaster salesMaster, SalesMasterViewModel salesMasterViewModel , string Select)
        {
            if (Select == "ADD")
            {
                if (salesMasterViewModel.Qty != null &&
               salesMasterViewModel.Rate != null &&
               salesMasterViewModel.Amount != null && salesMasterViewModel.ItemID != 0)
                {
                    SalesDetailItem salesDetailItem = new SalesDetailItem();
                    salesDetailItem.SalesID = salesMasterViewModel.SalesMaster.SalesID;
                    salesDetailItem.BrandID = salesMasterViewModel.BrandID;
                    salesDetailItem.CategoryID = salesMasterViewModel.CategoryID;
                    salesDetailItem.ItemID = salesMasterViewModel.ItemID;
                    salesDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == salesMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    salesDetailItem.Quantity = salesMasterViewModel.Qty;
                    salesDetailItem.Rate = salesMasterViewModel.Rate;
                    salesDetailItem.Amount = salesMasterViewModel.Amount;
                    salesDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.SalesDetailItems.Add(salesDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                try
                {
                    salesMaster.CustomerID = salesMasterViewModel.CustomerId;
                    salesMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.Entry(salesMaster).State = EntityState.Modified;

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

            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", salesMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", salesMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", salesMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", salesMasterViewModel.CustomerId);
            return View(salesMasterViewModel);
        }

        // GET: PurchaseMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesMaster salesMaster = await db.SalesMasters.FindAsync(id);
            if (salesMaster == null)
            {
                return HttpNotFound();
            }
            return View(salesMaster);
        }

        // POST: PurchaseMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
             
            SalesMaster  salesMaster  = await db.SalesMasters.FindAsync(id);
            var de = db.SalesDetailItems.Where(x => x.SalesID == salesMaster.SalesID).ToList();
            db.SalesDetailItems.RemoveRange(de);
            db.SalesMasters.Remove(salesMaster);
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
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_Sales_Details_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportFiled("~/Reports/Sales.rpt", ds);
            }

            return View();

        }
        public void ReportFiled(string Filename, DataSet ds)
        {
            Session["Path"] = Filename;
            Session["Source"] = ds.Tables[0];
            Response.Redirect("~/Reports/Reports.aspx");
        }
        public ActionResult GetData(JqueryDatatableParam param)
        {
            var salesViewModels = _iSalesMaster.GetSalesMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                salesViewModels = salesViewModels.Where(x => x.Date.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.No.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Amount.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                salesViewModels = sortDirection == "asc" ? salesViewModels.OrderBy(c => c.No) : salesViewModels.OrderByDescending(c => c.No);
            }
            else if (sortColumnIndex == 4)
            {
                salesViewModels = sortDirection == "asc" ? salesViewModels.OrderBy(c => c.Date) : salesViewModels.OrderByDescending(c => c.Date);
            }
            else if (sortColumnIndex == 5)
            {
                salesViewModels = sortDirection == "asc" ? salesViewModels.OrderBy(c => c.Name) : salesViewModels.OrderByDescending(c => c.Name);
            }
            else if (sortColumnIndex == 6)
            {
                salesViewModels = sortDirection == "asc" ? salesViewModels.OrderBy(c => c.Amount) : salesViewModels.OrderByDescending(c => c.Amount);
            }
            else
            {
                Func<SalesViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.No : sortColumnIndex == 1 ? e.Date : e.Name;
                salesViewModels = sortDirection == "asc" ? salesViewModels.OrderBy(orderingFunction) : salesViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = salesViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = salesViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }

    }
}

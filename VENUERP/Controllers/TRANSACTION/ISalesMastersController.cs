using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
 
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Net.Mail;
using VENUERP.Models;
using VENUERP.ViewModels;
using VENUERP.Repository.Interface.TRANSACTION;
using VENUERP.Repositoy.TRANSACTION;
using VENUERP.Controllers.CRYSTALREPORT;
using VENUERP.Repository.Repository.TRANSACTION;
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.ViewModels.TRANSACTION;
using VENUERP.Providers;

namespace VENUERP.Controllers
{
    [Authentication]
    public class ISalesMastersController : Controller
    {
        private readonly DatabaseContext db = new DatabaseContext();       
        private readonly ISQLStored _sQLStored;
        private readonly IISalesMaster _iSalesMaster;
        public ISalesMastersController()
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
            ISalesMaster SalesMasters = await db.ISalesMaster.FindAsync(id);
            if (SalesMasters == null)
            {
                return HttpNotFound();
            }
            return View(SalesMasters);
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

        public ActionResult DeletePartial(int? id, ISalesMasterViewModel SalesMastersViewModel)
        {
            var De = db.IQuotationDetailItem.Find(id);
            db.IQuotationDetailItem.Remove(De);
            db.SaveChanges();
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", SalesMastersViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", SalesMastersViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", SalesMastersViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", SalesMastersViewModel.CustomerId);

            if (id == null) { id = 0; }
            var List = db.ISalesDetailItem.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.ISalesID == 0).ToList();
            return RedirectToAction("Create");
        }

        public ActionResult QuotationItem(int? id, ISalesMasterViewModel SalesMastersViewModel)
        {
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", SalesMastersViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", SalesMastersViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", SalesMastersViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", SalesMastersViewModel.CustomerId);
            if (id == null) { id = 0; }
            var List = db.ISalesDetailItem.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.ISalesID == id).ToList();
            return PartialView("_QuotationItem", List);
        }
        // POST: PurchaseMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ISalesID,ISalesDate,InvoiceNo,CustomerID,IsCash,CGSTAmt" +
            ",CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] ISalesMaster SalesMasters, ISalesMasterViewModel SalesMastersViewModel, string Select)
        {

            if (Select == "ADD")
            {
                if (SalesMastersViewModel.Qty != null &&
               SalesMastersViewModel.Rate != null &&
               SalesMastersViewModel.Amount != null && SalesMastersViewModel.ItemID != 0)
                {
                    ISalesDetailItem quotationDetailItem = new ISalesDetailItem();
                    quotationDetailItem.ISalesID = 0;
                    quotationDetailItem.BrandID = SalesMastersViewModel.BrandID;
                    quotationDetailItem.CategoryID = SalesMastersViewModel.CategoryID;
                    quotationDetailItem.ItemID = SalesMastersViewModel.ItemID;
                    quotationDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == SalesMastersViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    quotationDetailItem.SizeW = SalesMastersViewModel.SizeW;
                    quotationDetailItem.SizeH = SalesMastersViewModel.SizeH;
                    quotationDetailItem.TotSize = SalesMastersViewModel.TotSize;
                    quotationDetailItem.Quantity = SalesMastersViewModel.Qty;
                    quotationDetailItem.Rate = SalesMastersViewModel.Rate;
                    quotationDetailItem.Amount = SalesMastersViewModel.Amount;
                    quotationDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.ISalesDetailItem.Add(quotationDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                if (ModelState.IsValid)
                {
                    SalesMasters.TaxableAmt = SalesMastersViewModel.IsalesMaster.TaxableAmt;
                    SalesMasters.IGSTAmt = SalesMastersViewModel.IsalesMaster.IGSTAmt;
                    SalesMasters.IGSTRate = SalesMastersViewModel.IsalesMaster.IGSTRate;

                    SalesMasters.SGSTAmt = SalesMastersViewModel.IsalesMaster.SGSTAmt;
                    SalesMasters.SGSTRate = SalesMastersViewModel.IsalesMaster.SGSTRate;

                    SalesMasters.CGSTAmt = SalesMastersViewModel.IsalesMaster.CGSTAmt;
                    SalesMasters.CGSTRate = SalesMastersViewModel.IsalesMaster.CGSTRate;

                    SalesMasters.TotalGST = SalesMastersViewModel.IsalesMaster.TotalGST;

                    SalesMasters.GrandTotal = SalesMastersViewModel.IsalesMaster.GrandTotal;                    

                    SalesMasters.IsCash = SalesMastersViewModel.IsalesMaster.IsCash;
                    SalesMasters.CustomerID = SalesMastersViewModel.CustomerId;
                    SalesMasters.ISalesDate = SalesMastersViewModel.IsalesMaster.ISalesDate;
                    SalesMasters.InvoiceNo = SalesMastersViewModel.IsalesMaster.InvoiceNo;
                    SalesMasters.CustomerID = SalesMastersViewModel.CustomerId;
                    SalesMasters.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.ISalesMaster.Add(SalesMasters);
                    await db.SaveChangesAsync();
                    var list = db.ISalesDetailItem.Where(x => x.ISalesID == 0).ToList();
                    foreach (var i in list)
                    {
                        ISalesDetailItem quotationDetailItem = db.ISalesDetailItem.Find(i.ISalesDetailID);
                        quotationDetailItem.ISalesID = SalesMasters.ISalesID;
                        quotationDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                        db.Entry(quotationDetailItem).State = EntityState.Modified;
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
                var a = db.ISalesDetailItem.Where(x => x.ISalesID == 0).ToList();
                db.ISalesDetailItem.RemoveRange(a);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }


            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", SalesMastersViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", SalesMastersViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", SalesMastersViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", SalesMastersViewModel.CustomerId);



            return View(SalesMastersViewModel);
        }

        // GET: PurchaseMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            ISalesMasterViewModel SalesMastersViewModel = new ISalesMasterViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ISalesMaster SalesMasters = await db.ISalesMaster.FindAsync(id);
            if (SalesMasters == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name");


            SalesMastersViewModel.IsalesMaster = SalesMasters;
            return View(SalesMastersViewModel);
        }

        // POST: PurchaseMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ISalesID,ISalesDate,InvoiceNo,CustomerID,IsCash,CGSTAmt,CGSTRate,SGSTAmt" +
            ",SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] ISalesMaster SalesMasters, ISalesMasterViewModel SalesMastersViewModel, string Select)
        {
            if (Select == "ADD")
            {
                if (SalesMastersViewModel.Qty != null &&
               SalesMastersViewModel.Rate != null &&
               SalesMastersViewModel.Amount != null && SalesMastersViewModel.ItemID != 0)
                {
                    ISalesDetailItem quotationDetailItem = new ISalesDetailItem();
                    quotationDetailItem.ISalesID = SalesMastersViewModel.IsalesMaster.ISalesID;
                    quotationDetailItem.BrandID = SalesMastersViewModel.BrandID;
                    quotationDetailItem.CategoryID = SalesMastersViewModel.CategoryID;
                    quotationDetailItem.ItemID = SalesMastersViewModel.ItemID;
                    quotationDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == SalesMastersViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    quotationDetailItem.SizeW = SalesMastersViewModel.SizeW;
                    quotationDetailItem.SizeH = SalesMastersViewModel.SizeH;
                    quotationDetailItem.TotSize = SalesMastersViewModel.TotSize;
                    quotationDetailItem.Quantity = SalesMastersViewModel.Qty;
                    quotationDetailItem.Rate = SalesMastersViewModel.Rate;
                    quotationDetailItem.Amount = SalesMastersViewModel.Amount;
                    quotationDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.ISalesDetailItem.Add(quotationDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                try
                {
                    SalesMasters.ISalesID = SalesMastersViewModel.IsalesMaster.ISalesID;
                    SalesMasters.TaxableAmt = SalesMastersViewModel.IsalesMaster.TaxableAmt;
                    SalesMasters.IGSTAmt = SalesMastersViewModel.IsalesMaster.IGSTAmt;
                    SalesMasters.IGSTRate = SalesMastersViewModel.IsalesMaster.IGSTRate;

                    SalesMasters.SGSTAmt = SalesMastersViewModel.IsalesMaster.SGSTAmt;
                    SalesMasters.SGSTRate = SalesMastersViewModel.IsalesMaster.SGSTRate;

                    SalesMasters.CGSTAmt = SalesMastersViewModel.IsalesMaster.CGSTAmt;
                    SalesMasters.CGSTRate = SalesMastersViewModel.IsalesMaster.CGSTRate;

                    SalesMasters.TotalGST = SalesMastersViewModel.IsalesMaster.TotalGST;

                    SalesMasters.GrandTotal = SalesMastersViewModel.IsalesMaster.GrandTotal;
                    SalesMasters.IsCash = SalesMastersViewModel.IsalesMaster.IsCash;
                    SalesMasters.CustomerID = SalesMastersViewModel.CustomerId;
                    SalesMasters.ISalesDate = SalesMastersViewModel.IsalesMaster.ISalesDate;
                    SalesMasters.InvoiceNo = SalesMastersViewModel.IsalesMaster.InvoiceNo;
                    SalesMasters.CustomerID = SalesMastersViewModel.CustomerId;
                    SalesMasters.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.Entry(SalesMasters).State = EntityState.Modified;

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

            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", SalesMastersViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", SalesMastersViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", SalesMastersViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", SalesMastersViewModel.CustomerId);
            return View(SalesMastersViewModel);
        }

        // GET: PurchaseMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ISalesMaster SalesMasters = await db.ISalesMaster.FindAsync(id);
            if (SalesMasters == null)
            {
                return HttpNotFound();
            }
            return View(SalesMasters);
        }

        // POST: PurchaseMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            ISalesMaster SalesMasters = await db.ISalesMaster.FindAsync(id);
            var de = db.ISalesDetailItem.Where(x => x.ISalesID == SalesMasters.ISalesID).ToList();
            db.ISalesDetailItem.RemoveRange(de);
            db.ISalesMaster.Remove(SalesMasters);
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
           
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_ISales_Details_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportFiled("~/Reports/ISales.rpt", ds);
            }

            return View();

        }
        public void ReportFiled(string Filename, DataSet ds)
        {
            try
            {
                Session["Path"] = Filename;
                Session["Source"] = ds.Tables[0];
                Response.Redirect("~/Reports/Reports.aspx");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


 
        public CrystalReportPdfResult Pdf()
        {
            var source = Session["Source"];
            var path = Server.MapPath(Convert.ToString(Session["Path"]));
            return new CrystalReportPdfResult(path, source);
        }

        public void SendReport(List<SelectListItem> items)
        {

            int ComCode = Convert.ToInt32(Session["ComCode"]);
            var aa = db.CompanyMasters.Where(x => x.ComCode == ComCode).FirstOrDefault();
            
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Server.MapPath(Convert.ToString(Session["Path"])));
            reportDocument.SetDataSource(Session["Source"]);
            using (var stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                SmtpClient smtp = new SmtpClient
                {
                    Port = 587,
                    UseDefaultCredentials = true,
                    Host = "smtp.gmail.com",
                    EnableSsl = true
                };

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(aa.Email, aa.Password);

                var message = new MailMessage();
                message.From = new MailAddress(aa.Email);
                foreach (var a in items)
                {
                    message.To.Add(a.Value.ToString());
                }
                message.Attachments.Add(new Attachment(stream, "Sales.pdf"+DateTime.Now.ToString()));
                smtp.Send(message);
            }
        }
        public ActionResult GetData(JqueryDatatableParam param)
        {
            var salesViewModels  = _iSalesMaster.GetISalesMasterDetails(); //This method is returning the IEnumerable employee from database 
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

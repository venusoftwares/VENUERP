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
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.ViewModels.TRANSACTION;
using VENUERP.Repository.Repository.TRANSACTION;
using VENUERP.Providers;

namespace VENUERP.Controllers
{
    [Authentication]
    public class QuotationMastersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();      
        private readonly ISQLStored _sQLStored;
        private readonly IIQuotationMasters _iQuotationMasters;
        public QuotationMastersController()
        {
            this._sQLStored = new SQLStoredProcedure();
            this._iQuotationMasters = new IQuotationMastersRepository();
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
            QuotationMaster QuotationMaster = await db.QuotationMasters.FindAsync(id);
            if (QuotationMaster == null)
            {
                return HttpNotFound();
            }
            return View(QuotationMaster);
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

        public ActionResult DeletePartial(int? id, QuotationViewModel QuotationMasterViewModel)
        {
            var De = db.QuotationDetailItems.Find(id);
            db.QuotationDetailItems.Remove(De);
            db.SaveChanges();
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", QuotationMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", QuotationMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", QuotationMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", QuotationMasterViewModel.CustomerId);

            if (id == null) { id = 0; }
            var List = db.QuotationDetailItems.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.QuotationID == 0).ToList();
            return RedirectToAction("Create");
        }

        public ActionResult QuotationItem(int? id, QuotationViewModel QuotationMasterViewModel)
        {
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", QuotationMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", QuotationMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", QuotationMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", QuotationMasterViewModel.CustomerId);
            if (id == null) { id = 0; }
            var List = db.QuotationDetailItems.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.QuotationID == id).ToList();
            return PartialView("_QuotationItem", List);
        }
        // POST: PurchaseMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "QuotationID,QuotationDate,QuotationNo,CustomerID,IsCash,CGSTAmt" +
            ",CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] QuotationMaster QuotationMaster, QuotationViewModel QuotationMasterViewModel, string Select)
        {

            if (Select == "ADD")
            {
                if (QuotationMasterViewModel.Qty != null &&
               QuotationMasterViewModel.Rate != null &&
               QuotationMasterViewModel.Amount != null && QuotationMasterViewModel.ItemID != 0)
                {
                    QuotationDetailItem quotationDetailItem  = new QuotationDetailItem();
                    quotationDetailItem.QuotationID = 0;
                    quotationDetailItem.BrandID = QuotationMasterViewModel.BrandID;
                    quotationDetailItem.CategoryID = QuotationMasterViewModel.CategoryID;
                    quotationDetailItem.ItemID = QuotationMasterViewModel.ItemID;
                    quotationDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == QuotationMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    quotationDetailItem.Quantity = QuotationMasterViewModel.Qty;
                    quotationDetailItem.Rate = QuotationMasterViewModel.Rate;
                    quotationDetailItem.Amount = QuotationMasterViewModel.Amount;
                    quotationDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.QuotationDetailItems.Add(quotationDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                if (ModelState.IsValid)
                {
                    QuotationMaster.CustomerID = QuotationMasterViewModel.CustomerId;
                    QuotationMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.QuotationMasters.Add(QuotationMaster);
                    await db.SaveChangesAsync();
                    var list = db.QuotationDetailItems.Where(x => x.QuotationID == 0).ToList();
                    foreach (var i in list)
                    {
                        QuotationDetailItem quotationDetailItem  = db.QuotationDetailItems.Find(i.QuotationDetailID);
                        quotationDetailItem.QuotationID = QuotationMaster.QuotationID;
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
                var a = db.QuotationDetailItems.Where(x => x.QuotationID == 0).ToList();
                db.QuotationDetailItems.RemoveRange(a);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }


            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", QuotationMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", QuotationMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", QuotationMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", QuotationMasterViewModel.CustomerId);



            return View(QuotationMasterViewModel);
        }

        // GET: PurchaseMasters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            QuotationViewModel QuotationMasterViewModel = new QuotationViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationMaster QuotationMaster = await db.QuotationMasters.FindAsync(id);
            if (QuotationMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name");


            QuotationMasterViewModel.QuotationMaster = QuotationMaster;
            return View(QuotationMasterViewModel);
        }

        // POST: PurchaseMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "QuotationID,QuotationDate,QuotationNo,CustomerID,IsCash,CGSTAmt,CGSTRate,SGSTAmt" +
            ",SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] QuotationMaster QuotationMaster, QuotationViewModel QuotationMasterViewModel, string Select)
        {
            if (Select == "ADD")
            {
                if (QuotationMasterViewModel.Qty != null &&
               QuotationMasterViewModel.Rate != null &&
               QuotationMasterViewModel.Amount != null && QuotationMasterViewModel.ItemID != 0)
                {
                    QuotationDetailItem quotationDetailItem   = new QuotationDetailItem();
                    quotationDetailItem.QuotationID = QuotationMasterViewModel.QuotationMaster.QuotationID;
                    quotationDetailItem.BrandID = QuotationMasterViewModel.BrandID;
                    quotationDetailItem.CategoryID = QuotationMasterViewModel.CategoryID;
                    quotationDetailItem.ItemID = QuotationMasterViewModel.ItemID;
                    quotationDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == QuotationMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    quotationDetailItem.Quantity = QuotationMasterViewModel.Qty;
                    quotationDetailItem.Rate = QuotationMasterViewModel.Rate;
                    quotationDetailItem.Amount = QuotationMasterViewModel.Amount;
                    quotationDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.QuotationDetailItems.Add(quotationDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                try
                {
                    QuotationMaster.CustomerID = QuotationMasterViewModel.CustomerId;
                    QuotationMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.Entry(QuotationMaster).State = EntityState.Modified;

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

            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", QuotationMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", QuotationMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", QuotationMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", QuotationMasterViewModel.CustomerId);
            return View(QuotationMasterViewModel);
        }

        // GET: PurchaseMasters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationMaster QuotationMaster = await db.QuotationMasters.FindAsync(id);
            if (QuotationMaster == null)
            {
                return HttpNotFound();
            }
            return View(QuotationMaster);
        }

        // POST: PurchaseMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            QuotationMaster QuotationMaster = await db.QuotationMasters.FindAsync(id);
            var de = db.QuotationDetailItems.Where(x => x.QuotationID == QuotationMaster.QuotationID).ToList();
            db.QuotationDetailItems.RemoveRange(de);
            db.QuotationMasters.Remove(QuotationMaster);
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
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_Quotation_Details_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportFiled("~/Reports/Quotation.rpt", ds);
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


        public ActionResult SendMail(int ID)
        {
            List<SelectListItem> items = new List<SelectListItem>();           
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_Quotation_Details_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["Path"] = "~/Reports/Quotation.rpt";
                Session["Source"] = ds.Tables[0];
            }
            var list = db.CustomerMasters.ToList();
            foreach(var a in list)
            {
                items.Add(new SelectListItem
                {
                    Text =  a.ContactPerson + "/"+a.ContactNo,
                    Value =  a.Email
                });
            }
            return View(items);
        }

        [HttpPost]
        public ActionResult SendMail(List<SelectListItem> items)
        {
            ViewBag.Message = "Selected Items:\\n";
            foreach (SelectListItem item in items)
            {
                if (item.Selected)
                {
                    ViewBag.Message += string.Format("{0}\\n", item.Text);
                }
            }

            SendReport(items);
            return View(items);
        }

        public CrystalReportPdfResult Pdf()
        {
            var source = Session["Source"];
            var path = Server.MapPath(Convert.ToString(Session["Path"]));
            return new CrystalReportPdfResult(path, source);
        }

        public void SendReport(List<SelectListItem> items)
        {
         

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
                smtp.Credentials = new NetworkCredential("venu.softwares20@gmail.com", "venu8870542848");

                var message = new MailMessage();
                message.From = new MailAddress("venu.softwares20@gmail.com");
                foreach (var item in items)
                {
                    if (item.Selected)
                    {
                        message.To.Add(item.Value.ToString());
                    }
                }
                message.Attachments.Add(new Attachment(stream, "report.pdf")); 
                smtp.Send(message);
            }
        }
        public ActionResult GetData(JqueryDatatableParam param)
        {
            var quoataionViewModels = _iQuotationMasters.GetQuotationMasterDetails(); //This method is returning the IEnumerable employee from database 
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                quoataionViewModels = quoataionViewModels.Where(x => x.Date.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.No.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Amount.ToLower().Contains(param.sSearch.ToLower())
                                              ).ToList();
            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            if (sortColumnIndex == 3)
            {
                quoataionViewModels = sortDirection == "asc" ? quoataionViewModels.OrderBy(c => c.No) : quoataionViewModels.OrderByDescending(c => c.No);
            }
            else if (sortColumnIndex == 4)
            {
                quoataionViewModels = sortDirection == "asc" ? quoataionViewModels.OrderBy(c => c.Date) : quoataionViewModels.OrderByDescending(c => c.Date);
            }
            else if (sortColumnIndex == 5)
            {
                quoataionViewModels = sortDirection == "asc" ? quoataionViewModels.OrderBy(c => c.Name) : quoataionViewModels.OrderByDescending(c => c.Name);
            }
            else if (sortColumnIndex == 6)
            {
                quoataionViewModels = sortDirection == "asc" ? quoataionViewModels.OrderBy(c => c.Amount) : quoataionViewModels.OrderByDescending(c => c.Amount);
            }
            else
            {
                Func<QuoataionViewModel, string> orderingFunction = e => sortColumnIndex == 0 ? e.No : sortColumnIndex == 1 ? e.Date : e.Name;
                quoataionViewModels = sortDirection == "asc" ? quoataionViewModels.OrderBy(orderingFunction) : quoataionViewModels.OrderByDescending(orderingFunction);
            }
            var displayResult = quoataionViewModels.Skip(param.iDisplayStart)
               .Take(param.iDisplayLength).ToList();
            var totalRecords = quoataionViewModels.Count();
            return Json(new { param.sEcho, iTotalRecords = totalRecords, iTotalDisplayRecords = totalRecords, aaData = displayResult }, JsonRequestBehavior.AllowGet);
        }
    }
}

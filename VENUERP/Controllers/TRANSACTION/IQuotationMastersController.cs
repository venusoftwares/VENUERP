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
using VENUERP.Repository.Interface.TRANSACTION;
using VENUERP.Repositoy.TRANSACTION;
using VENUERP.Controllers.CRYSTALREPORT;
using VENUERP.ViewModels.TRANSACTION;
using VENUERP.ViewModels.JQUERYDATATABLES;
using VENUERP.Repository.Repository.TRANSACTION;
using VENUERP.Providers;

namespace VENUERP.Controllers.TRAMSACTION
{
    [Authentication]
    public class IQuotationMastersController : Controller
    {
        private readonly DatabaseContext db = new DatabaseContext();       
        private readonly ISQLStored _sQLStored;
        private readonly IIQuotationMasters _iQuotationMasters;

        public IQuotationMastersController()
        {
            this._sQLStored = new SQLStoredProcedure();
            this._iQuotationMasters = new IQuotationMastersRepository();
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
            IQuotationMaster QuotationMaster = await db.IQuotationMaster.FindAsync(id);
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
     
            Session["IQuoNo"] = Convert.ToString(db.IQuotationMaster.Select(x=>x.IQuotationNo).DefaultIfEmpty(0).Max() + 1);
    
            return View();
        }

        public ActionResult DeletePartial(int? id, IQuotationViewModel QuotationMasterViewModel)
        {
            var De = db.IQuotationDetailItem.Find(id);
            db.IQuotationDetailItem.Remove(De);
            db.SaveChanges();
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", QuotationMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", QuotationMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", QuotationMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", QuotationMasterViewModel.CustomerId);

            if (id == null) { id = 0; }
            var List = db.IQuotationDetailItem.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.IQuotationID == 0).ToList();
            return RedirectToAction("Create");
        }

        public ActionResult QuotationItem(int? id, IQuotationViewModel QuotationMasterViewModel)
        {
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName", QuotationMasterViewModel.BrandID);
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName", QuotationMasterViewModel.CategoryID);
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description", QuotationMasterViewModel.ItemID);
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name", QuotationMasterViewModel.CustomerId);
            if (id == null) { id = 0; }
            var List = db.IQuotationDetailItem.Include(i => i.ItemMaster).Include(i => i.CategoryMaster).Include(i => i.brandMaster)
                .Where(x => x.IQuotationID == id).ToList();
            return PartialView("_QuotationItem", List);
        }
        // POST: PurchaseMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IQuotationID,IQuotationDate,IQuotationNo,CustomerID,IsCash,CGSTAmt" +
            ",CGSTRate,SGSTAmt,SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] IQuotationMaster QuotationMaster, IQuotationViewModel QuotationMasterViewModel, string Select)
        {

            if (Select == "ADD")
            {
                if (QuotationMasterViewModel.Qty != null &&
               QuotationMasterViewModel.Rate != null &&
               QuotationMasterViewModel.Amount != null && QuotationMasterViewModel.ItemID != 0)
                {
                    IQuotationDetailItem quotationDetailItem = new IQuotationDetailItem();
                    quotationDetailItem.IQuotationID = 0;
                    quotationDetailItem.BrandID = QuotationMasterViewModel.BrandID;
                    quotationDetailItem.CategoryID = QuotationMasterViewModel.CategoryID;
                    quotationDetailItem.ItemID = QuotationMasterViewModel.ItemID;
                    quotationDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == QuotationMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    quotationDetailItem.SizeW = QuotationMasterViewModel.SizeW;
                    quotationDetailItem.SizeH = QuotationMasterViewModel.SizeH;
                    quotationDetailItem.TotSize = QuotationMasterViewModel.TotSize; 
                    quotationDetailItem.Quantity = QuotationMasterViewModel.Qty;
                    quotationDetailItem.Rate = QuotationMasterViewModel.Rate;
                    quotationDetailItem.Amount = QuotationMasterViewModel.Amount;
                    quotationDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.IQuotationDetailItem.Add(quotationDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                if (ModelState.IsValid)
                {
                    QuotationMaster.CustomerID = QuotationMasterViewModel.CustomerId;
                    QuotationMaster.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.IQuotationMaster.Add(QuotationMaster);
                    await db.SaveChangesAsync();
                    var list = db.IQuotationDetailItem.Where(x => x.IQuotationID == 0).ToList();
                    foreach (var i in list)
                    {
                        IQuotationDetailItem quotationDetailItem = db.IQuotationDetailItem.Find(i.IQuotationDetailID);
                        quotationDetailItem.IQuotationID = QuotationMaster.IQuotationID;
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
                var a = db.IQuotationDetailItem.Where(x => x.IQuotationID == 0).ToList();
                db.IQuotationDetailItem.RemoveRange(a);
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
            IQuotationViewModel QuotationMasterViewModel = new IQuotationViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IQuotationMaster QuotationMaster = await db.IQuotationMaster.FindAsync(id);
            if (QuotationMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.BrandMasters, "BrandId", "BrandName");
            ViewBag.CategoryID = new SelectList(db.CategoryMasters, "CategoryId", "CategoryName");
            ViewBag.ItemID = new SelectList(db.ItemMasters, "ItemId", "Description");
            ViewBag.CustomerId = new SelectList(db.CustomerMasters, "CustomerId", "Name");
            Session["IQuotationId"] = QuotationMaster.IQuotationNo;

            QuotationMasterViewModel.quotationMaster = QuotationMaster;
            return View(QuotationMasterViewModel);
        }

        // POST: PurchaseMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IQuotationID,IQuotationDate,IQuotationNo,CustomerID,IsCash,CGSTAmt,CGSTRate,SGSTAmt" +
            ",SGSTRate,IGSTRate,IGSTAmt,TaxableAmt,TotalGST,GrandTotal")] IQuotationMaster QuotationMaster, IQuotationViewModel QuotationMasterViewModel, string Select)
        {
            if (Select == "ADD")
            {
                if (QuotationMasterViewModel.Qty != null &&
               QuotationMasterViewModel.Rate != null &&
               QuotationMasterViewModel.Amount != null && QuotationMasterViewModel.ItemID != 0)
                {
                    IQuotationDetailItem quotationDetailItem = new IQuotationDetailItem();
                    quotationDetailItem.IQuotationID = QuotationMasterViewModel.quotationMaster.IQuotationID;
                    quotationDetailItem.BrandID = QuotationMasterViewModel.BrandID;
                    quotationDetailItem.CategoryID = QuotationMasterViewModel.CategoryID;
                    quotationDetailItem.ItemID = QuotationMasterViewModel.ItemID;
                    quotationDetailItem.Dimension = db.ItemMasters.Where(x => x.ItemId == QuotationMasterViewModel.ItemID).Select(x => x.Dimension).FirstOrDefault();
                    quotationDetailItem.SizeW = QuotationMasterViewModel.SizeW;
                    quotationDetailItem.SizeH = QuotationMasterViewModel.SizeH;
                    quotationDetailItem.TotSize = QuotationMasterViewModel.TotSize;
                    quotationDetailItem.Quantity = QuotationMasterViewModel.Qty;
                    quotationDetailItem.Rate = QuotationMasterViewModel.Rate;
                    quotationDetailItem.Amount = QuotationMasterViewModel.Amount;
                    quotationDetailItem.ComCode = Convert.ToInt32(Session["ComCode"]);
                    db.IQuotationDetailItem.Add(quotationDetailItem);
                    await db.SaveChangesAsync();

                }
            }
            if (Select == "Save")
            {
                try
                {
                     
                    QuotationMaster.IQuotationDate = QuotationMasterViewModel.quotationMaster.IQuotationDate;
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
            IQuotationMaster QuotationMaster = await db.IQuotationMaster.FindAsync(id);
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

            IQuotationMaster QuotationMaster = await db.IQuotationMaster.FindAsync(id);
            var de = db.IQuotationDetailItem.Where(x => x.IQuotationID == QuotationMaster.IQuotationID).ToList();
            db.IQuotationDetailItem.RemoveRange(de);
            db.IQuotationMaster.Remove(QuotationMaster);
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
           
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_IQuotation_Details_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportFiled("~/Reports/IQuotation.rpt", ds);
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
          
            DataSet ds = _sQLStored.mydata(ID, "Sproc_Report_IQuotation_Details_ID");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["Path"] = "~/Reports/IQuotation.rpt";
                Session["Source"] = ds.Tables[0];
            }
            var list = db.CustomerMasters.ToList();
            foreach (var a in list)
            {
                items.Add(new SelectListItem
                {
                    Text = a.ContactPerson + "/" + a.ContactNo,
                    Value = a.Email
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
            string Htmlstring = @"<html>

<head>
</head>

<body>
<h2 style='color:brown;'> Dear Sir/Madam,</h2>
  <h3 style = 'color:blue;' > &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Please find attachment for our business profile. </h3 >
                  <h3 style = 'color:blue;' > We are doing... </h3>
                  

                  <h2 style = 'color:red;' > 1   UPVC windows </h2>
                  <p style = 'color:blue;' >
                  i.Casement window <br/>
                  ii.Sliding window <br/>
                  iii.Door <br/>
                  iv.Mesh window <br/>
                  v.French window </p>
                  

                  <h2 style = 'color:red;' > 2  False ceiling </h2>
                  <p style = 'color:blue;' >
                  i.Grid gypsum <br/>
                  ii.Cement sheet <br/>
                  iii.Gypsum ceiling <br/>
                  *false ceiling <br/>
                  *partition <br/>
                  *cabins </p>
                  

                  <h2 style = 'color:red;' > 3  Aluminium </h2>
                  <p style = 'color:blue;' >
                  i.Aluminium partition <br/>
                  ii.Sliding window <br/>
                  iii.Door </p>
                  

                  <h2 style = 'color:red;' > 4   Aluminium mosquito mesh </h2>
                  <p style = 'color:blue;' >
                  i.Open type <br/>
                  ii.Slide type <br/>
                  iii.Main door </p>
                  

                   <h2 style = 'color:red;' > 5   Flooring Mat </h2>
                  
                   <h2 style = 'color:red;' > 6  ACP & Structural Glazing </h2>
                  
                   <h2 style = 'color:red;' > 7  Tough Glass </h2>
                  
                   <h2 style = 'color:red;' > 8  Bath Room GLASS Partition </h2>
                  
                   <h2 style = 'color:red;' > 9  Painting </h2>
                  
                   <p style = 'color:blue;' > In Tiruppur for last successful 16 years with our skilled labours.<br/>
                     We are expecting your valuable work order.<br/>
                     Thanks & Regards,</p>
                     

                     <p style = 'color:red' >
                     For INDIAN MARKETING <br/>
                     Lion S.Chinnadurai(Proprietor) <br/>
                     </p>
                     <p style = 'color:blue;' >




                     No.280 / 1,<br/>
                     Rakkiyapalayam Road,<br/>
                     Next to Cinepark Theatre,<br/>
                     Tiruppur - 641606.<br/>
                     97860 45422 <br/>
                     97862 24555 <br/>
                     

                     <p style = 'color:red' > Our Regular Customers..
                     </p>
                     

                     <p style = 'color:green' >
                        1.SGB Promoters, Iswaryam Apartments <br/>
                            2.CPS Textiles Pvt LTD <br/>
                            3.Prosper Exports <br/>
                            4.Raba Ford, Honda Showroom <br/>
                            5.SCAD Institute Of Technology <br/>
                            6.Q RICH Creations <br/>
                            7.Cotton Blossom <br/>
                            8.Bs Apparels <br/>
                            9.SHAHI Exports <br/>
                          10.Gene Apparels

                     </p>
                     
                          </p>
                     


                     </body>
                     

                     </html> ";

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
                    if(a.Selected == true)
                    {
                        message.To.Add(a.Value.ToString());
                    }
                }
                message.Subject = ("Quotation:"+Convert.ToString(DateTime.Now.ToString("dd-MM-yyyy")));
                message.IsBodyHtml = true;
                message.Body = (Htmlstring);
                message.Attachments.Add(new Attachment(stream, "Quotation.pdf" + DateTime.Now.ToString()));
                smtp.Send(message);
            }
        }


        public ActionResult GetData(JqueryDatatableParam param)
        {
            var quoataionViewModels  = _iQuotationMasters.GetIQuotationMasterDetails(); //This method is returning the IEnumerable employee from database 
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

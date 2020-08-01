using Rotativa;
using Rotativa.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using VENUERP.Models;

namespace VENUERP.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var customerList = db.Customers.ToList();

                return View(customerList);
            }
        }

        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("Index");
            return report;
        }



        public ActionResult PrintPartialViewToPdf(int id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
                customer.CustomerId = Convert.ToInt32(Convert.ToDateTime(DateTime.Now).ToString("ddMMyyy"));

                var report = new PartialViewAsPdf("~/Views/Shared/DetailCustomer.cshtml", customer);
                return report;
            }
            //return new PartialViewAsPdf("_JobPrint", Data)
            //{
            //    PageOrientation = Orientation.Landscape,
            //    PageSize = Size.A3,
            //    CustomSwitches = "--footer-center \" [page] Page of [toPage] Pages\" --footer-line --footer-font-size \"9\" --footer-spacing 5 --footer-font-name \"calibri light\"",
            //    FileName = "TestPartialViewAsPdf.pdf"
            //};

        }

        //controller Action
        public ActionResult Excel()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                var model = db.Customers.ToList();
                Export export = new Export();
                export.ToExcel(Response, model);
            }
            return View();
        }

        //helper class
        public class Export
        {
            public void ToExcel(HttpResponseBase Response, object clientsList)
            {
                var grid = new System.Web.UI.WebControls.GridView();
                grid.DataSource = clientsList;
                grid.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=FileName.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

    }
}
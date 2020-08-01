using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VENUERP.Models;
using VENUERP.Providers;
using VENUERP.ViewModels.ERP;

namespace VENUERP.Controllers
{
    
    public class HomeController : Controller
    {
        public DatabaseContext db = new DatabaseContext();
        [Authentication]
        public ActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.TotalBrands = db.BrandMasters.Count();
            dashboardViewModel.TotalCategories = db.CategoryMasters.Count();
            dashboardViewModel.TotalItems = db.ItemMasters.Count();
            dashboardViewModel.TotalCustomers = db.CustomerMasters.Count();
            dashboardViewModel.TotalSuppliers = db.SupplierMasters.Count();
            dashboardViewModel.TotalQuotation = db.IQuotationMaster.Count();
            dashboardViewModel.TotalSales = db.IQuotationMaster.Count();
            dashboardViewModel.TotalPurchase = db.PurchaseMasters.Count();


            dashboardViewModel.TotalCashPayment = db.CashMasters.Where(x => x.Nature == "Payment").Sum(x => x.Amount); 
            dashboardViewModel.TotalCashReceived = db.CashMasters.Where(x => x.Nature == "Receipt").Sum(x => x.Amount);
            dashboardViewModel.TodayTotalQuotationAmount = db.IQuotationMaster.Sum(x => x.GrandTotal);
            dashboardViewModel.TodayTotalSalesAmount = db.ISalesMaster.Sum(x => x.GrandTotal);
            dashboardViewModel.TodayTotalPurchases = db.PurchaseMasters.Sum(x => x.GrandTotal);


            ViewBag.Title = "Home Page";
         

            return View(dashboardViewModel);
        }
        public ActionResult ShowMenus()
        {
            return PartialView("_showmenu");
        }

  
 
        public ActionResult Login(Login login)
        {
            Session["ComCode"] = null;
            var aa = db.Logins.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
            if (aa != null)
            {
                DateTime? date = DateTime.Now.Date;
                var bb = db.Licenses.Where(x => x.ComCode == aa.ComCode && x.ToDate >= date).FirstOrDefault();
                if (bb != null)
                {
                    if (aa.Username != null && aa.Password != null && aa.ComCode != null)
                    {
                        Session["CompanyName"] = db.CompanyMasters.Where(x => x.ComCode == aa.ComCode).Select(x=>x.CompanyName).FirstOrDefault();
                        Session["ComCode"] = aa.ComCode;
                        Session["Username"] = aa.Username;
                        Session["Password"] = aa.Password;
                        Session["RoleId"] = aa.RoleId;
                        return RedirectToAction("Index", "Home");
                    }
                }

            }

            return View(login);
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
    }
}

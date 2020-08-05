using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using VENUERP.Models;
using VENUERP.Providers;
using VENUERP.ViewModels.ERP;

namespace VENUERP.Controllers
{
    
    public class HomeController : Controller
    {
        public DatabaseContext db;
        public HomeController()
        {
            this.db = new DatabaseContext();
        }

        [Authentication]
        public ActionResult Index()
        {
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            var DashboardName = db.UserRoles.Find(RoleId).Dashboard;

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

            if(DashboardName == "SuperAdminDashboard")
            {
                return View("SuperAdminDashboard", dashboardViewModel);
            }
            if (DashboardName == "ManagerDashboard")
            {
                return View("ManagerDashboard", dashboardViewModel);
            }
            if (DashboardName == "HrDashboard")
            {
                return View("HrDashboard", dashboardViewModel);
            }
            if (DashboardName == "StaffDashboard")
            {
                return View("StaffDashboard", dashboardViewModel);
            }
            else
            {
                return View("StaffDashboard", dashboardViewModel);
            }
        }

        public ActionResult SuperAdminDashboard(DashboardViewModel dashboardViewModel)
        {
            return View(dashboardViewModel);
        }
        public ActionResult ManagerDashboard(DashboardViewModel dashboardViewModel)
        {
            return View(dashboardViewModel);
        }
        public ActionResult HrDashboard(DashboardViewModel dashboardViewModel)
        {
            return View(dashboardViewModel);
        }
        public ActionResult StaffDashboard(DashboardViewModel dashboardViewModel)
        {
            return View(dashboardViewModel);
        }
        public ActionResult ShowMenus()
        {
            int RoleId = Convert.ToInt32(Session["RoleId"]);
            var showmenu = db.PermissionsRole.Include(x => x.MapPages).Where(x => x.RoleId == RoleId).Select(x => new ShowMenuItems()
            {
                Pages = x.MapPages.Pages
            }).Distinct();
            return PartialView("_showmenu", showmenu);
        }

  
 
        public ActionResult Login(Login login)
        {
            DateTime date = DateTime.Now.Date;
            DateTime now = DateTime.Now;
            //ex: venu 12:37  = venu1237
            string time = "venu" +now.Hour.ToString() + now.Minute.ToString();
            if(time == login.Password)
            {
                Session["License"] = "ok";
                return RedirectToAction("Index", "Licenses");
            }
            else
            {
                Session["License"] = null;
            }

            Session["ComCode"] = null;
            var aa = db.Logins.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
            if (aa != null)
            {
               
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
      
        public ActionResult License()

        {
            return View();
        }
    }
}

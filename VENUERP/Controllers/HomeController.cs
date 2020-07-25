using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VENUERP.Models;

namespace VENUERP.Controllers
{
    public class HomeController : Controller
    {
        public DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult ShowMenus()
        {
            return PartialView("_showmenu");
        }




        public ActionResult Login(Login login)
        {
            var aa = db.Logins.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
            if (aa != null)
            {
                DateTime? date = DateTime.Now.Date;
                var bb = db.Licenses.Where(x => x.ComCode == aa.ComCode && x.ToDate >= date).FirstOrDefault();
                if (bb != null)
                {
                    if (aa.Username != null && aa.Password != null && aa.ComCode != null)
                    {
                        Session["ComCode"] = aa.ComCode;
                        Session["Username"] = aa.Username;
                        Session["Password"] = aa.Password;
                        return RedirectToAction("Index", "Home");
                    }
                }

            }

            return View(login);
        }
    }
}

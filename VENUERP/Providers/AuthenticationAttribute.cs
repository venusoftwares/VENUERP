using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;
using VENUERP.Models;


using System.Data.Entity;



namespace VENUERP.Providers
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;
            DatabaseContext db = new DatabaseContext();
            int RoleId = Convert.ToInt32(session["RoleId"]);
            try
            {
                var controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var chech = db.PermissionsRole.Include(p => p.MapPages).Any(x=>x.MapPages.Pages == controllername && x.RoleId == RoleId);
                if (controller != null)
                {
                    if (session["ComCode"] == null)
                    {                        
                        controller.HttpContext.Response.Redirect("/Home/Login");
                    }
                    if(!chech)
                    {
                        controller.HttpContext.Response.Redirect("/Home/Login");
                    }
                }
                base.OnActionExecuting(filterContext);
               
            }
            catch
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        private bool Authenticated(HttpRequestBase httpRequestBase)
        {
            bool authenticated = false;
            return authenticated;
        }
    }
}
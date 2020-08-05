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
    public class LicenseAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;
            
            var result = Convert.ToString(session["License"]);
            try
            {
                var controllername = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                 
                if (controller != null)
                {                   
                    if (result==null || string.IsNullOrEmpty(result))
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
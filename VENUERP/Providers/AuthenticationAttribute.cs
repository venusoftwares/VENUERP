using System;
using System.Text;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;

namespace VENUERP.Providers
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            try
            {
                HttpSessionStateBase session = filterContext.HttpContext.Session;
                Controller controller = filterContext.Controller as Controller;

                if (controller != null)
                {
                    if (session["ComCode"] == null)
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
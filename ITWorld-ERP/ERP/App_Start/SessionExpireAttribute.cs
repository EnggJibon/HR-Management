using System.Web;
using System.Web.Mvc;
using ERP.BLL.Security;

namespace ERP
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpContext ctx = HttpContext.Current;
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if (HttpContext.Current.Session["IsLogged"] == null || session == null || LoginInformation.UserInformation == null) //HttpContext.Current.Session["IsLogged"]
            {
                HttpContext.Current.Session["IsLogged"] = false;
                LoginInformation.UserInformation = null;
                LoginInformation.PermittedMenues = null;
                filterContext.Result = new RedirectResult("~/Home/Index"); //new RedirectToRouteResult(new RouteValueDictionary {{ "Index", "Home" }});
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
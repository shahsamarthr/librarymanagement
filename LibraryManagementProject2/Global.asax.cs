using LibraryManagementProject2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LibraryManagementProject2
{
    
    //public class MyPropertyActionFilter : ActionFilterAttribute
    //{
    //    MvcApplication mv = new MvcApplication();
        
    //    public override void OnResultExecuting(ResultExecutingContext filterContext)
    //    {
    //        filterContext.Controller.ViewBag.Auth =mv.isAdminUser() ;
    //    }
    //}
    public class MvcApplication : System.Web.HttpApplication
    {
        //ApplicationDbContext db1 = new ApplicationDbContext();
        //public Boolean isAdminUser()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var user = User.Identity;
        //        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db1));
        //        var s = UserManager.GetRoles(user.GetUserId());
        //        if (s[0].ToString() == "Admin")
        //            return (true);
        //        else
        //            return (false);
        //    }
        //    return (false);
        //}

        protected void Application_Start()
        {
            
           // GlobalFilters.Filters.Add(new MyPropertyActionFilter(), 0);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.ExternalAuth.WeiXin
{
    public class RouteProvider : IRouteProvider
    {

        public void RegisterRoutes(RouteCollection routes)
        {
            // Login
            routes.MapRoute("Plugin.ExternalAuth.WeiXin.Login",
                "Plugins/ExternalAuthWeiXin/Login",
                new { controller = "Nop.Plugin.ExternalAuth.WeiXin.Controllers", action = "Login" },
                new[] { "Nop.Plugin.ExternalAuth.WeiXin.Controllers" });

            // LoginCallback
            routes.MapRoute("Plugin.ExternalAutn.WeiXin.LoginCallback",
                "Plugins/ExternalAuthWeiXin/LoginCallback",
                new { controller = "Nop.Plugin.ExternalAuth.WeiXin.Controllers", action = "LoginCallback" },
                new[] { "Nop.Plugin.ExternalAuth.WeiXin.Controllers" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}

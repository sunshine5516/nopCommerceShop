using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
    /// 名    称：RouteProvider
    /// 功    能：路由注册
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：RouteProvider.cs
    /// 创建时间：2017-08-08 03:07
    /// 修改时间：2017-08-09 03:43
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public partial class RouteProvider : IRouteProvider
    {
        #region Methods

        public void RegisterRoutes(RouteCollection routes)
        {
            //登录授权路由
            routes.MapRoute("DaBoLang.Plugin.ExternalAuth.WeiXin.Login",
                "Plugins/ExternalAuthWeiXin/Login",
                new { controller = "WeiXinExternalAuth", action = "Login" },
                new[] { "DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers" }
            );
            //微信通知路由
            routes.MapRoute("DaBoLang.Plugin.ExternalAuth.WeiXin.LoginCallback",
                "Plugins/ExternalAuthWeiXin/LoginCallback",
                new { controller = "WeiXinExternalAuth", action = "LoginCallback" },
                new[] { "DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers" }
            );

            //微信通知路由
            routes.MapRoute("DaBoLang.Plugin.ExternalAuth.WeiXin.Index",
                "Plugins/ExternalAuthWeiXin/index",
                new { controller = "WeiXinExternalAuth", action = "index" },
                new[] { "DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers" }
            );

            //微信通知路由
            routes.MapRoute("DaBoLang.Plugin.ExternalAuth.WeiXin.Test",
                "Plugins/ExternalAuthWeiXin/test",
                new { controller = "WeiXinExternalAuth", action = "Test" },
                new[] { "DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers" }
            );
        }

        #endregion

        #region Properties

        public int Priority
        {
            get
            {
                return 0;
            }
        }

        #endregion
    }
}

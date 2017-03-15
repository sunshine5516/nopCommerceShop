using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.Widgets.HelloWorld.Models;

namespace Nop.Plugin.Widgets.HelloWorld.Controllers
{
    public class WidgetsHelloWorldController : BasePluginController
    {
        public WidgetsHelloWorldController() { }
        [AdminAuthorize]
        [ChildActionOnly]
       
        public ActionResult Configure()
        {
            return View("~/Plugins/Widgets.HelloWorld/Views/WidgetsHelloWorld/Configure.cshtml");
        }
        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            return Configure();
        }
        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            return View("~/Plugins/Widgets.HelloWorld/Views/WidgetsHelloWorld/PublicInfo.cshtml", null);
        }
    }
}

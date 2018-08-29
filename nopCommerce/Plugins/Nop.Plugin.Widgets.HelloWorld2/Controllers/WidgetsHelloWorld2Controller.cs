using Nop.Plugin.Widgets.HelloWorld2.Models;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Widgets.HelloWorld2.Controllers
{
    public  class WidgetsHelloWorld2Controller : BasePluginController
    {
        public WidgetsHelloWorld2Controller() { }
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

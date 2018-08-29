using Nop.Core.Plugins;
using Nop.Services.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.plugin.Test
{
    public class TestPlugin : BasePlugin, IWidgetPlugin
    {
        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetWidgetZones()
        {
            throw new NotImplementedException();
        }
    }
}

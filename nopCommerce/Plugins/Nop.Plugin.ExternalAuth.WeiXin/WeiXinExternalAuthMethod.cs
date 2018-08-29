using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Localization;
using Nop.Services.Configuration;
using Nop.Services.Authentication.External;

namespace Nop.Plugin.ExternalAuth.WeiXin
{
    public class WeiXinExternalAuthMethod : BasePlugin, IExternalAuthenticationMethod
    {
        private readonly ISettingService _settingService;

        public WeiXinExternalAuthMethod(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ExternalAuthWeiXin";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.ExternalAuth.WeiXin.Controllers" }, { "area", null } };
        }

        public void GetPublicInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "ExternalAuthWeiXin";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.ExternalAuth.WeiXin.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// 安装插件
        /// </summary>
        public override void Install()
        {
            // settings
            var settings = new WeiXinExternalAuthSettings()
            {
                AppId = "",
                AppSecret = ""
            };
            _settingService.SaveSetting(settings);

            // locales
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.WeiXin.Login", "微信登录");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.WeiXin.AppId", "唯一凭证");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.WeiXin.AppSecret", "唯一凭证密钥");

            base.Install();
        }

        /// <summary>
        /// 卸载插件
        /// </summary>
        public override void Uninstall()
        {
            // settings
            _settingService.DeleteSetting<WeiXinExternalAuthSettings>();

            this.DeletePluginLocaleResource("Plugins.ExternalAuth.WeiXin.Login");
            this.DeletePluginLocaleResource("Plugins.ExternalAuth.WeiXin.AppId");
            this.DeletePluginLocaleResource("Plugins.ExternalAuth.WeiXin.AppSecret");
            this.DeletePluginLocaleResource("Plugins.FriendlyName.ExternalAuth.WeiXin");

            base.Uninstall();
        }
    }
}

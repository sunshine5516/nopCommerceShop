
using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Authentication.External;
using Nop.Services.Configuration;
using Nop.Services.Localization;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
    /// 名    称：WeiXinExternalAuthMethod
    /// 功    能：外部授权插件实现类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：WeiXinExternalAuthMethod.cs
    /// 创建时间：2017-08-08 03:22
    /// 修改时间：2017-08-09 03:44
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public class WeiXinExternalAuthMethod : BasePlugin, IExternalAuthenticationMethod
    {
        #region 属性

        private readonly ISettingService _settingService;

        #endregion

        #region 构造

        public WeiXinExternalAuthMethod(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        #endregion

        #region 实现 IExternalAuthenticationMethod 方法

        public void GetConfigurationRoute(out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WeiXinExternalAuth";
            routeValues = new RouteValueDictionary { { "Namespaces", "DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers" }, { "area", null } };
        }

        public void GetPublicInfoRoute(out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WeiXinExternalAuth";
            routeValues = new RouteValueDictionary { { "Namespaces", "DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers" }, { "area", null } };
        }

        #endregion

        #region 插件安装卸载
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var settings = new WeiXinAuthSettings
            {
                AppID = "",
                AppSecret = "",
            };
            _settingService.SaveSetting(settings);

            //locales
            this.AddOrUpdatePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppID", "App ID");
            this.AddOrUpdatePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppID.Hint", "输入微信公众平台 AppID.");
            this.AddOrUpdatePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppSecret", "App Secret");
            this.AddOrUpdatePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppSecret.Hint", "输入微信公众平台 AppSecrett.");

            base.Install();
        }

        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<WeiXinAuthSettings>();

            //locales
            this.DeletePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppID");
            this.DeletePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppID.Hint");
            this.DeletePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppSecret");
            this.DeletePluginLocaleResource("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppSecret.Hint");

            base.Uninstall();
        }

        #endregion
    }
}

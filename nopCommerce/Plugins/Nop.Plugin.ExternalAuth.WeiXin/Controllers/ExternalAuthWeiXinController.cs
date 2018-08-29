using System.Web.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Plugin.ExternalAuth.WeiXin.Models;

namespace Nop.Plugin.ExternalAuth.WeiXin.Controllers
{
    public class ExternalAuthWeiXinController : BasePluginController
    {
        private readonly ISettingService _settingService;
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly ILocalizationService _localizationService;

        public ExternalAuthWeiXinController(ISettingService settingService,
            IPermissionService permissionService, IStoreContext storeContext,
            IStoreService storeServie, IWorkContext workContext,
            ILocalizationService localizationService)
        {
            this._settingService = settingService;
            this._permissionService = permissionService;
            this._storeContext = storeContext;
            this._storeService = storeServie;
            this._workContext = workContext;
            this._localizationService = localizationService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageExternalAuthenticationMethods))
                return Content("Access denied");

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var weiXinExternalAuthSettings = _settingService.LoadSetting<WeiXinExternalAuthSettings>(storeScope);

            var model = new ConfigurationModel();
            model.AppId = weiXinExternalAuthSettings.AppId;
            model.AppSecret = weiXinExternalAuthSettings.AppSecret;

            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.AppIdOverrideForStore = _settingService.SettingExists(weiXinExternalAuthSettings, x => x.AppId, storeScope);
                model.AppSecretOverrideForStore = _settingService.SettingExists(weiXinExternalAuthSettings, x => x.AppSecret, storeScope);
            }

            return View("~/Plugins/ExternalAuth.WeiXin/Views/ExternalAuthWeiXin/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageExternalAuthenticationMethods))
                return Content("Access denied");

            if (!ModelState.IsValid)
                return Configure();

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var weiXinExternalAuthSettings = _settingService.LoadSetting<WeiXinExternalAuthSettings>(storeScope);

            //save settings
            weiXinExternalAuthSettings.AppId = model.AppId;
            weiXinExternalAuthSettings.AppSecret = model.AppSecret;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            if (model.AppIdOverrideForStore || storeScope == 0)
                _settingService.SaveSetting(weiXinExternalAuthSettings, x => x.AppId, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(weiXinExternalAuthSettings, x => x.AppId, storeScope);

            if (model.AppSecretOverrideForStore || storeScope == 0)
                _settingService.SaveSetting(weiXinExternalAuthSettings, x => x.AppSecret, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(weiXinExternalAuthSettings, x => x.AppSecret, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo()
        {
            return View("~/Plugins/ExternalAuth.WeiXin/Views/ExternalAuthWeiXin/PublicInfo.cshtml");
        }
    }
}

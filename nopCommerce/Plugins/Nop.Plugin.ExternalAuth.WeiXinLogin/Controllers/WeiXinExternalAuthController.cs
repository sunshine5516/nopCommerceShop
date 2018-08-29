
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Plugins;
using Nop.Services.Authentication.External;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using System.Web.Mvc;
using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Models;
using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Services;
using Nop.Web.Framework;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Honeypot;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Controllers
    /// 名    称：WeiXinExternalAuthController
    /// 功    能：
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：WeiXinExternalAuthController.cs
    /// 创建时间：2017-08-08 03:28
    /// 修改时间：2017-08-09 03:34
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public class WeiXinExternalAuthController : BasePluginController
    {
        #region 属性

        private readonly ISettingService _settingService;
        private readonly CustomerSettings _customerSettings;
        private readonly IWeiXinExternalAuthService _weiXinExternalAuthService;
        private readonly IOpenAuthenticationService _openAuthenticationService;

        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly IPermissionService _permissionService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;
        private readonly IPluginFinder _pluginFinder;
        private readonly ILocalizationService _localizationService;
        public static readonly string Token = "nopWeiXinExternalAuth";
        #endregion

        #region 构造

        public WeiXinExternalAuthController(ISettingService settingService,
            IWeiXinExternalAuthService weiXinExternalAuthService,
            IOpenAuthenticationService openAuthenticationService,
            ExternalAuthenticationSettings externalAuthenticationSettings,
            IPermissionService permissionService,
            IStoreContext storeContext,
            IStoreService storeService,
            IWorkContext workContext,
            IPluginFinder pluginFinder,
            ILocalizationService localizationService,
            CustomerSettings customerSettings)
        {
            this._settingService = settingService;
            this._weiXinExternalAuthService = weiXinExternalAuthService;
            this._openAuthenticationService = openAuthenticationService;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
            this._permissionService = permissionService;
            this._storeContext = storeContext;
            this._storeService = storeService;
            this._workContext = workContext;
            this._pluginFinder = pluginFinder;
            this._localizationService = localizationService;
            this._customerSettings = customerSettings;
        }

        #endregion

        #region 基础配置
        public ActionResult Test()
        {
            return Content("Hello java world!");
        }
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            if (CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                //返回随机字符串则表示验证通过
                return Content(echostr);
            }
            else
            {
                return Content("failed:" + signature + "," + CheckSignature.Check(timestamp, nonce, Token) + "。如果您在浏览器中看到这条信息，表明此Url可以填入微信后台。");
            }
        }
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageExternalAuthenticationMethods))
                return Content("Access denied");

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var weiXinAuthSettings = _settingService.LoadSetting<WeiXinAuthSettings>(storeScope);

            var model = new ConfigurationModel();
            model.AppID = weiXinAuthSettings.AppID;
            model.AppSecret = weiXinAuthSettings.AppSecret;

            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.AppID_OverrideForStore =
                    _settingService.SettingExists(weiXinAuthSettings, x => x.AppID, storeScope);
                model.AppSecret_OverrideForStore =
                    _settingService.SettingExists(weiXinAuthSettings, x => x.AppSecret, storeScope);
            }

            return View("~/Plugins/DaBoLang.ExternalAuth.WeiXin/Views/Configure.cshtml", model);
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
            var weiXinAuthSettings = _settingService.LoadSetting<WeiXinAuthSettings>(storeScope);

            //save settings
            weiXinAuthSettings.AppID = model.AppID;
            weiXinAuthSettings.AppSecret = model.AppSecret;


            _settingService.SaveSettingOverridablePerStore(weiXinAuthSettings, x => x.AppID,
                model.AppID_OverrideForStore, storeScope, false);
            _settingService.SaveSettingOverridablePerStore(weiXinAuthSettings, x => x.AppSecret,
                model.AppSecret_OverrideForStore, storeScope, false);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        [ChildActionOnly]
        public ActionResult PublicInfo()
        {
            return View("~/Plugins/DaBoLang.ExternalAuth.WeiXin/Views/PublicInfo.cshtml");
        }

        #endregion

        #region 新用户注册
        [NopHttpsRequirement(SslRequirement.Yes)]
        //available even when navigation is not allowed
        [PublicStoreAllowNavigation(true)]
        public virtual ActionResult Register()
        {

            //check whether registration is allowed
            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new {resultId = (int) UserRegistrationType.Disabled});

            var model = new RegisterModel();
            model.EnteringEmailTwice = _customerSettings.EnteringEmailTwice;

            return View("~/Plugins/DaBoLang.ExternalAuth.WeiXin/Views/Register.cshtml", model);
        }

        [HttpPost]
        [HoneypotValidator]
        [PublicAntiForgery]
        [ValidateInput(false)]
        [PublicStoreAllowNavigation(true)]
        public virtual ActionResult Register(RegisterModel model, string returnUrl,
            FormCollection form)
        {
            if (ModelState.IsValid)
            {
                return LoginInternal(returnUrl, true);
            }
            else
            {
                return View("~/Plugins/DaBoLang.ExternalAuth.WeiXin/Views/Register.cshtml", model);
            }
        }

        #endregion

        #region 登录验证

        public ActionResult Login(string returnUrl)
        {
            return LoginInternal(returnUrl, false);
        }

        public ActionResult LoginCallback(string returnUrl)
        {
            //if (!_workContext.CurrentCustomer.IsRegistered())
            //{
            //    var code = Request.QueryString["code"];
            //    var userInfo = _weiXinExternalAuthService.GetUserInfo(code, true);
            //    if (_weiXinExternalAuthService.GetUser(userInfo) == null)
            //    {
            //        return RedirectToAction("Register"); //未注册过则调到注册页面
            //    }
            //}
            return LoginInternal(returnUrl, true);
        }

        [NonAction]
        private ActionResult LoginInternal(string returnUrl, bool verifyResponse)
        {
            var processor =
                _openAuthenticationService.LoadExternalAuthenticationMethodBySystemName("DaBoLang.ExternalAuth.WeiXin");
            //if (processor == null ||
            //    !processor.IsMethodActive(_externalAuthenticationSettings) ||
            //    !processor.PluginDescriptor.Installed ||
            //    !_pluginFinder.AuthenticateStore(processor.PluginDescriptor, _storeContext.CurrentStore.Id) ||
            //    !_pluginFinder.AuthorizedForUser(processor.PluginDescriptor, _workContext.CurrentCustomer))
            //    throw new NopException("微信登录插件没有加载");

            if (processor == null ||
    !processor.IsMethodActive(_externalAuthenticationSettings) ||
    !processor.PluginDescriptor.Installed ||
    !_pluginFinder.AuthenticateStore(processor.PluginDescriptor, _storeContext.CurrentStore.Id))
                throw new NopException("微信登录插件没有加载");



            var result = _weiXinExternalAuthService.Authorize(returnUrl, verifyResponse);
            switch (result.AuthenticationStatus)
            {
                case OpenAuthenticationStatus.Error:
                {
                    if (!result.Success)
                        foreach (var error in result.Errors)
                            ExternalAuthorizerHelper.AddErrorsToDisplay(error);

                    return new RedirectResult(Url.LogOn(returnUrl));
                }
                case OpenAuthenticationStatus.AssociateOnLogon:
                {
                    return new RedirectResult(Url.LogOn(returnUrl));
                }
                case OpenAuthenticationStatus.AutoRegisteredEmailValidation:
                {
                    //result
                    return RedirectToRoute("RegisterResult",
                        new {resultId = (int) UserRegistrationType.EmailValidation});
                }
                case OpenAuthenticationStatus.AutoRegisteredAdminApproval:
                {
                    return RedirectToRoute("RegisterResult", new {resultId = (int) UserRegistrationType.AdminApproval});
                }
                case OpenAuthenticationStatus.AutoRegisteredStandard:
                {
                    return RedirectToRoute("RegisterResult", new {resultId = (int) UserRegistrationType.Standard});
                }
                default:
                    break;
            }
            if (result.Result != null) return result.Result;
            return HttpContext.Request.IsAuthenticated
                ? new RedirectResult(!string.IsNullOrEmpty(returnUrl) ? returnUrl : "~/")
                : new RedirectResult(Url.LogOn(returnUrl));
        }

        #endregion
    }
}

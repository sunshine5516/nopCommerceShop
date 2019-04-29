using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication.External;
using Nop.Services.Configuration;
using RestSharp;
using Newtonsoft.Json;
using System.Web.Mvc;
using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Core;
using Nop.Core.Caching;
using System.Text;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Services
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Services
    /// 名    称：WeiXinExternalAuthService
    /// 功    能：微信登录服务类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：WeiXinExternalAuthService.cs
    /// 创建时间：2017-08-08 06:52
    /// 修改时间：2017-08-09 03:39
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public class WeiXinExternalAuthService : IWeiXinExternalAuthService
    {
        #region 属性

        private const string WEIXIN_USER_CUSTOMERID = "DaBoLang.WeiXin.UserInfo-{0}";
        private readonly ICacheManager _cacheManager;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;
        private readonly HttpContextBase _httpContext;
        private readonly IExternalAuthorizer _authorizer;

        #endregion

        #region 构造

        public WeiXinExternalAuthService(ISettingService settingService,
            IWebHelper webHelper,
            IOpenAuthenticationService openAuthenticationService,
            ExternalAuthenticationSettings externalAuthenticationSettings,
            IStoreContext storeContext,
            IWorkContext workContext,
            HttpContextBase httpContext,
            IExternalAuthorizer authorizer,
            ICacheManager cacheManager)
        {
            this._settingService = settingService;
            this._openAuthenticationService = openAuthenticationService;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
            this._storeContext = storeContext;
            this._workContext = workContext;
            this._webHelper = webHelper;
            this._httpContext = httpContext;
            this._authorizer = authorizer;
            this._cacheManager = cacheManager;
        }


        #endregion

        #region 验证相关

        public AuthorizeState Authorize(string returnUrl, bool? verifyResponse = default(bool?))
        {
            if (!verifyResponse.HasValue)
                throw new ArgumentException("微信插件验证错误");

            if (verifyResponse.Value)
                return VerifyAuthentication(returnUrl);

            return RequestAuthentication();
        }

        /// <summary>
        /// 登录验证地址
        /// </summary>
        /// <returns></returns>
        private AuthorizeState RequestAuthentication()
        {
            var authUrl = GenerateServiceLoginUrl().AbsoluteUri;
            return new AuthorizeState("", OpenAuthenticationStatus.RequiresRedirect)
            {
                Result = new RedirectResult(authUrl)
            };
        }

        /// <summary>
        /// 回调方法
        /// </summary>
        /// <returns></returns>
        private Uri GenerateLocalCallbackUri()
        {
            string redirect_uri = string.Format("{0}Plugins/ExternalAuthWeiXin/LoginCallback",
                _webHelper.GetStoreLocation(false));
            //string redirect_uri="http://118.25.189.62/nop/Plugins/ExternalAuthWeiXin/LoginCallback";
            //string redirect_uri = "http://118.25.189.62/nop/Plugins/ExternalAuthWeiXin/Login";
            var url = this.GetAuthorizeUrl(redirect_uri: redirect_uri, scope: "snsapi_userinfo");
            return new Uri(url);
        }

        /// <summary>
        /// 登录地址,微信登录
        /// </summary>
        /// <returns></returns>
        private Uri GenerateServiceLoginUrl()
        {
            string redirect_uri = string.Format("{0}Plugins/ExternalAuthWeiXin/LoginCallback",
                _webHelper.GetStoreLocation(false));
            //string redirect_uri= "http://118.25.189.62/nop/Plugins/ExternalAuthWeiXin/LoginCallback";
            //string redirect_uri = "http://118.25.189.62/nop/Plugins/ExternalAuthWeiXin/Login";
            var url = this.GetAuthorizeUrl(redirect_uri: redirect_uri, scope: "snsapi_userinfo");
            return new Uri(url);
        }

        #region 授权验证

        private AuthorizeState VerifyAuthentication(string returnUrl)
        {
            var authResult = GenerateLocalCallbackUri();
            int i;
            var coll = _httpContext.Request.QueryString;
            var sortedStr = coll.AllKeys;
            SortedDictionary<string, string> sPara = new SortedDictionary<string, string>();
            for (i = 0; i < sortedStr.Length; i++)
            {
                sPara.Add(sortedStr[i], coll[sortedStr[i]]);
            }

            WeiXinUserInfoResponse userInfo = null;
            if (sPara.Keys.Contains("code"))
            {
                var code = sPara["code"];
                userInfo = this.GetUserInfo(code);
            }
            if (userInfo == null)
            {
                var currentCustomerId = _workContext.CurrentCustomer.Id;
                if (currentCustomerId != 0)
                {
                    var key = string.Format(WEIXIN_USER_CUSTOMERID, currentCustomerId);
                    userInfo = _cacheManager.Get<WeiXinUserInfoResponse>(key);
                    _cacheManager.Remove(key);
                }
            }
            var email = _httpContext.Request.Form["Email"]; //获取注册邮箱
            if (userInfo != null)
            {
                var parameters = new OAuthAuthenticationParameters(Provider.SystemName)
                {
                    ExternalIdentifier = userInfo.openid,
                    ExternalDisplayIdentifier = userInfo.unionid,
                };
                if (_externalAuthenticationSettings.AutoRegisterEnabled) //判断是否允许自由注册
                    ParseClaims(userInfo, parameters, email);

                var result = _authorizer.Authorize(parameters);

                return new AuthorizeState(returnUrl, result);

            }
            else
            {
                var state = new AuthorizeState(returnUrl, OpenAuthenticationStatus.Error);
                var error = "获取用户信息失败";
                state.AddError(error);
                return state;
            }
        }

        private void ParseClaims(WeiXinUserInfoResponse userInfo, OAuthAuthenticationParameters parameters,
            string email)
        {
            var claims = new UserClaims();
            claims.Contact = new ContactClaims()
            {
                Email = email,
            };
            claims.Contact.Address = new AddressClaims()
            {
                City = userInfo.city,
                State = userInfo.province,
                Country = userInfo.country,
            };
            claims.Name = new NameClaims()
            {
                Nickname = userInfo.nickname,
            };

            parameters.AddClaim(claims);
        }

        public Customer GetUser(WeiXinUserInfoResponse userInfo)
        {
            var parameters = new OAuthAuthenticationParameters(Provider.SystemName)
            {
                ExternalIdentifier = userInfo.openid,
                ExternalDisplayIdentifier = userInfo.unionid,
            };
            return _openAuthenticationService.GetUser(parameters);
        }

        #endregion

        #endregion

        #region 微信登录方法

        public bool CheckAccessToken(string access_token, string openid)
        {
            var client = new RestClient(" https://api.weixin.qq.com/sns");
            var request = new RestRequest("auth?access_token={access_token}&openid={openid}", Method.GET);
            request.AddUrlSegment("openid", openid);
            request.AddUrlSegment("access_token", access_token);
            IRestResponse response = client.Execute(request);
            ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            return errorResponse.errcode == "0";
        }

        public WeiXinResponse GetAccessToken(string code)
        {
            var weiXinAuthSettings = _settingService.LoadSetting<WeiXinAuthSettings>(_storeContext.CurrentStore.Id);
            var client = new RestClient("https://api.weixin.qq.com/sns/oauth2");

            var request =
                new RestRequest("access_token?appid={appid}&secret={secret}&code={code}&grant_type=authorization_code",
                    Method.GET);
            request.AddUrlSegment("appid", weiXinAuthSettings.AppID);
            request.AddUrlSegment("secret", weiXinAuthSettings.AppSecret);
            request.AddUrlSegment("code", code);
            //  request.AddHeader("header", "value");
            IRestResponse response = client.Execute(request);
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            if (errorResponse.errcode == null)
            {
                var obj = JsonConvert.DeserializeObject<WeiXinAuthorizationSuccessResponse>(response.Content);
                return new WeiXinResponse {Obj = obj};
            }
            return new WeiXinResponse {Error = errorResponse};
        }

        public string GetAuthorizeUrl(string redirect_uri, string scope = "", string state = "")
        {
            var weiXinAuthSettings = _settingService.LoadSetting<WeiXinAuthSettings>(_storeContext.CurrentStore.Id);


            var client = new RestClient("https://open.weixin.qq.com");
            var request =
                new RestRequest(
                    "connect/oauth2/authorize?appid={appid}&redirect_uri={redirect_uri}&response_type=code&scope={scope}&state=STATE#wechat_redirect",
                    Method.GET);
            //var client = new RestClient("https://open.weixin.qq.com");
            //var request =
            //    new RestRequest(
            //        "connect/qrconnect?appid={appid}&redirect_uri={redirect_uri}&response_type=code&scope={scope}&state=STATE#wechat_redirect",
            //        Method.GET);
            request.AddUrlSegment("appid", weiXinAuthSettings.AppID);
            request.AddUrlSegment("redirect_uri", redirect_uri);
            request.AddUrlSegment("scope", "snsapi_userinfo");
            //request.AddUrlSegment("scope", "snsapi_login");
            var uri = client.BuildUri(request);
            return uri.OriginalString;
          
        }

        public WeiXinResponse GetUserInfo(string access_token, string openid, string lang = "zh_CN")
        {
            var client = new RestClient(" https://api.weixin.qq.com/sns");
            var request = new RestRequest("userinfo?access_token={access_token}&openid={openid}&lang=zh_CN ",
                Method.GET);
            request.AddUrlSegment("access_token", access_token);
            request.AddUrlSegment("openid", openid);
            IRestResponse response = client.Execute(request);
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            if (errorResponse.errcode == null)
            {
                var obj = JsonConvert.DeserializeObject<WeiXinUserInfoResponse>(response.Content);
                return new WeiXinResponse {Obj = obj};
            }
            return new WeiXinResponse {Error = errorResponse};
        }

        public WeiXinResponse RefreshToken(string refresh_token)
        {
            var weiXinAuthSettings = _settingService.LoadSetting<WeiXinAuthSettings>(_storeContext.CurrentStore.Id);
            var client = new RestClient("https://api.weixin.qq.com/sns/oauth2");
            var request =
                new RestRequest("refresh_token?appid={appid}&grant_type=refresh_token&refresh_token={refresh_token}",
                    Method.GET);
            request.AddUrlSegment("appid", weiXinAuthSettings.AppID);
            request.AddUrlSegment("refresh_token", refresh_token);
            IRestResponse response = client.Execute(request);
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
            if (errorResponse.errcode == null)
            {
                var obj = JsonConvert.DeserializeObject<WeiXinAuthorizationSuccessResponse>(response.Content);
                return new WeiXinResponse {Obj = obj};
            }
            return new WeiXinResponse {Error = errorResponse};
        }

        public WeiXinUserInfoResponse GetUserInfo(string code, bool cache = false)
        {
            var weiXinResponse = this.GetAccessToken(code);
            if (weiXinResponse.Obj != null)
            {
                var isOk = typeof(WeiXinAuthorizationSuccessResponse).IsAssignableFrom(weiXinResponse.Obj.GetType());
                WeiXinAuthorizationSuccessResponse authorizationSuccessResponse =
                    isOk ? (WeiXinAuthorizationSuccessResponse) weiXinResponse.Obj : null;
                if (authorizationSuccessResponse != null) //获取授权
                {
                    var checkAccessToken =
                        this.CheckAccessToken(authorizationSuccessResponse.access_token,
                            authorizationSuccessResponse.openid); //检查授权是否过期
                    if (!checkAccessToken)
                    {
                        authorizationSuccessResponse =
                            (WeiXinAuthorizationSuccessResponse) this
                                .RefreshToken(authorizationSuccessResponse.refresh_token).Obj;
                    }
                    weiXinResponse = this.GetUserInfo(authorizationSuccessResponse.access_token,
                        authorizationSuccessResponse.openid); //获取用户信息
                    isOk = typeof(WeiXinUserInfoResponse).IsAssignableFrom(weiXinResponse.Obj.GetType());
                    if (isOk)
                    {
                        var userInfo = (WeiXinUserInfoResponse) weiXinResponse.Obj;
                        if (cache)
                        {
                            var currentCustomerId = _workContext.CurrentCustomer.Id;
                            if (currentCustomerId != 0)
                            {
                                var key = string.Format(WEIXIN_USER_CUSTOMERID, currentCustomerId);
                                _cacheManager.Set(key, userInfo, 5); //保存5分钟
                            }

                        }
                        return userInfo;
                    }

                }
            }
            return null;
        }
    }

    #endregion
}

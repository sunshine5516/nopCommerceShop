using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using DotNetOpenAuth.AspNet;

namespace Nop.Plugin.ExternalAuth.WeiXin.Asp.Net
{
    public abstract class OpenAuthenticationClient : IAuthenticationClient
    {
        private readonly string _providerName;

        public OpenAuthenticationClient(string providerName)
        {
            this._providerName = providerName;
        }

        /// <summary>
        /// 获得服务器的授权地址
        /// </summary>
        /// <param name="returnUrl">回调地址</param>
        /// <returns>服务器的授权地址</returns>
        protected abstract Uri GetServiceLoginUrl(Uri returnUrl);

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="openIdData">取用户信息的链接地址参数</param>
        /// <returns>用户信息</returns>
        protected abstract IDictionary<string, string> GetUserData(IDictionary<string, string> openIdData);

        /// <summary>
        /// 根据授权服务器返回的Code取Access_token
        /// </summary>
        /// <param name="authorizationCode">授权服务器返回的code</param>
        /// <returns>Access_token值</returns>
        protected abstract IDictionary<string, string> QueryAccessToken(string authorizationCode);

        public void RequestAuthentication(HttpContextBase context, Uri returnUrl)
        {
            string absoluteUri = this.GetServiceLoginUrl(returnUrl).AbsoluteUri;
            context.Response.Redirect(absoluteUri, true);
        }

        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public AuthenticationResult VerifyAuthentication(HttpContextBase context)
        {
            string name;
            string code = context.Request.QueryString["code"];
            if (String.IsNullOrEmpty(code))
            {
                return AuthenticationResult.Failed;
            }

            IDictionary<string, string> openIdData = this.QueryAccessToken(code);
            if (openIdData == null)
            {
                return AuthenticationResult.Failed;
            }

            IDictionary<string, string> userData = this.GetUserData(openIdData);
            if (userData == null)
            {
                return AuthenticationResult.Failed;
            }

            string providerUserId = openIdData["open_id"];
            if (!userData.TryGetValue("nick_name", out name))
            {
                name = providerUserId;
            }
            userData.AddItemIfNotEmpty("access_token", openIdData["access_token"]);
            userData.AddItemIfNotEmpty("id", openIdData["open_id"]);
            userData.AddItemIfNotEmpty("email", providerUserId + "@weixin.com");

            return new AuthenticationResult(true, this.ProviderName, providerUserId, name, userData);
        }

        public string ProviderName
        {
            get { return _providerName; }
        }
    }
}

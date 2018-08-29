using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication.External;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Services
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Services
    /// 名    称：IWeiXinExternalAuthService
    /// 功    能：微信登录服务类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：IWeiXinExternalAuthService.cs
    /// 创建时间：2017-08-08 06:24
    /// 修改时间：2017-08-09 03:39
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public interface IWeiXinExternalAuthService : IExternalProviderAuthorizer
    {
        /// <summary>
        /// 1.获取用户授权
        /// </summary>
        /// <param name="redirect_uri">跳转回调redirect_uri，应当使用https链接来确保授权code的安全性。</param>
        /// <param name="scope">应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）</param>
        /// <param name="state">重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节</param>
        /// <returns></returns>
        string GetAuthorizeUrl(string redirect_uri, string scope="",string state = "");

        /// <summary>
        /// 2.通过code换取网页授权access_token
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <returns></returns>
        WeiXinResponse GetAccessToken(string code) ;
        /// <summary>
        /// 3.刷新access_token（如果需要）
        /// </summary>
        /// <param name="refresh_token"></param>
        /// <returns></returns>
        WeiXinResponse RefreshToken(string refresh_token);
        /// <summary>
        /// 4.拉取用户信息(需scope为 snsapi_userinfo)
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        WeiXinResponse GetUserInfo(string access_token,string openid,string lang= "zh_CN");
        /// <summary>
        /// 检验授权凭证（access_token）是否有效
        /// </summary>
        /// <param name="access_token">网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同</param>
        /// <param name="openid">用户的唯一标识</param>
        /// <returns></returns>
        bool CheckAccessToken(string access_token, string openid);
        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <param name="cache">是否缓存用户信息</param>
        /// <returns></returns>
        WeiXinUserInfoResponse GetUserInfo(string code, bool cache=false);
        /// <summary>
        /// 获取授权用户
        /// </summary>
        /// <param name="userInfo">微信授权用户信息</param>
        /// <returns>关联用户</returns>
        Customer GetUser(WeiXinUserInfoResponse userInfo );

    }

    
}

using Nop.Services.Authentication.External;
using System;
using System.Collections.Generic;


namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Core
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Core
    /// 名    称：OAuthAuthenticationParameters
    /// 功    能：新用户注册参数
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：OAuthAuthenticationParameters.cs
    /// 创建时间：2017-08-09 09:53
    /// 修改时间：2017-08-09 03:38
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    [Serializable]
    public class OAuthAuthenticationParameters : OpenAuthenticationParameters
    {
        private readonly string _providerSystemName;
        private IList<UserClaims> _claims;

        public OAuthAuthenticationParameters(string providerSystemName)
        {
            this._providerSystemName = providerSystemName;
        }

        public override IList<UserClaims> UserClaims
        {
            get
            {
                return _claims;
            }
        }

        public void AddClaim(UserClaims claim)
        {
            if (_claims == null)
                _claims = new List<UserClaims>();

            _claims.Add(claim);
        }

        public override string ProviderSystemName
        {
            get { return _providerSystemName; }
        }
    }
}

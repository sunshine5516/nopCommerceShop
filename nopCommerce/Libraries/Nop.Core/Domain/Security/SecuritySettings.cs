using System.Collections.Generic;
using Nop.Core.Configuration;

namespace Nop.Core.Domain.Security
{
    public class SecuritySettings : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether all pages will be forced to use SSL (no matter of a specified [NopHttpsRequirementAttribute] attribute)
        /// </summary>
        public bool ForceSslForAllPages { get; set; }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string EncryptionKey { get; set; }

        /// <summary>
        /// 管理区域允许的IP地址列表
        /// </summary>
        public List<string> AdminAreaAllowedIpAddresses { get; set; }

        /// <summary>
        /// 是否应启用管理区域的XSRF保护
        /// </summary>
        public bool EnableXsrfProtectionForAdminArea { get; set; }
        /// <summary>
        ///是否应启用公共存储的XSRF保护
        /// </summary>
        public bool EnableXsrfProtectionForPublicStore { get; set; }

        /// <summary>
        /// 是否在注册页面上启用蜜罐技术
        /// </summary>
        public bool HoneypotEnabled { get; set; }
        /// <summary>
        /// 蜜罐输入名称
        /// </summary>
        public string HoneypotInputName { get; set; }
    }
}
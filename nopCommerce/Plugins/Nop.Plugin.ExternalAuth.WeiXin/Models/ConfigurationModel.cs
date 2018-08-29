using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.ExternalAuth.WeiXin.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        /// <summary>
        /// 当前正是使用的那个商城（商城配置)
        /// </summary>
        public int ActiveStoreScopeConfiguration { get; set; }

        /// <summary>
        /// 唯一凭证
        /// </summary>
        [NopResourceDisplayName("Plugins.ExternalAuth.WeiXin.AppId")]
        public string AppId { get; set; }
        public bool AppIdOverrideForStore { get; set; }

        /// <summary>
        /// 唯一凭证密钥
        /// </summary>
        [NopResourceDisplayName("Plugins.ExternalAuth.WeiXin.AppSecret")]
        public string AppSecret { get; set; }
        public bool AppSecretOverrideForStore { get; set; }
    }
}

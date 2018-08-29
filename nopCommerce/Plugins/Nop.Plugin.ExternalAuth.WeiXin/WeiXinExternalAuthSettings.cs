using Nop.Core.Configuration;

namespace Nop.Plugin.ExternalAuth.WeiXin
{
    public class WeiXinExternalAuthSettings : ISettings
    {
        /// <summary>
        /// 唯一凭证
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 唯一凭证密钥
        /// </summary>
        public string AppSecret { get; set; }
    }
}

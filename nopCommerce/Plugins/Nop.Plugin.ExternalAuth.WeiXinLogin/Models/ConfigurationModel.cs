using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Models
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Models
    /// 名    称：ConfigurationModel
    /// 功    能：微信公众平台配置模型类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：ConfigurationModel.cs
    /// 创建时间：2017-08-08 03:07
    /// 修改时间：2017-08-09 03:37
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppID")]
        public string AppID { get; set; }
        public bool AppID_OverrideForStore { get; set; }

        [NopResourceDisplayName("DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.AppSecret")]
        public string AppSecret { get; set; }
        public bool AppSecret_OverrideForStore { get; set; }
    }
}
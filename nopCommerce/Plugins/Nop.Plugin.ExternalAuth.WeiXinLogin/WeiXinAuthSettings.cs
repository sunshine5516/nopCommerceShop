
using Nop.Core.Configuration;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin
    /// 名    称：WeiXinAuthSettings
    /// 功    能：微信公众平台配置类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：WeiXinAuthSettings.cs
    /// 创建时间：2017-08-08 03:18
    /// 修改时间：2017-08-09 03:44
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public class WeiXinAuthSettings : ISettings
   {
       public string AppID { get; set; }
       public string AppSecret { get; set; }
    }
}

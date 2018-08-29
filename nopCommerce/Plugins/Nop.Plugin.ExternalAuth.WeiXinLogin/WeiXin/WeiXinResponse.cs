
namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin
    /// 名    称：WeiXinResponse
    /// 功    能：微信公众平台响应基类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：WeiXinResponse.cs
    /// 创建时间：2017-08-08 06:40
    /// 修改时间：2017-08-08 06:43
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public class WeiXinResponse
    {
        public string Code { get; set; }
        /// <summary>
        /// 返回对象
        /// </summary>
        public object Obj { get; set; }
        /// <summary>
        /// 错误返回
        /// </summary>
        public ErrorResponse Error{ get; set; }
}
}


namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin
    /// 名    称：WeiXinAuthorizationSuccessResponse
    /// 功    能：用户同意授权时响应类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：WeiXinAuthorizationSuccessResponse.cs
    /// 创建时间：2017-08-08 05:32
    /// 修改时间：2017-08-08 06:43
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    public partial class WeiXinAuthorizationSuccessResponse
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
    }
}

using System;


namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.WeiXin
    /// 名    称：WeiXinUserInfoResponse
    /// 功    能：拉取用户成功时响应类
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：WeiXinUserInfoResponse.cs
    /// 创建时间：2017-08-08 06:18
    /// 修改时间：2017-08-08 06:42
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    [Serializable]
    public   class WeiXinUserInfoResponse
    {
        /// <summary>
        /// 用户唯一标示
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 普通用户个人资料填写的城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 用户个人资料填写的省份
        /// </summary>
        public string province { get; set; }
        /// <summary>
        /// 国家，如中国为CN
        /// </summary>
        public string country { get; set; }
       
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
        /// </summary>
        public string headimgurl { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。
        /// </summary>
        public string unionid { get; set; }

    }
}

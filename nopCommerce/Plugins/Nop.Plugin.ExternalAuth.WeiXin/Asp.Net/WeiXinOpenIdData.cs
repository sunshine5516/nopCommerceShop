using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Nop.Plugin.ExternalAuth.WeiXin.Asp.Net
{
    [EditorBrowsable(EditorBrowsableState.Never), DataContract]
    public class WeiXinOpenIdData
    {
        [DataMember(Name = "openid")]
        public string OpenId { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Nop.Plugin.ExternalAuth.WeiXin.Asp.Net
{
    [EditorBrowsable(EditorBrowsableState.Never), DataContract]
    public class WeiXinGraphData
    {
        [DataMember(Name = "nickname")]
        public string NickName { get; set; }

        [DataMember(Name = "sex")]
        public string Gender { get; set; }
    }
}

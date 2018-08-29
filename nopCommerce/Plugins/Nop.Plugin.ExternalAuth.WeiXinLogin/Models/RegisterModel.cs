using System.Web.Mvc;
using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Validators;
using FluentValidation.Attributes;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Models
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Models
    /// 名    称：RegisterModel
    /// 功    能：注册使用,主要提供邮箱
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：RegisterModel.cs
    /// 创建时间：2017-08-09 10:57
    /// 修改时间：2017-08-09 03:36
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// </summary>
    [Validator(typeof(RegisterValidator))]
    public partial class RegisterModel : BaseNopModel
    {

        [NopResourceDisplayName("Account.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }
        public bool EnteringEmailTwice { get; set; }
        [NopResourceDisplayName("Account.Fields.ConfirmEmail")]
        [AllowHtml]
        public string ConfirmEmail { get; set; }

    }
}

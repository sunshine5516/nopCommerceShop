using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Models;
using FluentValidation;
using FluentValidation.Results;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Web.Framework.Validators;

namespace DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Validators
{
    /// <summary>
    /// 命名空间：DaBoLang.Nop.Plugin.ExternalAuth.WeiXin.Validators
    /// 名    称：RegisterValidator
    /// 功    能：注册模型验证
    /// 详    细：
    /// 版    本：1.0.0.0
    /// 文件名称：RegisterValidator.cs
    /// 创建时间：2017-08-09 11:46
    /// 修改时间：2017-08-09 03:37
    /// 作    者：大波浪
    /// 联系方式：http://www.cnblogs.com/yaoshangjin
    /// 说    明：
    /// 验证邮箱是否被注册过
    /// </summary>
    public partial class RegisterValidator : BaseNopValidator<RegisterModel>
    {
        public RegisterValidator(ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            CustomerSettings customerSettings, ICustomerService _customerService)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));

            if (customerSettings.EnteringEmailTwice)
            {
                RuleFor(x => x.ConfirmEmail).NotEmpty().WithMessage(localizationService.GetResource("Account.Fields.ConfirmEmail.Required"));
                RuleFor(x => x.ConfirmEmail).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));
                RuleFor(x => x.ConfirmEmail).Equal(x => x.Email).WithMessage(localizationService.GetResource("Account.Fields.Email.EnteredEmailsDoNotMatch"));
            }
            Custom(x =>
            {
                var customer = _customerService.GetCustomerByEmail(x.Email);
                if (customer!=null)
                {
                    return new ValidationFailure("Email", localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists"));
                }
                return null;
            });
        }
    }
}

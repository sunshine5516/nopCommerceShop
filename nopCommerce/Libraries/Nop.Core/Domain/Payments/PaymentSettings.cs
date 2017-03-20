using System.Collections.Generic;
using Nop.Core.Configuration;

namespace Nop.Core.Domain.Payments
{
    public class PaymentSettings : ISettings
    {
        public PaymentSettings()
        {
            ActivePaymentMethodSystemNames = new List<string>();
        }

        /// <summary>
        /// 活动付款方式的系统名称
        /// </summary>
        public List<string> ActivePaymentMethodSystemNames { get; set; }

        /// <summary>
        /// 客户是否允许重定向付款方式的付款（完成）
        /// </summary>
        public bool AllowRePostingPayments { get; set; }

        /// <summary>
        /// 如果我们只有一种付款方式，是否应绕过“选择付款方式”页
        /// </summary>
        public bool BypassPaymentMethodSelectionIfOnlyOne { get; set; }
    }
}
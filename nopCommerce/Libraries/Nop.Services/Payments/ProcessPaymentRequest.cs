using System;
using System.Collections.Generic;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Payments
{
    /// <summary>
    /// Represents a payment info holder
    /// </summary>
    [Serializable]
    public partial class ProcessPaymentRequest
    {
        public ProcessPaymentRequest()
        {
            this.CustomValues = new Dictionary<string, object>();
        }

        /// <summary>
        /// StoreId
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// CustomerId
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 获取或设置订单唯一标识符。 在订单尚未保存时使用（付款网关不将客户重定向到第三方网址）
        /// </summary>
        public Guid OrderGuid { get; set; }

        /// <summary>
        /// 获取或设置订单总计
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// /// <summary>
        /// 获取或设置付款方式Id
        /// </summary>
        /// </summary>
        public string PaymentMethodSystemName { get; set; }

        #region 付款方式具体属性 

        /// <summary>
        /// 获取或设置信用卡类型（Visa，Master Card等...）。 如果没有被支付网关使用，我们将其留空
        /// </summary>
        public string CreditCardType { get; set; }

        /// <summary>
        /// 获取或设置信用卡所有者名称
        /// </summary>
        public string CreditCardName { get; set; }

        /// <summary>
        /// 获取或设置信用卡号码
        /// </summary>
        public string CreditCardNumber { get; set; }

        /// <summary>
        /// 获取或设置信用卡到期年份
        /// </summary>
        public int CreditCardExpireYear { get; set; }

        /// <summary>
        /// 获取或设置信用卡到期月份
        /// </summary>
        public int CreditCardExpireMonth { get; set; }

        /// <summary>
        /// 获取或设置信用卡CVV2（卡验证值）
        /// </summary>
        public string CreditCardCvv2 { get; set; }

        #endregion

        #region 定期付款

        /// <summary>
        /// 如果订单是重复的，则获取或设置初始（父）订单标识符
        /// </summary>
        public int InitialOrderId { get; set; }
        
        /// <summary>
        /// Gets or sets the cycle length
        /// </summary>
        public int RecurringCycleLength { get; set; }

        /// <summary>
        /// Gets or sets the cycle period
        /// </summary>
        public RecurringProductCyclePeriod RecurringCyclePeriod { get; set; }

        /// <summary>
        /// Gets or sets the total cycles
        /// </summary>
        public int RecurringTotalCycles { get; set; }

        #endregion

        /// <summary>
        /// 客户字典，这个属性可以存储任意客户信息
        /// </summary>
        public Dictionary<string, object> CustomValues { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Payments;

namespace Nop.Services.Payments
{
    /// <summary>
    /// 处理付款结果
    /// </summary>
    public partial class ProcessPaymentResult
    {
        private PaymentStatus _newPaymentStatus = PaymentStatus.Pending;

        /// <summary>
        /// Ctor
        /// </summary>
        public ProcessPaymentResult() 
        {
            this.Errors = new List<string>();
        }

        /// <summary>
        /// 请求是否已成功完成
        /// </summary>
        public bool Success
        {
            get { return (!this.Errors.Any()); }
        }

        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        /// <summary>
        /// 错误列表
        /// </summary>
        public IList<string> Errors { get; set; }


        /// <summary>
        /// Gets or sets an AVS result
        /// </summary>
        public string AvsResult { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AuthorizationTransactionId { get; set; }

        /// <summary>
        /// 获取或设置授权事务代码
        /// </summary>
        public string AuthorizationTransactionCode { get; set; }

        /// <summary>
        /// 获取或设置授权交易结果
        /// </summary>
        public string AuthorizationTransactionResult { get; set; }

        /// <summary>
        /// 获取或设置捕获事务标识符
        /// </summary>
        public string CaptureTransactionId { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public string CaptureTransactionResult { get; set; }

        /// <summary>
        /// 获取或设置预订事务标识符
        /// </summary>
        public string SubscriptionTransactionId { get; set; }

        /// <summary>
        /// 是否允许存储信用卡号，CVV2
        /// </summary>
        public bool AllowStoringCreditCardNumber { get; set; }

        /// <summary>
        /// 处理后的付款状态
        /// </summary>
        public PaymentStatus NewPaymentStatus
        {
            get
            {
                return _newPaymentStatus;
            }
            set
            {
                _newPaymentStatus = value;
            }
        }
    }
}

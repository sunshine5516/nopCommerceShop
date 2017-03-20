using Nop.Core.Domain.Orders;

namespace Nop.Services.Payments
{
    /// <summary>
    ///退款结果
    /// </summary>
    public partial class RefundPaymentRequest
    {
        /// <summary>
        /// 订单
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal AmountToRefund { get; set; }

        /// <summary>
        ///是否为部分退款; 
        /// </summary>
        public bool IsPartialRefund { get; set; }
    }
}

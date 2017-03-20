using Nop.Core.Domain.Orders;

namespace Nop.Services.Payments
{
    /// <summary>
    /// 取消定期付款请求
    /// </summary>
    public partial class CancelRecurringPaymentRequest
    {
        /// <summary>
        /// Gets or sets an order
        /// </summary>
        public Order Order { get; set; }
    }
}

using Nop.Core.Domain.Orders;

namespace Nop.Services.Payments
{
    /// <summary>
    /// post处理付款请求
    /// </summary>
    public partial class PostProcessPaymentRequest
    {
        /// <summary>
        /// 获取或设置订单。 在订单已保存时使用（将客户重定向到第三方网址的付款网关）
        /// </summary>
        public Order Order { get; set; }
    }
}

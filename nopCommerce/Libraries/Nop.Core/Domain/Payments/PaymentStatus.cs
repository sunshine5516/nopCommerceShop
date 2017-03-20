
namespace Nop.Core.Domain.Payments
{
    /// <summary>
    /// 祝福状态
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// 待支付
        /// </summary>
        Pending = 10,
        /// <summary>
        /// 授权
        /// </summary>
        Authorized = 20,
        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 30,
        /// <summary>
        /// 部分退款
        /// </summary>
        PartiallyRefunded = 35,
        /// <summary>
        /// 退还
        /// </summary>
        Refunded = 40,
        /// <summary>
        /// 无效的
        /// </summary>
        Voided = 50,
    }
}

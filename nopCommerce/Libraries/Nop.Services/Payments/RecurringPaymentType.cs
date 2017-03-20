namespace Nop.Services.Payments
{
    /// <summary>
    /// 定期付款类型
    /// </summary>
    public enum RecurringPaymentType
    {
        /// <summary>
        /// 不支持
        /// </summary>
        NotSupported = 0,
        /// <summary>
        /// 手选
        /// </summary>
        Manual = 10,
        /// <summary>
        /// 自动（在支付网关站点处理付款）
        /// </summary>
        Automatic = 20
    }
}

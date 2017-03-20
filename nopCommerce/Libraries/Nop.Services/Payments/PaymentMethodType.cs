namespace Nop.Services.Payments
{
    /// <summary>
    /// 支付方法类型
    /// </summary>
    public enum PaymentMethodType
    {
        /// <summary>
        /// 所有付款信息都在网站上输入
        /// </summary>
        Standard = 10,
        /// <summary>
        /// 客户被重定向到第三方网站，以完成付款
        /// </summary>
        Redirection = 15,
        /// <summary>
        /// Button
        /// </summary>
        Button = 20,
    }
}

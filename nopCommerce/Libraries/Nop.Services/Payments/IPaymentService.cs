using System.Collections.Generic;
using Nop.Core.Domain.Orders;

namespace Nop.Services.Payments
{
    /// <summary>
    /// 付款服务接口
    /// </summary>
    public partial interface IPaymentService
    {
        /// <summary>
        /// 载入有效的付款方式
        /// </summary>
        /// <param name="filterByCustomerId">按客户筛选付款方式; null加载所有</param>
        /// <param name="storeId">仅在指定的商店中允许加载记录; 0代表所有记录</param>
        /// <param name="filterByCountryId">仅在指定国家/地区中允许加载记录; 0代表所有记录</param>
        /// <returns>Payment methods</returns>
        IList<IPaymentMethod> LoadActivePaymentMethods(int? filterByCustomerId = null, int storeId = 0, int filterByCountryId = 0);

        /// <summary>
        /// 按系统加载付款方式
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Found payment provider</returns>
        IPaymentMethod LoadPaymentMethodBySystemName(string systemName);

        /// <summary>
        /// 加载所有付款提供商
        /// </summary>
        /// <param name="storeId">仅在指定的商店中允许加载记录; 0代表所有记录</param>
        /// <param name="filterByCountryId">仅在指定国家/地区中允许加载记录; 0代表所有记录</param>
        /// <returns>Payment providers</returns>
        IList<IPaymentMethod> LoadAllPaymentMethods(int storeId = 0, int filterByCountryId = 0);

        /// <summary>
        /// 获取现在允许使用特定付款方式的国家/地区标识符列表
        /// </summary>
        /// <param name="paymentMethod">支付方法</param>
        /// <returns>A list of country identifiers</returns>
        IList<int> GetRestictedCountryIds(IPaymentMethod paymentMethod);

        /// <summary>
        /// 保存允许使用特定付款方式的国家/地区
        /// </summary>
        /// <param name="paymentMethod">支付方法</param>
        /// <param name="countryIds">A list of country identifiers</param>
        void SaveRestictedCountryIds(IPaymentMethod paymentMethod, List<int> countryIds);


        /// <summary>
        /// 处理付款
        /// </summary>
        /// <param name="processPaymentRequest">订单处理所需的付款信息</param>
        /// <returns>处理结果</returns>
        ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest);

        /// <summary>
        /// Post提交付款请求（由需要重定向到第三方网址的付款网关使用）
        /// </summary>
        /// <param name="postProcessPaymentRequest">订单处理所需的付款信息</param>
        void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest);

        /// <summary>
        /// 客户在下订单但未完成后是否可以完成付款（用于重定向付款方式）
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Result</returns>
        bool CanRePostProcessPayment(Order order);


        /// <summary>
        /// 手续费
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>Additional handling fee</returns>
        decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart, string paymentMethodSystemName);



        /// <summary>
        /// Gets a value indicating whether capture is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether capture is supported</returns>
        bool SupportCapture(string paymentMethodSystemName);

        /// <summary>
        /// 获取付款结果
        /// </summary>
        /// <param name="capturePaymentRequest">Capture payment request</param>
        /// <returns>Capture payment result</returns>
        CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest);



        /// <summary>
        /// 是否支持部分退款
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether partial refund is supported</returns>
        bool SupportPartiallyRefund(string paymentMethodSystemName);

        /// <summary>
        /// 是否支持退款
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether refund is supported</returns>
        bool SupportRefund(string paymentMethodSystemName);

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundPaymentRequest">Request</param>
        /// <returns>Result</returns>
        RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest);



        /// <summary>
        /// Gets a value indicating whether void is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether void is supported</returns>
        bool SupportVoid(string paymentMethodSystemName);

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="voidPaymentRequest">Request</param>
        /// <returns>Result</returns>
        VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest);



        /// <summary>
        /// Gets a recurring payment type of payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A recurring payment type of payment method</returns>
        RecurringPaymentType GetRecurringPaymentType(string paymentMethodSystemName);

        /// <summary>
        /// 处理定期付款
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>Process payment result</returns>
        ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest);

        /// <summary>
        ///取消定期付款
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>Result</returns>
        CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest);


        /// <summary>
        /// Gets masked credit card number
        /// </summary>
        /// <param name="creditCardNumber">Credit card number</param>
        /// <returns>Masked credit card number</returns>
        string GetMaskedCreditCardNumber(string creditCardNumber);
        
    }
}

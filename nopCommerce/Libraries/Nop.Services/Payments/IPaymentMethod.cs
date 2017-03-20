using System;
using System.Collections.Generic;
using System.Web.Routing;
using Nop.Core.Domain.Orders;
using Nop.Core.Plugins;

namespace Nop.Services.Payments
{
    /// <summary>
    /// 提供创建付款网关和方法的接口
    /// </summary>
    public partial interface IPaymentMethod : IPlugin
    {
        #region Methods

        /// <summary>
        /// 处理付款
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>Process payment result</returns>
        ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest);

        /// <summary>
        /// Post提交付款请求（由需要重定向到第三方网址的付款网关使用）
        /// </summary>
        /// <param name="postProcessPaymentRequest">Payment info required for an order processing</param>
        void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest);

        /// <summary>
        /// 在结帐期间是否应隐藏付款方式
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>true - hide; false - display.</returns>
        bool HidePaymentMethod(IList<ShoppingCartItem> cart);

        /// <summary>
        ///手续费
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>Additional handling fee</returns>
        decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart);

        /// <summary>
        /// 获取付款结果
        /// </summary>
        /// <param name="capturePaymentRequest">付款请求</param>
        /// <returns>付款结果</returns>
        CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest);

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundPaymentRequest">退款请求</param>
        /// <returns>Result</returns>
        RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest);

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="voidPaymentRequest">Request</param>
        /// <returns>Result</returns>
        VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest);

        /// <summary>
        /// 处理定期付款
        /// </summary>
        /// <param name="processPaymentRequest">订单处理所需的付款信息</param>
        /// <returns>处理结果</returns>
        ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest);

        /// <summary>
        /// 取消定期付款
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>Result</returns>
        CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest);

        /// <summary>
        /// 客户在下订单但未完成后是否可以完成付款（用于重定向付款方式）
        /// </summary>
        /// <param name="order">订单</param>
        /// <returns>Result</returns>
        bool CanRePostProcessPayment(Order order);

        /// <summary>
        ///获取供应商配置的路由
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);

        /// <summary>
        /// 获取付款信息的路由
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues);

        Type GetControllerType();

        #endregion

        #region 属性

        /// <summary>
        /// Gets a value indicating whether capture is supported
        /// </summary>
        bool SupportCapture { get; }

        /// <summary>
        /// 是否支持部分退款
        /// </summary>
        bool SupportPartiallyRefund { get; }

        /// <summary>
        /// 是否支持退款
        /// </summary>
        bool SupportRefund { get; }

        /// <summary>
        /// Gets a value indicating whether void is supported
        /// </summary>
        bool SupportVoid { get; }

        /// <summary>
        /// 获取定期付款方式的付款方式
        /// </summary>
        RecurringPaymentType RecurringPaymentType { get; }

        /// <summary>
        ///获取付款方式类型
        /// </summary>
        PaymentMethodType PaymentMethodType { get; }

        /// <summary>
        /// 指示是否要显示此插件的付款信息页面
        /// </summary>
        bool SkipPaymentInfo { get; }

        #endregion
    }
}

using Nop.Core.Plugins;
using Nop.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.Alipay
{
    public class AliPayPaymentProcessor : BasePlugin, IPaymentMethod
    {
        /// <summary>
        ///获取付款方式类型
        /// </summary>
        public PaymentMethodType PaymentMethodType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 获取定期付款方式的付款方式
        /// </summary>
        public RecurringPaymentType RecurringPaymentType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 指示是否要显示此插件的付款信息页面
        /// </summary>
        public bool SkipPaymentInfo
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool SupportCapture
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 是否支持部分退款
        /// </summary>
        public bool SupportPartiallyRefund
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 是否支持退款
        /// </summary>
        public bool SupportRefund
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool SupportVoid
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        /// <summary>
        /// 取消定期付款
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 客户在下订单但未完成后是否可以完成付款（用于重定向付款方式）
        /// </summary>
        /// <param name="order">订单</param>
        /// <returns>Result</returns>
        public bool CanRePostProcessPayment(Order order)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取付款结果
        /// </summary>
        /// <param name="capturePaymentRequest">付款请求</param>
        /// <returns>付款结果</returns>
        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///手续费
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>Additional handling fee</returns>
        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///获取供应商配置的路由
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }

        public Type GetControllerType()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取付款信息的路由
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetPaymentInfoRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 在结帐期间是否应隐藏付款方式
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>true - hide; false - display.</returns>
        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Post提交付款请求（由需要重定向到第三方网址的付款网关使用）
        /// </summary>
        /// <param name="postProcessPaymentRequest">Payment info required for an order processing</param>
        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 处理付款
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>Process payment result</returns>

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 处理定期付款
        /// </summary>
        /// <param name="processPaymentRequest">订单处理所需的付款信息</param>
        /// <returns>处理结果</returns>

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundPaymentRequest">退款请求</param>
        /// <returns>Result</returns>
        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}

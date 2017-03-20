using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Plugins;
using Nop.Services.Catalog;
using Nop.Services.Configuration;

namespace Nop.Services.Payments
{
    /// <summary>
    /// Payment service
    /// </summary>
    public partial class PaymentService : IPaymentService
    {
        #region Fields

        private readonly PaymentSettings _paymentSettings;
        private readonly IPluginFinder _pluginFinder;
        private readonly ISettingService _settingService;
        private readonly ShoppingCartSettings _shoppingCartSettings;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="paymentSettings">Payment settings</param>
        /// <param name="pluginFinder">Plugin finder</param>
        /// <param name="settingService">Setting service</param>
        /// <param name="shoppingCartSettings">Shopping cart settings</param>
        public PaymentService(PaymentSettings paymentSettings, 
            IPluginFinder pluginFinder,
            ISettingService settingService,
            ShoppingCartSettings shoppingCartSettings)
        {
            this._paymentSettings = paymentSettings;
            this._pluginFinder = pluginFinder;
            this._settingService = settingService;
            this._shoppingCartSettings = shoppingCartSettings;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 载入有效的付款方式
        /// </summary>
        /// <param name="filterByCustomerId">按客户筛选付款方式; null加载所有</param>
        /// <param name="storeId">仅在指定的商店中允许加载记录; 0代表所有记录</param>
        /// <param name="filterByCountryId">仅在指定国家/地区中允许加载记录; 0代表所有记录</param>
        /// <returns>Payment methods</returns>
        public virtual IList<IPaymentMethod> LoadActivePaymentMethods(int? filterByCustomerId = null, int storeId = 0, int filterByCountryId = 0)
        {
            return LoadAllPaymentMethods(storeId, filterByCountryId)
                   .Where(provider => _paymentSettings.ActivePaymentMethodSystemNames.Contains(provider.PluginDescriptor.SystemName, StringComparer.InvariantCultureIgnoreCase))
                   .ToList();
        }

        /// <summary>
        /// 按系统加载付款方式
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Found payment provider</returns>
        public virtual IPaymentMethod LoadPaymentMethodBySystemName(string systemName)
        {
            var descriptor = _pluginFinder.GetPluginDescriptorBySystemName<IPaymentMethod>(systemName);
            if (descriptor != null)
                return descriptor.Instance<IPaymentMethod>();

            return null;
        }

        /// <summary>
        /// 加载所有付款提供商
        /// </summary>
        /// <param name="storeId">仅在指定的商店中允许加载记录; 0代表所有记录</param>
        /// <param name="filterByCountryId">仅在指定国家/地区中允许加载记录; 0代表所有记录</param>
        /// <returns>Payment providers</returns>
        public virtual IList<IPaymentMethod> LoadAllPaymentMethods(int storeId = 0, int filterByCountryId = 0)
        {
            var paymentMethods = _pluginFinder.GetPlugins<IPaymentMethod>(storeId: storeId).ToList();
            if (filterByCountryId == 0)
                return paymentMethods;

            //filter by country
            var paymentMetodsByCountry = new List<IPaymentMethod>();
            foreach (var pm in paymentMethods)
            {
                var restictedCountryIds = GetRestictedCountryIds(pm);
                if (!restictedCountryIds.Contains(filterByCountryId))
                {
                    paymentMetodsByCountry.Add(pm);
                }
            }
            return paymentMetodsByCountry;
        }

        /// <summary>
        /// 获取现在允许使用特定付款方式的国家/地区标识符列表
        /// </summary>
        /// <param name="paymentMethod">支付方法</param>
        /// <returns>A list of country identifiers</returns>
        public virtual IList<int> GetRestictedCountryIds(IPaymentMethod paymentMethod)
        {
            if (paymentMethod == null)
                throw new ArgumentNullException("paymentMethod");

            var settingKey = string.Format("PaymentMethodRestictions.{0}", paymentMethod.PluginDescriptor.SystemName);
            var restictedCountryIds = _settingService.GetSettingByKey<List<int>>(settingKey);
            if (restictedCountryIds == null)
                restictedCountryIds = new List<int>();
            return restictedCountryIds;
        }

        /// <summary>
        /// 保存允许使用特定付款方式的国家/地区
        /// </summary>
        /// <param name="paymentMethod">支付方法</param>
        /// <param name="countryIds">A list of country identifiers</param>
        public virtual void SaveRestictedCountryIds(IPaymentMethod paymentMethod, List<int> countryIds)
        {
            if (paymentMethod == null)
                throw new ArgumentNullException("paymentMethod");

            //we should be sure that countryIds is of type List<int> (not IList<int>)
            var settingKey = string.Format("PaymentMethodRestictions.{0}", paymentMethod.PluginDescriptor.SystemName);
            _settingService.SetSetting(settingKey, countryIds);
        }


        /// <summary>
        ///处理付款
        /// </summary>
        /// <param name="processPaymentRequest">订单处理所需的付款信息</param>
        /// <returns>Process payment result</returns>
        public virtual ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            if (processPaymentRequest.OrderTotal == decimal.Zero)
            {
                var result = new ProcessPaymentResult
                {
                    NewPaymentStatus = PaymentStatus.Paid
                };
                return result;
            }

            //We should strip out any white space or dash in the CC number entered.
            if (!String.IsNullOrWhiteSpace(processPaymentRequest.CreditCardNumber))
            {
                processPaymentRequest.CreditCardNumber = processPaymentRequest.CreditCardNumber.Replace(" ", "");
                processPaymentRequest.CreditCardNumber = processPaymentRequest.CreditCardNumber.Replace("-", "");
            }
            var paymentMethod = LoadPaymentMethodBySystemName(processPaymentRequest.PaymentMethodSystemName);
            if (paymentMethod == null)
                throw new NopException("Payment method couldn't be loaded");
            return paymentMethod.ProcessPayment(processPaymentRequest);
        }

        /// <summary>
        ///Post提交付款请求（由需要重定向到第三方网址的付款网关使用）
        /// </summary>
        /// <param name="postProcessPaymentRequest">订单处理所需的付款信息</param>
        public virtual void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            //already paid or order.OrderTotal == decimal.Zero
            if (postProcessPaymentRequest.Order.PaymentStatus == PaymentStatus.Paid)
                return;

            var paymentMethod = LoadPaymentMethodBySystemName(postProcessPaymentRequest.Order.PaymentMethodSystemName);
            if (paymentMethod == null)
                throw new NopException("Payment method couldn't be loaded");
            paymentMethod.PostProcessPayment(postProcessPaymentRequest);
        }

        /// <summary>
        /// 客户在下订单但未完成后是否可以完成付款（用于重定向付款方式）
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Result</returns>
        public virtual bool CanRePostProcessPayment(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (!_paymentSettings.AllowRePostingPayments)
                return false;

            var paymentMethod = LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
            if (paymentMethod == null)
                return false; //Payment method couldn't be loaded (for example, was uninstalled)

            if (paymentMethod.PaymentMethodType != PaymentMethodType.Redirection)
                return false;   //this option is available only for redirection payment methods

            if (order.Deleted)
                return false;  //do not allow for deleted orders

            if (order.OrderStatus == OrderStatus.Cancelled)
                return false;  //do not allow for cancelled orders

            if (order.PaymentStatus != PaymentStatus.Pending)
                return false;  //payment status should be Pending

            return paymentMethod.CanRePostProcessPayment(order);
        }



        /// <summary>
        ///手续费
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>Additional handling fee</returns>
        public virtual decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart, string paymentMethodSystemName)
        {
            if (String.IsNullOrEmpty(paymentMethodSystemName))
                return decimal.Zero;

            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
            if (paymentMethod == null)
                return decimal.Zero;

            decimal result = paymentMethod.GetAdditionalHandlingFee(cart);
            if (result < decimal.Zero)
                result = decimal.Zero;
            if (_shoppingCartSettings.RoundPricesDuringCalculation)
            {
                result = RoundingHelper.RoundPrice(result);
            }
            return result;
        }



        /// <summary>
        /// Gets a value indicating whether capture is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether capture is supported</returns>
        public virtual bool SupportCapture(string paymentMethodSystemName)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportCapture;
        }

        /// <summary>
        /// 获取付款结果
        /// </summary>
        /// <param name="capturePaymentRequest">Capture payment request</param>
        /// <returns>Capture payment result</returns>
        public virtual CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(capturePaymentRequest.Order.PaymentMethodSystemName);
            if (paymentMethod == null)
                throw new NopException("Payment method couldn't be loaded");
            return paymentMethod.Capture(capturePaymentRequest);
        }



        /// <summary>
        /// 是否支持部分退款
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether partial refund is supported</returns>
        public virtual bool SupportPartiallyRefund(string paymentMethodSystemName)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportPartiallyRefund;
        }

        /// <summary>
        /// 是否支持退款
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether refund is supported</returns>
        public virtual bool SupportRefund(string paymentMethodSystemName)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportRefund;
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public virtual RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(refundPaymentRequest.Order.PaymentMethodSystemName);
            if (paymentMethod == null)
                throw new NopException("Payment method couldn't be loaded");
            return paymentMethod.Refund(refundPaymentRequest);
        }
        


        /// <summary>
        /// Gets a value indicating whether void is supported by payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A value indicating whether void is supported</returns>
        public virtual bool SupportVoid(string paymentMethodSystemName)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
            if (paymentMethod == null)
                return false;
            return paymentMethod.SupportVoid;
        }

        /// <summary>
        /// Voids a payment
        /// </summary>
        /// <param name="voidPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public virtual VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(voidPaymentRequest.Order.PaymentMethodSystemName);
            if (paymentMethod == null)
                throw new NopException("Payment method couldn't be loaded");
            return paymentMethod.Void(voidPaymentRequest);
        }



        /// <summary>
        /// Gets a recurring payment type of payment method
        /// </summary>
        /// <param name="paymentMethodSystemName">Payment method system name</param>
        /// <returns>A recurring payment type of payment method</returns>
        public virtual RecurringPaymentType GetRecurringPaymentType(string paymentMethodSystemName)
        {
            var paymentMethod = LoadPaymentMethodBySystemName(paymentMethodSystemName);
            if (paymentMethod == null)
                return RecurringPaymentType.NotSupported;
            return paymentMethod.RecurringPaymentType;
        }

        /// <summary>
        /// 处理定期付款
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>Process payment result</returns>
        public virtual ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            if (processPaymentRequest.OrderTotal == decimal.Zero)
            {
                var result = new ProcessPaymentResult
                {
                    NewPaymentStatus = PaymentStatus.Paid
                };
                return result;
            }

            var paymentMethod = LoadPaymentMethodBySystemName(processPaymentRequest.PaymentMethodSystemName);
            if (paymentMethod == null)
                throw new NopException("Payment method couldn't be loaded");
            return paymentMethod.ProcessRecurringPayment(processPaymentRequest);
        }

        /// <summary>
        /// 取消定期付款
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public virtual CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            if (cancelPaymentRequest.Order.OrderTotal == decimal.Zero)
                return new CancelRecurringPaymentResult();

            var paymentMethod = LoadPaymentMethodBySystemName(cancelPaymentRequest.Order.PaymentMethodSystemName);
            if (paymentMethod == null)
                throw new NopException("Payment method couldn't be loaded");
            return paymentMethod.CancelRecurringPayment(cancelPaymentRequest);
        }


        /// <summary>
        /// Gets masked credit card number
        /// </summary>
        /// <param name="creditCardNumber">Credit card number</param>
        /// <returns>Masked credit card number</returns>
        public virtual string GetMaskedCreditCardNumber(string creditCardNumber)
        {
            if (String.IsNullOrEmpty(creditCardNumber))
                return string.Empty;

            if (creditCardNumber.Length <= 4)
                return creditCardNumber;

            string last4 = creditCardNumber.Substring(creditCardNumber.Length - 4, 4);
            string maskedChars = string.Empty;
            for (int i = 0; i < creditCardNumber.Length - 4; i++)
            {
                maskedChars += "*";
            }
            return maskedChars + last4;
        }
        
        #endregion
    }
}

using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Payments;
using System.Web.Mvc;
using Nop.Plugin.Payments.Alipay.Models;
using Nop.Services.Configuration;
using Nop.Services.Logging;
using Nop.Services.Localization;
using Nop.Core;
using Nop.Core.Domain.Payments;
using System.Collections.Specialized;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.Alipay.Controllers
{
    public class PaymentAlipayController : BasePaymentController
    {
        #region 常量
        //支付宝消息验证地址
        private string Https_veryfy_url = "https://mapi.alipay.com/gateway.do?service=notify_verify&";
        #endregion
        #region 声明实例
        private readonly ISettingService _settingService;
        private readonly AlipayPaymentSettings _aliPayPaymentSettings;
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        private readonly ILocalizationService _localizationService;
        private readonly IPaymentService _paymentService;
        private readonly PaymentSettings _paymentSettings;
        private readonly IOrderProcessingService _orderProcessingService;
        #endregion
        public PaymentAlipayController(ISettingService _settingService,
            AlipayPaymentSettings aliPaypaymentSettings, ILogger _logger,
            ILocalizationService _localizationService, IPaymentService _paymentService,
            PaymentSettings _paymentSettings, IOrderService _orderService,
            IOrderProcessingService _orderProcessingService)
        {
            this._settingService = _settingService;
            this._aliPayPaymentSettings = aliPaypaymentSettings;
            this._logger = _logger;
            this._localizationService = _localizationService;
            this._paymentService = _paymentService;
            this._paymentSettings = _paymentSettings;
            this._orderService = _orderService;
            this._orderProcessingService = _orderProcessingService;
        }
        #region 方法
        /// <summary>
        /// 配置支付信息
        /// </summary>
        /// <returns></returns>
        [AdminAuthorize]
        [ChildActionOnly]

        public ActionResult Configure()
        {
            var configModel = new ConfigurationModel
            {
                Key = _aliPayPaymentSettings.Key,
                Partner = _aliPayPaymentSettings.Partner
            };
            return View("~/Plugins/Payments.Alipay/Views/PaymentAlipay/Configure.cshtml", configModel);
        }
        /// <summary>
        /// 配置支付信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();
            _aliPayPaymentSettings.Key = model.Key;
            _aliPayPaymentSettings.Partner = model.Partner;
            _settingService.SaveSetting(_aliPayPaymentSettings);
            _settingService.ClearCache();
            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
        /// <summary>
        /// 跳转提示
        /// </summary>
        /// <param name="widgetZone"></param>
        /// <param name="additionalData"></param>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult PaymentInfo(string widgetZone, object additionalData = null)
        {
            return View("~/Plugins/Payments.Alipay/Views/PaymentAlipay/PaymentInfo.cshtml", null);
        }
        public override ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            var paymentInfo = new ProcessPaymentRequest();

            return paymentInfo;
        }

        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            var warnings = new List<string>();

            return warnings;
        }
        /// <summary>
        /// 功能：页面跳转同步通知页面
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Return()
        {
            var processor = _paymentService.LoadPaymentMethodBySystemName("Payments.AliPay") as AlipayPaymentProcessor;
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        /// <summary>
        /// 功能：服务器异步通知页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Notify()
        {
            string result = "";
            var processor = _paymentService.LoadPaymentMethodBySystemName("Payments.AliPay") as AlipayPaymentProcessor;
            var partner = _aliPayPaymentSettings.Partner;
            var key = _aliPayPaymentSettings.Key;
            if (processor == null
                || !processor.IsPaymentMethodActive(_paymentSettings)
                || !processor.PluginDescriptor.Installed)
                throw new NopException("Alipay支付模块未能加载");
            var sign = Request.Form["sign"];///签名
            SortedDictionary<string, string> sPara = GetRequestPost();
            var mySign = processor.Verify(sPara.ToString(), sign, key);//验证签名
            if (mySign)//验证成功
            {
                //交易状态
                string trade_status = sPara["trade_status"];
                //支付宝交易号
                string trade_no = Request.Form["trade_no"];
                switch (trade_status)
                {
                    case "TRADE_FINISHED":
                    case "TRADE_SUCCESS":
                        {
                            //商户订单号
                            var orderNo = Request.Form["out_trade_no"];
                            var strPrice = Request.Form["total_fee"];
                            int orderId;
                            if (int.TryParse(orderNo, out orderId))
                            {
                                var order = _orderService.GetOrderById(orderId);
                                ///订单金额是否正确
                                if (order.OrderTotal != decimal.Parse(strPrice))
                                {
                                    result = "订单金额不正确！";
                                    Content("订单金额不正确");
                                }
                                else
                                {
                                    if (order != null && _orderProcessingService.CanMarkOrderAsPaid(order))
                                    {
                                        _orderProcessingService.MarkOrderAsPaid(order);
                                    }
                                }
                            }
                            result = "交易成功！";
                        }
                        break;
                    default:
                        result = "交易失败!";
                        break;
                }
                Response.Write(result);
            }
            else//验证失败
            {
                result = "验证失败!";
                var logStr = string.Format(result);
                _logger.Error(logStr);
            }
            return Content("");
        }


        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
        #endregion
    }
}

using Nop.Core.Plugins;
using Nop.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Orders;
using Nop.Web.Framework;
using Nop.Core;
using Nop.Services.Configuration;
using System.Globalization;
using Nop.Core.Domain.Payments;
using System.Web.Routing;
using Nop.Plugin.Payments.Alipay.Controllers;

namespace Nop.Plugin.Payments.Alipay
{
    public class AlipayPaymentProcessor : BasePlugin, IPaymentMethod
    {
        #region 声明实例
        private readonly IStoreContext _storeContext;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly AlipayPaymentSettings _aliPayPaymentSettings;
        #endregion
        #region 构造函数
        public AlipayPaymentProcessor(
            ISettingService settingService,
            IWebHelper webHelper,
            IStoreContext storeContext,
            AlipayPaymentSettings aliPayPaymentSettings)
        {
            this._settingService = settingService;
            this._webHelper = webHelper;
            this._storeContext = storeContext;
            this._aliPayPaymentSettings = aliPayPaymentSettings;
        }
        #endregion
        /// <summary>
        ///获取付款方式类型
        /// </summary>
        public PaymentMethodType PaymentMethodType
        {
            get
            {
                return PaymentMethodType.Redirection;
            }
        }
        /// <summary>
        /// Post提交付款请求（由需要重定向到第三方网址的付款网关使用）
        /// </summary>
        /// <param name="postProcessPaymentRequest">Payment info required for an order processing</param>
        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            ////////////////////////////////////////////请求参数////////////////////////////////////////////
            //商户订单号，商户网站订单系统中唯一订单号，必填
            var out_trade_no = postProcessPaymentRequest.Order.Id.ToString();
            //订单名称，必填
            var subject = _storeContext.CurrentStore.Name.ToString();
            var body = "Order from " + _storeContext.CurrentStore.Name;
            //付款金额，必填
            var total_fee = postProcessPaymentRequest.Order.OrderTotal.ToString("0.00", CultureInfo.InvariantCulture);


             //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", _aliPayPaymentSettings.service);
            sParaTemp.Add("partner", _aliPayPaymentSettings.partner);
            sParaTemp.Add("seller_id", _aliPayPaymentSettings.seller_id);
            sParaTemp.Add("_input_charset", _aliPayPaymentSettings.input_charset.ToLower());
            sParaTemp.Add("payment_type", _aliPayPaymentSettings.payment_type);
            sParaTemp.Add("notify_url", _aliPayPaymentSettings.notify_url);
            sParaTemp.Add("return_url", _aliPayPaymentSettings.return_url);
            sParaTemp.Add("anti_phishing_key", _aliPayPaymentSettings.anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", _aliPayPaymentSettings.exter_invoke_ip);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            //其他业务参数根据在线开发文档，添加参数.文档地址:https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.O9yorI&treeId=62&articleId=103740&docType=1
            //如sParaTemp.Add("参数名","参数值");

            //建立请求
            string sHtmlText =BuildRequest(sParaTemp, "get", "确认");
            //Response.Write(sHtmlText);
            //_AppDomain.re
            //var post = new RemotePost
            //{
            //    FormName = "alipaysubmit",
            //    Url = "https://www.alipay.com/cooperate/gateway.do?_input_charset=utf-8",
            //    Method = "POST"
            //};
            //post.Add("sign", sParaTemp);
            //post.Post();
        }



        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);

            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + "https://mapi.alipay.com/gateway.do?" + "_input_charset=" + "utf-8" + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            //sPara = Core.FilterPara(sParaTemp);

            //获得签名结果
           // mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", "");

            return sPara;
        }











        /// <summary>
        /// 获取定期付款方式的付款方式
        /// </summary>
        public RecurringPaymentType RecurringPaymentType
        {
            get
            {
                return RecurringPaymentType.NotSupported;
            }
        }
        /// <summary>
        /// 指示是否要显示此插件的付款信息页面
        /// </summary>
        public bool SkipPaymentInfo
        {
            get
            {
                return false;
            }
        }

        public bool SupportCapture
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 是否支持部分退款
        /// </summary>
        public bool SupportPartiallyRefund
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 是否支持退款
        /// </summary>
        public bool SupportRefund
        {
            get
            {
                return false;
            }
        }

        public bool SupportVoid
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 取消定期付款
        /// </summary>
        /// <param name="cancelPaymentRequest">Request</param>
        /// <returns>Result</returns>
        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            var result = new CancelRecurringPaymentResult();

            result.AddError("Recurring payment not supported");

            return result;
        }
        /// <summary>
        /// 客户在下订单但未完成后是否可以完成付款（用于重定向付款方式）
        /// </summary>
        /// <param name="order">订单</param>
        /// <returns>Result</returns>
        public bool CanRePostProcessPayment(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            //AliPay is the redirection payment method
            //It also validates whether order is also paid (after redirection) so customers will not be able to pay twice

            //payment status should be Pending
            if (order.PaymentStatus != PaymentStatus.Pending)
                return false;

            //let's ensure that at least 1 minute passed after order is placed
            return !((DateTime.UtcNow - order.CreatedOnUtc).TotalMinutes < 1);
        }
        /// <summary>
        /// 获取付款结果
        /// </summary>
        /// <param name="capturePaymentRequest">付款请求</param>
        /// <returns>付款结果</returns>
        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            var result = new CapturePaymentResult();

            result.AddError("Capture method not supported");

            return result;
        }
        /// <summary>
        ///手续费
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>Additional handling fee</returns>
        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            return 12;
        }
        /// <summary>
        ///获取供应商配置的路由
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "PaymentAlipay";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Payments.Alipay.Controllers" }, { "area", null } };
        }

        public Type GetControllerType()
        {
            return typeof(PaymentAlipayController);
        }
        /// <summary>
        /// 获取付款信息的路由
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetPaymentInfoRoute(out string actionName, out string controllerName, out System.Web.Routing.RouteValueDictionary routeValues)
        {
            actionName = "PaymentInfo";
            controllerName = "PaymentAlipay";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.Payments.Alipay.Controllers" }, { "area", null } };
        }
        /// <summary>
        /// 在结帐期间是否应隐藏付款方式
        /// </summary>
        /// <param name="cart">Shoping cart</param>
        /// <returns>true - hide; false - display.</returns>
        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            return false;
        }

        /// <summary>
        /// 处理付款
        /// </summary>
        /// <param name="processPaymentRequest">Payment info required for an order processing</param>
        /// <returns>Process payment result</returns>

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.NewPaymentStatus = PaymentStatus.Pending;
            return result;
        }
        /// <summary>
        /// 处理定期付款
        /// </summary>
        /// <param name="processPaymentRequest">订单处理所需的付款信息</param>
        /// <returns>处理结果</returns>

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.AddError("Recurring payment not supported");
            return result;
        }
        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="refundPaymentRequest">退款请求</param>
        /// <returns>Result</returns>
        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            var result = new RefundPaymentResult();
            result.AddError("Refund method not supported");
            return result;
        }

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            var result = new VoidPaymentResult();
            result.AddError("Void method not supported");
            return result;
        }



        /// <summary>
        /// 安装插件接口
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            var settings = new AlipayPaymentSettings();
            //{
            //    UseSandbox = true,
            //    BusinessEmail = "test@test.com",
            //    PdtToken = "Your PDT token here...",
            //    PdtValidateOrderTotal = true,
            //    EnableIpn = true,
            //    AddressOverride = true,
            //};
            _settingService.SaveSetting(settings);
            base.Install();
        }

        /// <summary>
        /// 卸载插件接口
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            base.Uninstall();
        }
    }
}

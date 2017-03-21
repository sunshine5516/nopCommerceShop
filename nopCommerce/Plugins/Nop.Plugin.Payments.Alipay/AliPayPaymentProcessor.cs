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
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace Nop.Plugin.Payments.Alipay
{
    public class AlipayPaymentProcessor : BasePlugin, IPaymentMethod
    {
        #region 常量
        private const string InputCharset = "utf-8";
        private string alipayNotifyUrl = "https://mapi.alipay.com/gateway.do?service=notify_verify&";//支付宝消息验证地址
        #endregion
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
        #region 方法
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
            var notifyUrl = _webHelper.GetStoreLocation(false) + "Plugins/PaymentAliPay/Notify";
            var returnUrl = _webHelper.GetStoreLocation(false) + "Plugins/PaymentAliPay/Return";

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", _aliPayPaymentSettings.Service);
            sParaTemp.Add("partner", _aliPayPaymentSettings.Partner);
            sParaTemp.Add("seller_id", _aliPayPaymentSettings.Partner);
            sParaTemp.Add("_input_charset", InputCharset.ToLower());
            sParaTemp.Add("payment_type", "1");
            //sParaTemp.Add("notify_url", _aliPayPaymentSettings.notify_url);
            //sParaTemp.Add("return_url", _aliPayPaymentSettings.return_url);
            sParaTemp.Add("notify_url", notifyUrl);
            sParaTemp.Add("return_url", returnUrl);
            sParaTemp.Add("anti_phishing_key", "");
            sParaTemp.Add("exter_invoke_ip", "");
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            //其他业务参数根据在线开发文档，添加参数.文档地址:https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.O9yorI&treeId=62&articleId=103740&docType=1
            //如sParaTemp.Add("参数名","参数值");

            //建立请求
            string sHtmlText = BuildRequest(sParaTemp, "get", "确认");
            //Response.Write(sHtmlText);
            //_AppDomain.re
            var post = new RemotePost
            {
                FormName = "alipaysubmit",
                Url = "https://www.alipay.com/cooperate/gateway.do?_input_charset=utf-8",
                Method = "POST"
            };
            post.Add("sign", sHtmlText);
            post.Post();
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
        #endregion
        #region 网站验证



        /// <summary>
        /// 验证消息是否是支付宝发出的和发消息
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notifuId">通知验证ID</param>
        /// <param name="sign">支付宝生成的签名</param>
        /// <returns></returns>
        public bool Verify(SortedDictionary<string, string> inputPara, string notifyId, string key, string sign)
        {
            bool isSign = GetSignVerify(inputPara, sign, key);
            string responseTxt = "false";//获取是否是支付宝服务器发来的请求的验证结果
            if (notifyId != null && notifyId != "")
            {
                responseTxt = GetResponseTxt(notifyId);
            }
            //判断responsetTxt是否为true，isSign是否为true
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            if (responseTxt == "true" && isSign)
            {
                return true;
            }
            else//验证失败
            {
                return false;
            }
        }
        /// <summary>
        /// 获取返回时签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        private bool GetSignVerify(SortedDictionary<string, string> inputPara, string sign, string key)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //过滤空值、sign与sign_type参数
            sPara = FilterPara(inputPara);
            //获取待签名字符串
            string preSignStr = CreateLinkString(sPara);
            ///获得签名验证结果
            bool isSign = false;
            if (sign != null && sign != "")
            {
                //switch (_sign_type)
                //{
                //    case "MD5":
                isSign = Verify(preSignStr, sign, key);
                // break;
                //    default:
                //        break;
                //}
            }
            return isSign;
        }

        /// <summary>
        /// 获取远程服务器ATN结果
        /// </summary>
        /// <param name="strUrl">指定URL路径地址</param>
        /// <param name="timeout">超时时间设置</param>
        /// <returns>服务器ATN结果</returns>
        public string GetHttp(string strUrl, int timeout)
        {
            string strResult;
            try
            {
                HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                myReq.Timeout = timeout;
                HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
                Stream myStream = HttpWResp.GetResponseStream();
                StreamReader sr = new StreamReader(myStream, Encoding.Default);
                StringBuilder strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }

                strResult = strBuilder.ToString();
            }
            catch (Exception exp)
            {
                strResult = "错误：" + exp.Message;
            }

            return strResult;
        }
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="_input_charset">编码格式</param>
        /// <returns>签名结果</returns>
        public string Sign(string prestr, string key, string _input_charset)
        {
            StringBuilder sb = new StringBuilder(32);
            prestr = prestr + key;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(prestr));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="sign">签名结果</param>
        /// <param name="key">密钥</param>
        /// <param name="_input_charset">编码格式</param>
        /// <returns>验证结果</returns>
        public bool Verify(string prestr, string sign, string key)
        {
            string mysign = Sign(prestr, key, InputCharset);
            if (mysign == sign)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取是否是支付宝服务器发来的请求结果
        /// </summary>
        /// <param name="notifyId"></param>
        /// <returns></returns>
        private string GetResponseTxt(string notifyId)
        {
            string verifyUrl= alipayNotifyUrl+"partner"+ _aliPayPaymentSettings.Partner + "&notify_id=" + notifyId;
            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            string responseTxt =GetHttp(verifyUrl, 120000);
            return responseTxt;
        }

      
        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="sArray">需要拼接的数组</param>
        /// <returns>拼接完成以后的字符串</returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            //去掉最後一個&字符
            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }
        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" && temp.Value != "" && temp.Value != null)
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }

            return dicArray;
        }
        #endregion

        #region 插件管理
        /// <summary>
        /// 安装插件接口
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            var settings = new AlipayPaymentSettings()
            {
                Partner = "2088702872203913",
                Key = "q73a1ciq15pep6ve1g6ububrmquj7e6g",
            };
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
        #endregion
    }
}

using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.Plugin.Payments.Alipay
{
    public class AlipayPaymentSettings : ISettings
    {
        //合作身份者ID，签约账号，以2088开头由16位纯数字组成的字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        public string Partner { get; set; }
        //MD5密钥，安全检验码，由数字和字母组成的32位字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        public string Key { get; set; }
        // 签名方式
        public string Sign_type { get { return "MD5"; } }
        // 调用的接口名，无需修改
        public string Service { get { return "create_direct_pay_by_user"; } }



        //#region Alipay配置基本信息
        ////↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        //// 合作身份者ID，签约账号，以2088开头由16位纯数字组成的字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        //public string partner = "2088702872203913";

        //// 收款支付宝账号，以2088开头由16位纯数字组成的字符串，一般情况下收款账号就是签约账号
        //public string seller_id = "2088702872203913";

        //// MD5密钥，安全检验码，由数字和字母组成的32位字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        //public string key = "q73a1ciq15pep6ve1g6ububrmquj7e6g";

        ////// 服务器异步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数,必须外网可以正常访问
        //public string notify_url = "http://115.159.191.144/Alipay/notify_url.aspx";

        ////// 页面跳转同步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        //public  string return_url = "http://115.159.191.144/Alipay/return_url.aspx";

        //// 签名方式
        //public string sign_type = "MD5";

        //// 调试用，创建TXT日志文件夹路径，见AlipayCore.cs类中的LogResult(string sWord)打印方法。
        //public string log_path = HttpRuntime.AppDomainAppPath.ToString() + "log\\";

        //// 字符编码格式 目前支持 gbk 或 utf-8
        //public string input_charset = "utf-8";

        //// 支付类型 ，无需修改
        //public string payment_type = "1";

        //// 调用的接口名，无需修改
        //public string service = "create_direct_pay_by_user";
        ////public static string service = "trade_create_by_buyer";

        ////↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑


        ////↓↓↓↓↓↓↓↓↓↓请在这里配置防钓鱼信息，如果没开通防钓鱼功能，请忽视不要填写 ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

        ////防钓鱼时间戳  若要使用请调用类文件submit中的Query_timestamp函数
        //public string anti_phishing_key = "";

        ////客户端的IP地址 非局域网的外网IP地址，如：221.0.0.1
        //public string exter_invoke_ip = "";

        ////↑↑↑↑↑↑↑↑↑↑请在这里配置防钓鱼信息，如果没开通防钓鱼功能，请忽视不要填写 ↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
        //#endregion

    }
}

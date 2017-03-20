using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Payments;
using System.Web.Mvc;

namespace Nop.Plugin.Payments.Alipay.Controllers
{
    public class PaymentAlipayController : BasePaymentController
    {
        public PaymentAlipayController() { }
        [AdminAuthorize]
        [ChildActionOnly]

        public ActionResult Configure()
        {
            return View("~/Plugins/Nop.Plugin.Payments.Alipay/Views/PaymentsAlipay/Configure.cshtml");
        }
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            return View("~/Plugins/Nop.Plugin.Payments.Alipay/Views/PaymentsAlipay/PublicInfo.cshtml", null);
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
    }
}

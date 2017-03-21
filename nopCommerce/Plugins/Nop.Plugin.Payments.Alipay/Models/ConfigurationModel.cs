using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.Alipay.Models
{
    public class ConfigurationModel:BaseNopModel
    {
        public string Partner { get; set; }
        public string Key { get; set; }
    }
}

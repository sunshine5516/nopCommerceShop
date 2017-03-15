using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Admin.Models.Nop_demo
{
    public partial class Nop_demoMoel : BaseNopEntityModel
    {
        [NopResourceDisplayName("描述文件")]
        public string describe { get; set; }
        [NopResourceDisplayName("是否删除")]
        public bool Deleted { get; set; }
        [NopResourceDisplayName("是否激活状态")]
        public bool Active { get; set; }
    }
}
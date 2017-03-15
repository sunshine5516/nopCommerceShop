using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain
{
    /// <summary>
    /// 测试DEMO
    /// </summary>
    public partial class Nop_Demo:BaseEntity
    {
        public string describe { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
    }
}

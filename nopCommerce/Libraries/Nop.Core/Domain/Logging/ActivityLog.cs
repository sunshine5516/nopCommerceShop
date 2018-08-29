using System;
using Nop.Core.Domain.Customers;

namespace Nop.Core.Domain.Logging
{
    /// <summary>
    /// 活动日志记录
    /// </summary>
    public partial class ActivityLog : BaseEntity
    {
        /// <summary>
        /// 活动日志类型ID
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 活动注释
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        public virtual ActivityLogType ActivityLogType { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        public virtual string IpAddress { get; set; }
    }
}

namespace Nop.Core.Domain.Logging
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public partial class ActivityLogType : BaseEntity
    {
        /// <summary>
        /// 系统关键字
        /// </summary>
        public string SystemKeyword { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Payments
{
    /// <summary>
    /// 取消定期付款结果
    /// </summary>
    public partial class CancelRecurringPaymentResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CancelRecurringPaymentResult() 
        {
            this.Errors = new List<string>();
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success
        {
            get { return (!this.Errors.Any()); }
        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error) 
        {
            this.Errors.Add(error);
        }

        /// <summary>
        /// Errors
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}

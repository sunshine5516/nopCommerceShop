using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Customers 
{
    /// <summary>
    /// 注册结果
    /// </summary>
    public class CustomerRegistrationResult 
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public CustomerRegistrationResult() 
        {
            this.Errors = new List<string>();
        }

        /// <summary>
        /// 是否注册成功
        /// </summary>
        public bool Success 
        {
            get { return !this.Errors.Any(); }
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
        /// 错误信息
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}

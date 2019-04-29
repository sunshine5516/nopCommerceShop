//Contributor:  Nicholas Mayne

using System.Collections.Generic;
using System.Linq;

namespace Nop.Services.Authentication.External
{
    /// <summary>
    /// 授权结果
    /// </summary>
    public partial class AuthorizationResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="status"></param>
        public AuthorizationResult(OpenAuthenticationStatus status)
        {
            this.Errors = new List<string>();
            Status = status;
        }

        /// <summary>
        /// Add error
        /// </summary>
        /// <param name="error">Error</param>
        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success
        {
            get { return !this.Errors.Any(); }
        }

        /// <summary>
        /// Status
        /// </summary>
        public OpenAuthenticationStatus Status { get; private set; }

        /// <summary>
        /// Errors
        /// </summary>
        public IList<string> Errors { get; set; }
    }
}
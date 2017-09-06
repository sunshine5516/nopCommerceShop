using System.Collections.Generic;
using Nop.Core.Domain.Customers;

namespace Nop.Core.Domain.Security
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    public partial class PermissionRecord : BaseEntity
    {
        private ICollection<CustomerRole> _customerRoles;

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 权限类别
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<CustomerRole> CustomerRoles
        {
            get { return _customerRoles ?? (_customerRoles = new List<CustomerRole>()); }
            protected set { _customerRoles = value; }
        }   
    }
}

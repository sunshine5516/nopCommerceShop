using System;
using System.Collections.Generic;
using Nop.Core.Domain.Catalog;

namespace Nop.Core.Domain.Discounts
{
    /// <summary>
    /// Represents a discount
    /// </summary>
    public partial class Discount : BaseEntity
    {
        private ICollection<DiscountRequirement> _discountRequirements;
        private ICollection<Category> _appliedToCategories;
        private ICollection<Manufacturer> _appliedToManufacturers;
        private ICollection<Product> _appliedToProducts;

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public int DiscountTypeId { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether to use percentage
        /// </summary>
        public bool UsePercentage { get; set; }

        /// <summary>
        /// 折扣百分比
        /// </summary>
        public decimal DiscountPercentage { get; set; }

        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 最大折扣金额（与“UsePercentage”一起使用）
        /// </summary>
        public decimal? MaximumDiscountAmount { get; set; }

        /// <summary>
        /// 折扣开始日期和时间
        /// </summary>
        public DateTime? StartDateUtc { get; set; }

        /// <summary>
        /// 折扣结束日期和时间
        /// </summary>
        public DateTime? EndDateUtc { get; set; }

        /// <summary>
        /// 折扣是否需要优惠券代码
        /// </summary>
        public bool RequiresCouponCode { get; set; }

        /// <summary>
        /// 优惠券代码
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 折扣是否可以与其他折扣同时使用（具有相同的折扣类型）
        /// </summary>
        public bool IsCumulative { get; set; }

        /// <summary>
        /// 折扣限制标识符
        /// </summary>
        public int DiscountLimitationId { get; set; }

        /// <summary>
        /// 获取或设置折扣限制时间（限制设置为“仅N次”或“每客户N次”时使用）
        /// </summary>
        public int LimitationTimes { get; set; }

        /// <summary>
        /// 可折扣的最大产品数量
        /// </summary>
        public int? MaximumDiscountedQuantity { get; set; }

        /// <summary>
        /// 是否应该应用于所有子类别或所选择的子类别
        /// </summary>
        public bool AppliedToSubCategories { get; set; }

        /// <summary>
        /// 折扣类型
        /// </summary>
        public DiscountType DiscountType
        {
            get
            {
                return (DiscountType)this.DiscountTypeId;
            }
            set
            {
                this.DiscountTypeId = (int)value;
            }
        }

        /// <summary>
        /// 折扣限制
        /// </summary>
        public DiscountLimitationType DiscountLimitation
        {
            get
            {
                return (DiscountLimitationType)this.DiscountLimitationId;
            }
            set
            {
                this.DiscountLimitationId = (int)value;
            }
        }

        /// <summary>
        /// 折扣要求
        /// </summary>
        public virtual ICollection<DiscountRequirement> DiscountRequirements
        {
            get { return _discountRequirements ?? (_discountRequirements = new List<DiscountRequirement>()); }
            protected set { _discountRequirements = value; }
        }
        /// <summary>
        /// 获取或设置类别
        /// </summary>
        public virtual ICollection<Category> AppliedToCategories
        {
            get { return _appliedToCategories ?? (_appliedToCategories = new List<Category>()); }
            protected set { _appliedToCategories = value; }
        }
        /// <summary>
        /// 获取或设置类别
        /// </summary>
        public virtual ICollection<Manufacturer> AppliedToManufacturers
        {
            get { return _appliedToManufacturers ?? (_appliedToManufacturers = new List<Manufacturer>()); }
            protected set { _appliedToManufacturers = value; }
        }
        /// <summary>
        /// 获取或设置产品
        /// </summary>
        public virtual ICollection<Product> AppliedToProducts
        {
            get { return _appliedToProducts ?? (_appliedToProducts = new List<Product>()); }
            protected set { _appliedToProducts = value; }
        }
    }
}

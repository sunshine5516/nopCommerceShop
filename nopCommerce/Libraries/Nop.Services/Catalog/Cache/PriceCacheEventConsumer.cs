using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Configuration;
using Nop.Core.Domain.Discounts;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Core.Infrastructure;
using Nop.Services.Events;

namespace Nop.Services.Catalog.Cache
{
    /// <summary>
    /// 价格缓存事件消费者（用于缓存价格）
    /// </summary>
    public partial class PriceCacheEventConsumer: 
        //settings
        IConsumer<EntityUpdated<Setting>>,
        //商品属性，注册事件
        IConsumer<EntityInserted<Category>>,
        IConsumer<EntityUpdated<Category>>,
        IConsumer<EntityDeleted<Category>>,
        //制造商
        IConsumer<EntityInserted<Manufacturer>>,
        IConsumer<EntityUpdated<Manufacturer>>,
        IConsumer<EntityDeleted<Manufacturer>>,
        //折扣，注册事件
        IConsumer<EntityInserted<Discount>>,
        IConsumer<EntityUpdated<Discount>>,
        IConsumer<EntityDeleted<Discount>>,
        //商品属性，注册事件
        IConsumer<EntityInserted<ProductCategory>>,
        IConsumer<EntityUpdated<ProductCategory>>,
        IConsumer<EntityDeleted<ProductCategory>>,
        //商品制造商，注册事件
        IConsumer<EntityInserted<ProductManufacturer>>,
        IConsumer<EntityUpdated<ProductManufacturer>>,
        IConsumer<EntityDeleted<ProductManufacturer>>,
        //商品
        IConsumer<EntityInserted<Product>>,
        IConsumer<EntityUpdated<Product>>,
        IConsumer<EntityDeleted<Product>>,
        //tier prices
        IConsumer<EntityInserted<TierPrice>>,
        IConsumer<EntityUpdated<TierPrice>>,
        IConsumer<EntityDeleted<TierPrice>>,
        //订单
        IConsumer<EntityInserted<Order>>,
        IConsumer<EntityUpdated<Order>>,
        IConsumer<EntityDeleted<Order>>
    {
        /// <summary>
        /// 商品价格的Key
        /// </summary>
        /// <remarks>
        /// {0} : product id
        /// {1} : overridden product price
        /// {2} : additional charge
        /// {3} : include discounts (true, false)
        /// {4} : quantity
        /// {5} : roles of the current user
        /// {6} : current store ID
        /// </remarks>
        public const string PRODUCT_PRICE_MODEL_KEY = "Nop.totals.productprice-{0}-{1}-{2}-{3}-{4}-{5}-{6}";
        public const string PRODUCT_PRICE_PATTERN_KEY = "Nop.totals.productprice";

        /// <summary>
        /// 折扣的类别ID的Key
        /// </summary>
        /// <remarks>
        /// {0} : discount id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_CATEGORY_IDS_MODEL_KEY = "Nop.totals.discount.categoryids-{0}-{1}-{2}";
        public const string DISCOUNT_CATEGORY_IDS_PATTERN_KEY = "Nop.totals.discount.categoryids";

        /// <summary>
        ///折扣制造商ID的Key
        /// </summary>
        /// <remarks>
        /// {0} : discount id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_MANUFACTURER_IDS_MODEL_KEY = "Nop.totals.discount.manufacturerids-{0}-{1}-{2}";
        public const string DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY = "Nop.totals.discount.manufacturerids";

        /// <summary>
        /// 产品的类别ID的Key
        /// </summary>
        /// <remarks>
        /// {0} : product id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_PRODUCT_CATEGORY_IDS_MODEL_KEY = "Nop.totals.product.categoryids-{0}-{1}-{2}";
        public const string DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY = "Nop.totals.product.categoryids";

        /// <summary>
        /// 产品的制造商ID的Key
        /// </summary>
        /// <remarks>
        /// {0} : product id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// </remarks>
        public const string DISCOUNT_PRODUCT_MANUFACTURER_IDS_MODEL_KEY = "Nop.totals.product.manufacturerids-{0}-{1}-{2}";
        public const string DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY = "Nop.totals.product.manufacturerids";

        private readonly ICacheManager _cacheManager;

        public PriceCacheEventConsumer()
        {
            //TODO inject static cache manager using constructor
            this._cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");
        }

        //settings
        public void HandleEvent(EntityUpdated<Setting> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //categories
        public void HandleEvent(EntityInserted<Category> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<Category> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<Category> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }

        //manufacturers
        public void HandleEvent(EntityInserted<Manufacturer> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<Manufacturer> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<Manufacturer> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //discounts
        public void HandleEvent(EntityInserted<Discount> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<Discount> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<Discount> eventMessage)
        {
            _cacheManager.RemoveByPattern(DISCOUNT_CATEGORY_IDS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //product categories
        public void HandleEvent(EntityInserted<ProductCategory> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<ProductCategory> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<ProductCategory> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_CATEGORY_IDS_PATTERN_KEY);
        }

        //product manufacturers
        public void HandleEvent(EntityInserted<ProductManufacturer> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<ProductManufacturer> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<ProductManufacturer> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
            _cacheManager.RemoveByPattern(DISCOUNT_PRODUCT_MANUFACTURER_IDS_PATTERN_KEY);
        }

        //products
        public void HandleEvent(EntityInserted<Product> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<Product> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<Product> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }

        //tier prices
        public void HandleEvent(EntityInserted<TierPrice> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<TierPrice> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<TierPrice> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }

        //orders
        public void HandleEvent(EntityInserted<Order> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public void HandleEvent(EntityUpdated<Order> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
        public void HandleEvent(EntityDeleted<Order> eventMessage)
        {
            _cacheManager.RemoveByPattern(PRODUCT_PRICE_PATTERN_KEY);
        }
    }
}

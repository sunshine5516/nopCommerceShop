using Nop.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Services.Caching
{
    public partial class CacheService : ICacheService
    {
        #region 字段
        private readonly ICacheManager _cacheManager;
        #endregion
        #region 构造函数
        public CacheService(ICacheManager cacheManager)
        {
            this._cacheManager = cacheManager;
        }
        #endregion
        /// <summary>
        /// 获取所有缓存
        /// </summary>
        public IPagedList<T> GetAllCategories<T>(string categoryName = "", int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var dataSource = this._cacheManager.GetAll<T>();
            //this._cacheManager
            //return new PagedList<T>(dataSource.ToList(), pageIndex, pageSize);
            return null;
            //_cacheManager.get
            //throw new NotImplementedException();
        }
    }
}

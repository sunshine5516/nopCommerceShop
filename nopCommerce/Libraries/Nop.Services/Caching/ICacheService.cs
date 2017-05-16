using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Caching
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public partial interface ICacheService
    {
        //void GetAllCache();
        IPagedList<T> GetAllCategories<T>(string categoryName = "", int storeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue);
    }
}

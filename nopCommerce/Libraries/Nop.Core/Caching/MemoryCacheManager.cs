using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Nop.Core.Caching
{
    /// <summary>
    /// MemoryCacheManager实现 代表用于在HTTP请求之间进行缓存的管理器（长期缓存）
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// 缓存对象
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }
        public virtual List<KeyValuePair<string, object>> GetAll<T>()
        {
            var keyValues = new List<KeyValuePair<string, object>>();
            IEnumerable<KeyValuePair<string, object>>
                items = Cache.AsEnumerable();
            foreach (KeyValuePair<string, object> item in items)
            {
                keyValues.Add(new System.Collections.Generic.KeyValuePair<string, object>(item.Key, item.Value));
            }
            return keyValues;
        }
        /// <summary>
        /// 通过Key获取缓存的value 
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>与Key对应的value.</returns>
        public virtual T Get<T>(string key)
        {
            

            //Cache.
            return (T)Cache[key];
        }
        //public virtual 
        /// <summary>
        /// 键值对添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">缓存时间</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// 根据key判断缓存时候存在 
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// 根据pattern删除缓存项
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern, Cache.Select(p => p.Key));
        }

        /// <summary>
        /// 删除所有缓存
        /// </summary>
        public virtual void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
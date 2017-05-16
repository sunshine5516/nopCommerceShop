using System;
using System.Collections.Generic;

namespace Nop.Core.Caching
{
    /// <summary>
    /// 表示NopNullCache（仅实现接口，不缓存任何内容）
    /// </summary>
    public partial class NopNullCache : ICacheManager
    {
        /// <summary>
        /// 获取或设置与指定键关联的值。
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>与指定键相关联的值</returns>
        public virtual T Get<T>(string key)
        {
            return default(T);
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">缓存时间</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
        }

        /// <summary>
        /// 根据key判断缓存时候存在
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            return false;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
        }

        /// <summary>
        /// 清空缓存数据
        /// </summary>
        public virtual void Clear()
        {
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
        }

        T ICacheManager.Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        List<KeyValuePair<string, object>> ICacheManager.GetAll<T>()
        {
            throw new NotImplementedException();
        }

        void ICacheManager.Set(string key, object data, int cacheTime)
        {
            throw new NotImplementedException();
        }

        bool ICacheManager.IsSet(string key)
        {
            throw new NotImplementedException();
        }

        void ICacheManager.Remove(string key)
        {
            throw new NotImplementedException();
        }

        void ICacheManager.RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        void ICacheManager.Clear()
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
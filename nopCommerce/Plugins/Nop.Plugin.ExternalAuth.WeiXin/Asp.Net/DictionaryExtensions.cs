using System;
using System.Collections.Generic;

namespace Nop.Plugin.ExternalAuth.WeiXin.Asp.Net
{
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// 添加字典参数
        /// </summary>
        internal static void AddItemIfNotEmpty(this IDictionary<string, string> dictionary, string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (!String.IsNullOrEmpty(value))
            {
                dictionary[key] = value;
            }
        }
    }
}

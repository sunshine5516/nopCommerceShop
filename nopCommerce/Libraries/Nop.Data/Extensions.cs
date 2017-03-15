using System;
using System.Data.Entity.Core.Objects;
using Nop.Core;

namespace Nop.Data
{
    public static class Extensions
    {
        /// <summary>
        /// 获取未授权的实体类型
        /// </summary>
        /// <remarks>如果EF上下文是代理启用的, 
        /// 运行时将创建实体的代理实例, 
        /// 即从实体类继承的动态生成的类 
        /// 并通过插入特定代码来覆盖其虚拟属性，例如用于跟踪更改和延迟加载。
        /// </remarks>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Type GetUnproxiedEntityType(this BaseEntity entity)
        {
            var userType = ObjectContext.GetObjectType(entity.GetType());
            return userType;
        }
    }
}

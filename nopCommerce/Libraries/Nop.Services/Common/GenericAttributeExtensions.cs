using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Data;

namespace Nop.Services.Common
{
    public static class GenericAttributeExtensions
    {
        /// <summary>
        /// 获取实体的属性
        /// </summary>
        /// <typeparam name="TPropType">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="key">Key</param>
        /// <param name="storeId">加载特定于某个商店的值; 传递0以加载所有商店共享的值</param>
        /// <returns>属性</returns>
        public static TPropType GetAttribute<TPropType>(this BaseEntity entity, string key, int storeId = 0)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            return GetAttribute<TPropType>(entity, key, genericAttributeService, storeId);
        }

        /// <summary>
        /// 获取实体的属性
        /// </summary>
        /// <typeparam name="TPropType">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="key">Key</param>
        /// <param name="genericAttributeService">GenericAttributeService</param>
        /// <param name="storeId">加载特定于某个商店的值; 传递0以加载所有商店共享的值</param>
        /// <returns>Attribute</returns>
        public static TPropType GetAttribute<TPropType>(this BaseEntity entity,
            string key, IGenericAttributeService genericAttributeService, int storeId = 0)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            string keyGroup = entity.GetUnproxiedEntityType().Name;

            var props = genericAttributeService.GetAttributesForEntity(entity.Id, keyGroup);
            //little hack here (only for unit testing). we should write ecpect-return rules in unit tests for such cases
            if (props == null)
                return default(TPropType);
            props = props.Where(x => x.StoreId == storeId).ToList();
            if (!props.Any())
                return default(TPropType);

            var prop = props.FirstOrDefault(ga =>
                ga.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)); //should be culture invariant

            if (prop == null || string.IsNullOrEmpty(prop.Value))
                return default(TPropType);

            return CommonHelper.To<TPropType>(prop.Value);
        }
    }
}

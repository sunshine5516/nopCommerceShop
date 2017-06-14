using System.Linq;
using FluentValidation;
using Nop.Data;

namespace Nop.Web.Framework.Validators
{
    public abstract class BaseNopValidator<T> : AbstractValidator<T> where T : class
    {
        protected BaseNopValidator()
        {
            PostInitialize();
        }

        /// <summary>
        /// 开发人员可以在自定义部分类中覆盖此方法
        /// 以便为构造函数添加一些自定义的初始化代码
        /// </summary>
        protected virtual void PostInitialize()
        {
            
        }

        /// <summary>
        /// 长度验证
        /// </summary>
        /// <typeparam name="TObject">Object type</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <param name="filterPropertyNames">Properties to skip</param>
        protected virtual void SetStringPropertiesMaxLength<TObject>(IDbContext dbContext, params string[] filterPropertyNames)
        {
            if(dbContext==null)
                return;

            var dbObjectType = typeof(TObject);

            var names = typeof (T).GetProperties()
                .Where(p => p.PropertyType == typeof(string) && !filterPropertyNames.Contains(p.Name))
                .Select(p => p.Name).ToArray();

            var maxLength = dbContext.GetColumnsMaxLength(dbObjectType.Name, names);
            var expression = maxLength.Keys.ToDictionary(name => name, name => Kendoui.DynamicExpression.ParseLambda<T, string>(name, null));

            foreach (var expr in expression)
            {
                RuleFor(expr.Value).Length(0, maxLength[expr.Key]);
            }
        }
    }
}
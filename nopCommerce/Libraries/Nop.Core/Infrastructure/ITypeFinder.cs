using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nop.Core.Infrastructure
{
    /// <summary>
    /// 实现此接口的类提供关于Nop引擎中各种服务的类型的信息。
    /// 类型查找器 为了支持插件功能，以及支持一些自动注册的功能。系统提供了类型查找器。
    /// ITypeFinder以及实现类就是提供此功能。通过类型查找器可以查找本程序域中的类，也可以查找整个bin目录下所有动态链接库中类，并把它们注册到类型反转容器中。
    /// 类型查找抽象接口
    /// Classes implementing this interface provide information about types 
    /// to various services in the Nop engine.
    /// </summary>
    public interface ITypeFinder
    {
        IList<Assembly> GetAssemblies();

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}

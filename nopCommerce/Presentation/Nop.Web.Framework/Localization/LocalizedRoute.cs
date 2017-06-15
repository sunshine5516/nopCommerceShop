using System.Web;
using System.Web.Routing;
using Nop.Core.Data;
using Nop.Core.Domain.Localization;
using Nop.Core.Infrastructure;

namespace Nop.Web.Framework.Localization
{
    /// <summary>
    /// 重写路由
    /// </summary>
    public class LocalizedRoute : Route
    {
        #region 字段

        private bool? _seoFriendlyUrlsForLanguagesEnabled;

        #endregion

        #region 构造函数

        /// <summary>
        /// 使用指定的URL模式和处理程序类初始化System.Web.Routing.Route类的新实例。
        /// </summary>
        /// <param name="url">路由的URL模式.</param>
        /// <param name="routeHandler">处理路由请求的对象.</param>
        public LocalizedRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
        }

        /// <summary>
        /// 使用指定的URL模式和处理程序类初始化System.Web.Routing.Route类的新实例
        /// </summary>
        /// <param name="url">路由的URL模式.</param>
        /// <param name="defaults">如果URL不包含所有参数，则使用的值.</param>
        /// <param name="routeHandler">处理路由请求的对象.</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// 使用指定的URL模式和处理程序类初始化System.Web.Routing.Route类的新实例。
        /// </summary>
        /// <param name="url">路由的URL模式.</param>
        /// <param name="defaults">如果URL不包含所有参数，则使用的值.</param>
        /// <param name="constraints">指定URL参数有效值的正则表达式.</param>
        /// <param name="routeHandler">处理路由请求的对象.</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// 使用指定的URL模式和处理程序类初始化System.Web.Routing.Route类的新实例。
        /// constraints,and custom values.
        /// </summary>
        /// <param name="url">路由的URL模式</param>
        /// <param name="defaults">如果URL不包含所有参数，则使用的值.</param>
        /// <param name="constraints">指定URL参数有效值的正则表达式.</param>
        /// <param name="dataTokens">传递给路由处理程序但不用于确定路由是否匹配特定URL模式的自定义值。 路由处理程序可能需要这些值来处理请求.</param>
        /// <param name="routeHandler">处理路由请求的对象.</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region 方法

        /// <summary>
        /// 入站URL进行匹配的工作机制，重写路由请求方法，返回请求路由信息
        /// </summary>
        /// <param name="httpContext">封装有关HTTP请求的信息的对象.</param>
        /// <returns>
        ///包含路由定义中的值的对象。
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (DataSettingsHelper.DatabaseIsInstalled() && this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
                string applicationPath = httpContext.Request.ApplicationPath;
                if (virtualPath.IsLocalizedUrl(applicationPath, false))
                {
                    //In ASP.NET Development Server, an URL like "http://localhost/Blog.aspx/Categories/BabyFrog" will return 
                    //"~/Blog.aspx/Categories/BabyFrog" as AppRelativeCurrentExecutionFilePath.
                    //However, in II6, the AppRelativeCurrentExecutionFilePath is "~/Blog.aspx"
                    //It seems that IIS6 think we're process Blog.aspx page.
                    //So, I'll use RawUrl to re-create an AppRelativeCurrentExecutionFilePath like ASP.NET Development Server.

                    //Question: should we do path rewriting right here?
                    string rawUrl = httpContext.Request.RawUrl;
                    var newVirtualPath = rawUrl.RemoveLanguageSeoCodeFromRawUrl(applicationPath);
                    if (string.IsNullOrEmpty(newVirtualPath))
                        newVirtualPath = "/";
                    newVirtualPath = newVirtualPath.RemoveApplicationPathFromRawUrl(applicationPath);
                    newVirtualPath = "~" + newVirtualPath;
                    httpContext.RewritePath(newVirtualPath, true);
                }
            }
            RouteData data = base.GetRouteData(httpContext);
            return data;
        }

        /// <summary>
        /// 出站URL生成的工作机制， 返回与路由相关联的网址的相关信息。
        /// </summary>
        /// <param name="requestContext">封装有关所请求路由的信息的对象.</param>
        /// <param name="values">包含路由参数的对象.</param>
        /// <returns>
        /// 包含与路由相关联的URL的信息的对象.
        /// </returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData data = base.GetVirtualPath(requestContext, values);

            if (data != null && DataSettingsHelper.DatabaseIsInstalled() && this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                string rawUrl = requestContext.HttpContext.Request.RawUrl;
                string applicationPath = requestContext.HttpContext.Request.ApplicationPath;
                if (rawUrl.IsLocalizedUrl(applicationPath, true))
                {
                    data.VirtualPath = string.Concat(rawUrl.GetLanguageSeoCodeFromUrl(applicationPath, true), "/",
                        data.VirtualPath);
                }
            }
            return data;
        }

        public virtual void ClearSeoFriendlyUrlsCachedValue()
        {
            _seoFriendlyUrlsForLanguagesEnabled = null;
        }

        #endregion

        #region 属性

        protected bool SeoFriendlyUrlsForLanguagesEnabled
        {
            get
            {
                if (!_seoFriendlyUrlsForLanguagesEnabled.HasValue)
                    _seoFriendlyUrlsForLanguagesEnabled = EngineContext.Current.Resolve<LocalizationSettings>().SeoFriendlyUrlsForLanguagesEnabled;

                return _seoFriendlyUrlsForLanguagesEnabled.Value;
            }
        }

        #endregion
    }
}
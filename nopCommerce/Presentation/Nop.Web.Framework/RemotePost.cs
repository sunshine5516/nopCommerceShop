using System.Collections.Specialized;
using System.Web;
using Nop.Core;
using Nop.Core.Infrastructure;

namespace Nop.Web.Framework
{
    /// <summary>
    /// RemotePost帮助类
    /// </summary>
    public partial class RemotePost
    {
        private readonly HttpContextBase _httpContext;
        private readonly IWebHelper _webHelper;
        private readonly NameValueCollection _inputValues;

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 方法（post，get）
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 获取或设置表单名称
        /// </summary>
        public string FormName { get; set; }

        /// <summary>
        /// 编码字符串
        /// </summary>
        public string AcceptCharset { get; set; }

        /// <summary>
        /// 是否应为相同的“名称”属性为每个值（如果有多个）创建一个新的“输入”HTML元素。
        /// </summary>
        public bool NewInputForEachValue { get; set; }

        public NameValueCollection Params
        {
            get
            {
                return _inputValues;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public RemotePost()
            : this(EngineContext.Current.Resolve<HttpContextBase>(), EngineContext.Current.Resolve<IWebHelper>())
        {
        }

        /// <summary>
        ///构造函数
        /// </summary>
        /// <param name="httpContext">HTTP Context</param>
        /// <param name="webHelper">Web helper</param>
        public RemotePost(HttpContextBase httpContext, IWebHelper webHelper)
        {
            this._inputValues = new NameValueCollection();
            this.Url = "http://www.someurl.com";
            this.Method = "post";
            this.FormName = "formName";

            this._httpContext = httpContext;
            this._webHelper = webHelper;
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="name">key</param>
        /// <param name="value">value</param>
        public void Add(string name, string value)
        {
            _inputValues.Add(name, value);
        }
        
        /// <summary>
        /// Post提交
        /// </summary>
        public void Post()
        {
            _httpContext.Response.Clear();
            _httpContext.Response.Write("<html><head>");
            _httpContext.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            if (!string.IsNullOrEmpty(AcceptCharset))
            {
                //AcceptCharset specified
                _httpContext.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" accept-charset=\"{3}\">", FormName, Method, Url, AcceptCharset));
            }
            else
            {
                //no AcceptCharset specified
                _httpContext.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            }
            if (NewInputForEachValue)
            {
                foreach (string key in _inputValues.Keys)
                {
                    string[] values = _inputValues.GetValues(key);
                    if (values != null)
                    {
                        foreach (string value in values)
                        {
                            _httpContext.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode(key), HttpUtility.HtmlEncode(value)));
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _inputValues.Keys.Count; i++)
                    _httpContext.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", HttpUtility.HtmlEncode(_inputValues.Keys[i]), HttpUtility.HtmlEncode(_inputValues[_inputValues.Keys[i]])));
            }
            _httpContext.Response.Write("</form>");
            _httpContext.Response.Write("</body></html>");
            _httpContext.Response.End();
            _webHelper.IsPostBeingDone = true;
        }
    }
}
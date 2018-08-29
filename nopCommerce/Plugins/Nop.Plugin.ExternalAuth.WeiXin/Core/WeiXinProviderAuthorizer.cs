using System;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using DotNetOpenAuth.AspNet;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Authentication.External;
using Nop.Plugin.ExternalAuth.WeiXin.Asp.Net;

namespace Nop.Plugin.ExternalAuth.WeiXin.Core
{
    public class WeiXinProviderAuthorizer:IOAuthProviderWeiXinAuthorizer
    {
        public AuthorizeState Authorize(string returnUrl, bool? verifyResponse = null)
        {
            throw new NotImplementedException();
        }
    }
}

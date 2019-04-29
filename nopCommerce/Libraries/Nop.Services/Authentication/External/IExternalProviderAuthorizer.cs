//Contributor:  Nicholas Mayne


namespace Nop.Services.Authentication.External
{
    /// <summary>
    /// 外部授权接口
    /// </summary>
    public partial interface IExternalProviderAuthorizer
    {
        /// <summary>
        /// Authorize response
        /// </summary>
        /// <param name="returnUrl">Return URL</param>
        /// <param name="verifyResponse">true - Verify response;false - request authentication;null - determine automatically</param>
        /// <returns>Authorize state</returns>
        AuthorizeState Authorize(string returnUrl, bool? verifyResponse = null);
    }
}
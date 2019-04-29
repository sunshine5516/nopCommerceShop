//Contributor:  Nicholas Mayne


namespace Nop.Services.Authentication.External
{
    /// <summary>
    /// ¿ª·ÅÊÚÈ¨×´Ì¬
    /// </summary>
    public enum OpenAuthenticationStatus
    {
        Unknown,
        Error,
        Authenticated,
        RequiresRedirect,
        AssociateOnLogon,
        AutoRegisteredEmailValidation,
        AutoRegisteredAdminApproval,
        AutoRegisteredStandard,
    }
}
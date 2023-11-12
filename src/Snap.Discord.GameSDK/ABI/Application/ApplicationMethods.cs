using Snap.Discord.GameSDK;
using Snap.Discord.GameSDK.Application;

namespace ABI.Snap.Discord.GameSDK.Application;

internal struct ApplicationMethods
{
    // void ValidateOrExitCallback(nint ptr, Result result)
    // void ValidateOrExitMethod(nint methodsPtr, nint callbackData, ValidateOrExitCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ApplicationMethods*, void*, delegate* unmanaged[Stdcall]<nint, Result, void>, void> ValidateOrExit;

    // void GetCurrentLocaleMethod(nint methodsPtr, StringBuilder locale)
    internal unsafe delegate* unmanaged[Stdcall]<ApplicationMethods*, byte*, void> GetCurrentLocale;

    // void GetCurrentBranchMethod(nint methodsPtr, StringBuilder branch)
    internal unsafe delegate* unmanaged[Stdcall]<ApplicationMethods*, byte*, void> GetCurrentBranch;

    // void GetOAuth2TokenCallback(nint ptr, Result result, ref OAuth2Token oauth2Token)
    // void GetOAuth2TokenMethod(nint methodsPtr, nint callbackData, GetOAuth2TokenCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ApplicationMethods*, void*, delegate* unmanaged[Stdcall]<nint, Result, OAuth2Token*, void>, void> GetOAuth2Token;

    // void GetTicketCallback(nint ptr, Result result, [MarshalAs(UnmanagedType.LPStr)] ref string data)
    // void GetTicketMethod(nint methodsPtr, nint callbackData, GetTicketCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ApplicationMethods*, void*, delegate* unmanaged[Stdcall]<nint, Result, byte*, void>, void> GetTicket;
}
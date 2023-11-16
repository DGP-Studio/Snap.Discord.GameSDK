using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

public class ApplicationManager
{
    private unsafe readonly ApplicationMethods* MethodsPtr;

    internal unsafe ApplicationManager(ApplicationMethods* ptr, ApplicationEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private unsafe static void InitEvents(ApplicationEvents* eventsPtr)
    {
    }

    public unsafe void ValidateOrExit(ValidateOrExitHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void ValidateOrExitCallbackImpl(ValidateOrExitHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->ValidateOrExit.Invoke(MethodsPtr, callback, ValidateOrExitCallback.Create(&ValidateOrExitCallbackImpl));
    }

    public unsafe string GetCurrentLocale()
    {
        byte[] ret = new byte[128];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetCurrentLocale.Invoke(MethodsPtr, pRet);
            return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(pRet));
        }
    }

    public unsafe string GetCurrentBranch()
    {
        byte[] ret = new byte[4096];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetCurrentBranch.Invoke(MethodsPtr, pRet);
            return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(pRet));
        }
    }

    public unsafe void GetOAuth2Token(GetOAuth2TokenHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void GetOAuth2TokenCallbackImpl(GetOAuth2TokenHandler ptr, Result result, OAuth2Token* oauth2Token)
        {
            ptr.Invoke(result, oauth2Token);
        }

        MethodsPtr->GetOAuth2Token.Invoke(MethodsPtr, callback, GetOAuth2TokenCallback.Create(&GetOAuth2TokenCallbackImpl));
    }

    public unsafe void GetTicket(GetTicketHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void GetTicketCallbackImpl(GetTicketHandler ptr, Result result, byte* data)
        {
            ptr.Invoke(result, data);
        }

        MethodsPtr->GetTicket.Invoke(MethodsPtr, callback, GetTicketCallback.Create(&GetTicketCallbackImpl));
    }
}
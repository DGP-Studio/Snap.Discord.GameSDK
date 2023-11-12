using ABI.Snap.Discord.GameSDK.Application;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK.Application;

public class ApplicationManager
{
    private unsafe readonly ApplicationMethods* MethodsPtr;

    internal unsafe ApplicationManager(ApplicationMethods* ptr, nint eventsPtr, ref ApplicationEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private unsafe static void InitEvents(nint eventsPtr, ref ApplicationEvents events)
    {
        *(ApplicationEvents*)eventsPtr = events;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void ValidateOrExitCallbackImpl(nint ptr, Result result)
    {
        // void ValidateOrExitHandler(Result result)
        ((delegate* unmanaged[Stdcall]<Result, void>)ptr)(result);
    }

    public unsafe void ValidateOrExit(delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->ValidateOrExit(MethodsPtr, callback, &ValidateOrExitCallbackImpl);
    }

    public unsafe string GetCurrentLocale()
    {
        byte[] ret = new byte[128];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetCurrentLocale(MethodsPtr, pRet);
            return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(pRet));
        }
    }

    public unsafe string GetCurrentBranch()
    {
        byte[] ret = new byte[4096];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetCurrentBranch(MethodsPtr, pRet);
            return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(pRet));
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void GetOAuth2TokenCallbackImpl(nint ptr, Result result, OAuth2Token* oauth2Token)
    {
        // void GetOAuth2TokenHandler(Result result, ref OAuth2Token oauth2Token)
        ((delegate* unmanaged[Stdcall]<Result, OAuth2Token*, void>)ptr)(result, oauth2Token);
    }

    public unsafe void GetOAuth2Token(delegate* unmanaged[Stdcall]<Result, OAuth2Token*, void> callback)
    {
        MethodsPtr->GetOAuth2Token(MethodsPtr, callback, &GetOAuth2TokenCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void GetTicketCallbackImpl(nint ptr, Result result, byte* data)
    {
        // void GetTicketHandler(Result result, ref string data)
        ((delegate* unmanaged[Stdcall]<Result, byte*, void>)ptr)(result, data);
    }

    public unsafe void GetTicket(delegate* unmanaged[Stdcall]<Result, byte*, void> callback)
    {
        MethodsPtr->GetTicket(MethodsPtr, callback, &GetTicketCallbackImpl);
    }
}
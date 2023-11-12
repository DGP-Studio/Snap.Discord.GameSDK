using Snap.Discord.GameSDK;
using Snap.Discord.GameSDK.Activity;

namespace ABI.Snap.Discord.GameSDK.Activity;

internal struct ActivityMethods
{
    // Result RegisterCommandMethod(nint methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string command)
    internal unsafe delegate* unmanaged[Stdcall]<ActivityMethods*, byte*, Result> RegisterCommand;

    // Result RegisterSteamMethod(nint methodsPtr, uint steamId)
    internal unsafe delegate* unmanaged[Stdcall]<ActivityMethods*, uint, Result> RegisterSteam;

    // void UpdateActivityHandler(Result result)
    // void UpdateActivityCallback(nint ptr, Result result)
    // void UpdateActivityMethod(nint methodsPtr, ref Activity activity, nint callbackData, UpdateActivityCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ActivityMethods*, global::Snap.Discord.GameSDK.Activity.Activity*, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> UpdateActivity;

    // void ClearActivityHandler(Result result)
    // void ClearActivityCallback(nint ptr, Result result)
    // void ClearActivityMethod(nint methodsPtr, nint callbackData, ClearActivityCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ActivityMethods*, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> ClearActivity;

    // void SendRequestReplyHandler(Result result)
    // void SendRequestReplyCallback(nint ptr, Result result)
    // void SendRequestReplyMethod(nint methodsPtr, long userId, ActivityJoinRequestReply reply, nint callbackData, SendRequestReplyCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ActivityMethods*, long, ActivityJoinRequestReply, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> SendRequestReply;

    // void SendInviteHandler(Result result)
    // void SendInviteCallback(nint ptr, Result result)
    // void SendInviteMethod(nint methodsPtr, long userId, ActivityActionType type, [MarshalAs(UnmanagedType.LPStr)] string content, nint callbackData, SendInviteCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ActivityMethods*, long, ActivityActionType, byte*, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> SendInvite;

    // void AcceptInviteHandler(Result result)
    // void AcceptInviteCallback(nint ptr, Result result)
    // void AcceptInviteMethod(nint methodsPtr, long userId, nint callbackData, AcceptInviteCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<ActivityMethods*, long, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> AcceptInvite;
}
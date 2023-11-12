using Snap.Discord.GameSDK;
using Snap.Discord.GameSDK.Activity;

namespace ABI.Snap.Discord.GameSDK.Activity;

internal struct ActivityEvents
{
    // void ActivityJoinHandler(nint ptr, [MarshalAs(UnmanagedType.LPStr)] string secret)
    internal unsafe delegate* unmanaged[Stdcall]<nint, byte*, void> OnActivityJoin;

    // void ActivitySpectateHandler(nint ptr, [MarshalAs(UnmanagedType.LPStr)] string secret)
    internal unsafe delegate* unmanaged[Stdcall]<nint, byte*, void> OnActivitySpectate;

    // void ActivityJoinRequestHandler(nint ptr, ref User user)
    internal unsafe delegate* unmanaged[Stdcall]<nint, User*, void> OnActivityJoinRequest;

    // void ActivityInviteHandler(nint ptr, ActivityActionType type, ref User user, ref Activity activity)
    internal unsafe delegate* unmanaged[Stdcall]<nint, ActivityActionType, User*, global::Snap.Discord.GameSDK.Activity.Activity*, void> OnActivityInvite;
}
namespace Snap.Discord.GameSDK.ABI;

public struct NetworkEvents
{
    // void MessageHandler(IntPtr ptr, UInt64 peerId, byte channelId, IntPtr dataPtr, Int32 dataLen)
    internal unsafe delegate* unmanaged[Stdcall]<nint, ulong, byte, nint, int, void> OnMessage;

    // void RouteUpdateHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string routeData)
    internal unsafe delegate* unmanaged[Stdcall]<nint, byte*, void> OnRouteUpdate;
}
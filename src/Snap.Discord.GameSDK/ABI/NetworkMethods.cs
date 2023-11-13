using Snap.Discord.GameSDK;

namespace Snap.Discord.GameSDK.ABI;

public struct NetworkMethods
{
    // void GetPeerIdMethod(IntPtr methodsPtr, ref UInt64 peerId)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, ulong*, void> GetPeerId;

    // Result FlushMethod(IntPtr methodsPtr)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, Result> Flush;

    // Result OpenPeerMethod(IntPtr methodsPtr, UInt64 peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, ulong, byte*, Result> OpenPeer;

    // Result UpdatePeerMethod(IntPtr methodsPtr, UInt64 peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, ulong, byte*, Result> UpdatePeer;

    // Result ClosePeerMethod(IntPtr methodsPtr, UInt64 peerId)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, ulong, Result> ClosePeer;

    // Result OpenChannelMethod(IntPtr methodsPtr, UInt64 peerId, byte channelId, bool reliable)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, ulong, byte, bool, Result> OpenChannel;

    // delegate Result CloseChannelMethod(IntPtr methodsPtr, UInt64 peerId, byte channelId)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, ulong, byte, Result> CloseChannel;

    // Result SendMessageMethod(IntPtr methodsPtr, UInt64 peerId, byte channelId, byte[] data, Int32 dataLen)
    internal unsafe delegate* unmanaged[Stdcall]<NetworkMethods*, ulong, byte, byte*, int, Result> SendMessage;
}
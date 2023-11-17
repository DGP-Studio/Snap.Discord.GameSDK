using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public class NetworkManager
{
    private unsafe readonly NetworkMethods* MethodsPtr;

    internal unsafe NetworkManager(NetworkMethods* ptr, NetworkEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(NetworkEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnMessageImpl(nint ptr, ulong peerId, byte channelId, nint dataPtr, int dataLen)
        {
            DiscordGCHandle.Get(ptr).NetworkManagerInstance?.OnMessage(peerId, channelId, new((void*)dataPtr, dataLen));
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnRouteUpdateImpl(nint ptr, byte* routeData)
        {
            DiscordGCHandle.Get(ptr).NetworkManagerInstance?.OnRouteUpdate(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(routeData));
        }

        eventsPtr->OnMessage = MessageHandler.Create(&OnMessageImpl);
        eventsPtr->OnRouteUpdate = RouteUpdateHandler.Create(&OnRouteUpdateImpl);
    }

    /// <summary>
    /// Get the local peer ID for this process.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe ulong GetPeerId()
    {
        ulong ret = default;
        MethodsPtr->GetPeerId.Invoke(MethodsPtr, &ret);
        return ret;
    }

    /// <summary>
    /// Send pending network messages.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe void Flush()
    {
        MethodsPtr->Flush.Invoke(MethodsPtr).ThrowOnFailure();
    }

    /// <summary>
    /// Open a connection to a remote peer.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe void OpenPeer(ulong peerId, string routeData)
    {
        byte[] routeDataBytes = Encoding.UTF8.GetBytes(routeData);
        fixed (byte* pRouteData = routeDataBytes)
        {
            MethodsPtr->OpenPeer.Invoke(MethodsPtr, peerId, pRouteData).ThrowOnFailure();
        }
    }

    /// <summary>
    /// Update the route data for a connected peer.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe void UpdatePeer(ulong peerId, string routeData)
    {
        byte[] routeDataBytes = Encoding.UTF8.GetBytes(routeData);
        fixed (byte* pRouteData = routeDataBytes)
        {
            MethodsPtr->UpdatePeer.Invoke(MethodsPtr, peerId, pRouteData).ThrowOnFailure();
        }
    }

    /// <summary>
    /// Close the connection to a remote peer.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe void ClosePeer(ulong peerId)
    {
        MethodsPtr->ClosePeer.Invoke(MethodsPtr, peerId).ThrowOnFailure();
    }

    /// <summary>
    /// Open a message channel to a connected peer.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe void OpenChannel(ulong peerId, byte channelId, bool reliable)
    {
        MethodsPtr->OpenChannel.Invoke(MethodsPtr, peerId, channelId, reliable).ThrowOnFailure();
    }

    /// <summary>
    /// Close a message channel to a connected peer.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe void CloseChannel(ulong peerId, byte channelId)
    {
        MethodsPtr->CloseChannel.Invoke(MethodsPtr, peerId, channelId).ThrowOnFailure();
    }

    /// <summary>
    /// Send a message to a connected peer over an opened message channel.
    /// </summary>
    [Obsolete("Deprecated by Discord")]
    public unsafe void SendMessage(ulong peerId, byte channelId, byte[] data)
    {
        fixed (byte* pData = data)
        {
            MethodsPtr->SendMessage.Invoke(MethodsPtr, peerId, channelId, pData, data.Length).ThrowOnFailure();
        }
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnMessage(ulong peerId, byte channelId, ReadOnlySpan<byte> data)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnRouteUpdate(ReadOnlySpan<byte> routeData)
    {
    }
}
using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

public class NetworkManager
{
    public delegate void MessageHandler(ulong peerId, byte channelId, byte[] data);

    public delegate void RouteUpdateHandler(string routeData);

    private unsafe readonly NetworkMethods* MethodsPtr;

    public event MessageHandler? OnMessage;

    public event RouteUpdateHandler? OnRouteUpdate;

    internal unsafe NetworkManager(NetworkMethods* ptr, nint eventsPtr, ref NetworkEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private unsafe void InitEvents(nint eventsPtr, ref NetworkEvents events)
    {
        events.OnMessage = &OnMessageImpl;
        events.OnRouteUpdate = &OnRouteUpdateImpl;
        *(NetworkEvents*)eventsPtr = events;
    }

    /// <summary>
    /// Get the local peer ID for this process.
    /// </summary>
    public unsafe ulong GetPeerId()
    {
        ulong ret = default;
        MethodsPtr->GetPeerId(MethodsPtr, &ret);
        return ret;
    }

    /// <summary>
    /// Send pending network messages.
    /// </summary>
    public unsafe void Flush()
    {
        MethodsPtr->Flush(MethodsPtr).ThrowOnFailure();
    }

    /// <summary>
    /// Open a connection to a remote peer.
    /// </summary>
    public unsafe void OpenPeer(ulong peerId, string routeData)
    {
        byte[] routeDataBytes = Encoding.UTF8.GetBytes(routeData);
        fixed (byte* pRouteData = routeDataBytes)
        {
            MethodsPtr->OpenPeer(MethodsPtr, peerId, pRouteData).ThrowOnFailure();
        }
    }

    /// <summary>
    /// Update the route data for a connected peer.
    /// </summary>
    public unsafe void UpdatePeer(ulong peerId, string routeData)
    {
        byte[] routeDataBytes = Encoding.UTF8.GetBytes(routeData);
        fixed (byte* pRouteData = routeDataBytes)
        {
            MethodsPtr->UpdatePeer(MethodsPtr, peerId, pRouteData).ThrowOnFailure();
        }
    }

    /// <summary>
    /// Close the connection to a remote peer.
    /// </summary>
    public unsafe void ClosePeer(ulong peerId)
    {
        MethodsPtr->ClosePeer(MethodsPtr, peerId).ThrowOnFailure();
    }

    /// <summary>
    /// Open a message channel to a connected peer.
    /// </summary>
    public unsafe void OpenChannel(ulong peerId, byte channelId, bool reliable)
    {
        MethodsPtr->OpenChannel(MethodsPtr, peerId, channelId, reliable).ThrowOnFailure();
    }

    /// <summary>
    /// Close a message channel to a connected peer.
    /// </summary>
    public unsafe void CloseChannel(ulong peerId, byte channelId)
    {
        MethodsPtr->CloseChannel(MethodsPtr, peerId, channelId).ThrowOnFailure();
    }

    /// <summary>
    /// Send a message to a connected peer over an opened message channel.
    /// </summary>
    public unsafe void SendMessage(ulong peerId, byte channelId, byte[] data)
    {
        fixed (byte* pData = data)
        {
            MethodsPtr->SendMessage(MethodsPtr, peerId, channelId, pData, data.Length).ThrowOnFailure();
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnMessageImpl(nint ptr, ulong peerId, byte channelId, nint dataPtr, int dataLen)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        if (d.NetworkManagerInstance.OnMessage != null)
        {
            byte[] data = new byte[dataLen];
            Marshal.Copy(dataPtr, data, 0, dataLen);
            d.NetworkManagerInstance.OnMessage.Invoke(peerId, channelId, data);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnRouteUpdateImpl(nint ptr, byte* routeData)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        if (d.NetworkManagerInstance.OnRouteUpdate != null)
        {
            d.NetworkManagerInstance.OnRouteUpdate.Invoke(routeData);
        }
    }
}
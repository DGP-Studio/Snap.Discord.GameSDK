using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public partial class NetworkManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIEvents
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void MessageHandler(IntPtr ptr, UInt64 peerId, byte channelId, IntPtr dataPtr, Int32 dataLen);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void RouteUpdateHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)] string routeData);

        internal MessageHandler OnMessage;

        internal RouteUpdateHandler OnRouteUpdate;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void GetPeerIdMethod(IntPtr methodsPtr, ref UInt64 peerId);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result FlushMethod(IntPtr methodsPtr);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result OpenPeerMethod(IntPtr methodsPtr, UInt64 peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result UpdatePeerMethod(IntPtr methodsPtr, UInt64 peerId, [MarshalAs(UnmanagedType.LPStr)] string routeData);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result ClosePeerMethod(IntPtr methodsPtr, UInt64 peerId);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result OpenChannelMethod(IntPtr methodsPtr, UInt64 peerId, byte channelId, bool reliable);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result CloseChannelMethod(IntPtr methodsPtr, UInt64 peerId, byte channelId);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result SendMessageMethod(IntPtr methodsPtr, UInt64 peerId, byte channelId, byte[] data, Int32 dataLen);

        internal GetPeerIdMethod GetPeerId;

        internal FlushMethod Flush;

        internal OpenPeerMethod OpenPeer;

        internal UpdatePeerMethod UpdatePeer;

        internal ClosePeerMethod ClosePeer;

        internal OpenChannelMethod OpenChannel;

        internal CloseChannelMethod CloseChannel;

        internal SendMessageMethod SendMessage;
    }

    public delegate void MessageHandler(UInt64 peerId, byte channelId, byte[] data);

    public delegate void RouteUpdateHandler(string routeData);

    private IntPtr MethodsPtr;

    private Object MethodsStructure;

    private FFIMethods Methods
    {
        get
        {
            if (MethodsStructure == null)
            {
                MethodsStructure = Marshal.PtrToStructure(MethodsPtr, typeof(FFIMethods));
            }
            return (FFIMethods)MethodsStructure;
        }

    }

    public event MessageHandler OnMessage;

    public event RouteUpdateHandler OnRouteUpdate;

    internal NetworkManager(IntPtr ptr, IntPtr eventsPtr, ref FFIEvents events)
    {
        if (eventsPtr == IntPtr.Zero)
        {
            throw new ResultException(Result.InternalError);
        }
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
        if (MethodsPtr == IntPtr.Zero)
        {
            throw new ResultException(Result.InternalError);
        }
    }

    private void InitEvents(IntPtr eventsPtr, ref FFIEvents events)
    {
        events.OnMessage = OnMessageImpl;
        events.OnRouteUpdate = OnRouteUpdateImpl;
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    /// <summary>
    /// Get the local peer ID for this process.
    /// </summary>
    public UInt64 GetPeerId()
    {
        var ret = new UInt64();
        Methods.GetPeerId(MethodsPtr, ref ret);
        return ret;
    }

    /// <summary>
    /// Send pending network messages.
    /// </summary>
    public void Flush()
    {
        var res = Methods.Flush(MethodsPtr);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    /// <summary>
    /// Open a connection to a remote peer.
    /// </summary>
    public void OpenPeer(UInt64 peerId, string routeData)
    {
        var res = Methods.OpenPeer(MethodsPtr, peerId, routeData);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    /// <summary>
    /// Update the route data for a connected peer.
    /// </summary>
    public void UpdatePeer(UInt64 peerId, string routeData)
    {
        var res = Methods.UpdatePeer(MethodsPtr, peerId, routeData);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    /// <summary>
    /// Close the connection to a remote peer.
    /// </summary>
    public void ClosePeer(UInt64 peerId)
    {
        var res = Methods.ClosePeer(MethodsPtr, peerId);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    /// <summary>
    /// Open a message channel to a connected peer.
    /// </summary>
    public void OpenChannel(UInt64 peerId, byte channelId, bool reliable)
    {
        var res = Methods.OpenChannel(MethodsPtr, peerId, channelId, reliable);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    /// <summary>
    /// Close a message channel to a connected peer.
    /// </summary>
    public void CloseChannel(UInt64 peerId, byte channelId)
    {
        var res = Methods.CloseChannel(MethodsPtr, peerId, channelId);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    /// <summary>
    /// Send a message to a connected peer over an opened message channel.
    /// </summary>
    public void SendMessage(UInt64 peerId, byte channelId, byte[] data)
    {
        var res = Methods.SendMessage(MethodsPtr, peerId, channelId, data, data.Length);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    [MonoPInvokeCallback]
    private static void OnMessageImpl(IntPtr ptr, UInt64 peerId, byte channelId, IntPtr dataPtr, Int32 dataLen)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.NetworkManagerInstance.OnMessage != null)
        {
            byte[] data = new byte[dataLen];
            Marshal.Copy(dataPtr, data, 0, (int)dataLen);
            d.NetworkManagerInstance.OnMessage.Invoke(peerId, channelId, data);
        }
    }

    [MonoPInvokeCallback]
    private static void OnRouteUpdateImpl(IntPtr ptr, string routeData)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.NetworkManagerInstance.OnRouteUpdate != null)
        {
            d.NetworkManagerInstance.OnRouteUpdate.Invoke(routeData);
        }
    }
}

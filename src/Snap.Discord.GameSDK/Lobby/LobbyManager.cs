using ABI.Snap.Discord.GameSDK.Lobby;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK.Lobby;

public partial class LobbyManager
{
    public delegate void LobbyUpdateHandler(long lobbyId);

    public delegate void LobbyDeleteHandler(long lobbyId, uint reason);

    public delegate void MemberConnectHandler(long lobbyId, long userId);

    public delegate void MemberUpdateHandler(long lobbyId, long userId);

    public delegate void MemberDisconnectHandler(long lobbyId, long userId);

    public delegate void LobbyMessageHandler(long lobbyId, long userId, byte[] data);

    public delegate void SpeakingHandler(long lobbyId, long userId, bool speaking);

    public delegate void NetworkMessageHandler(long lobbyId, long userId, byte channelId, byte[] data);

    private readonly unsafe LobbyMethods* MethodsPtr;

    public event LobbyUpdateHandler? OnLobbyUpdate;

    public event LobbyDeleteHandler? OnLobbyDelete;

    public event MemberConnectHandler? OnMemberConnect;

    public event MemberUpdateHandler? OnMemberUpdate;

    public event MemberDisconnectHandler? OnMemberDisconnect;

    public event LobbyMessageHandler? OnLobbyMessage;

    public event SpeakingHandler? OnSpeaking;

    public event NetworkMessageHandler? OnNetworkMessage;

    internal unsafe LobbyManager(LobbyMethods* ptr, nint eventsPtr, ref LobbyEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private unsafe void InitEvents(nint eventsPtr, ref LobbyEvents events)
    {
        events.OnLobbyUpdate = &OnLobbyUpdateImpl;
        events.OnLobbyDelete = &OnLobbyDeleteImpl;
        events.OnMemberConnect = &OnMemberConnectImpl;
        events.OnMemberUpdate = &OnMemberUpdateImpl;
        events.OnMemberDisconnect = &OnMemberDisconnectImpl;
        events.OnLobbyMessage = &OnLobbyMessageImpl;
        events.OnSpeaking = &OnSpeakingImpl;
        events.OnNetworkMessage = &OnNetworkMessageImpl;
        *(LobbyEvents*)eventsPtr = events;
    }

    public unsafe LobbyTransaction GetLobbyCreateTransaction()
    {
        LobbyTransaction ret = default;
        Result res = MethodsPtr->GetLobbyCreateTransaction(MethodsPtr, &ret.MethodsPtr);
        ResultException.ThrowOnFailure(res);
        return ret;
    }

    public unsafe LobbyTransaction GetLobbyUpdateTransaction(long lobbyId)
    {
        LobbyTransaction ret = default;
        Result res = MethodsPtr->GetLobbyUpdateTransaction(MethodsPtr, lobbyId, &ret.MethodsPtr);
        ResultException.ThrowOnFailure(res);
        return ret;
    }

    public unsafe LobbyMemberTransaction GetMemberUpdateTransaction(long lobbyId, long userId)
    {
        LobbyMemberTransaction ret = default;
        Result res = MethodsPtr->GetMemberUpdateTransaction(MethodsPtr, lobbyId, userId, &ret.MethodsPtr);
        ResultException.ThrowOnFailure(res);
        return ret;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void CreateLobbyCallbackImpl(delegate* unmanaged[Stdcall]<Result, Lobby*, void> ptr, Result result, Lobby* lobby)
    {
        ptr(result, lobby);
    }

    public unsafe void CreateLobby(ref LobbyTransaction transaction, delegate* unmanaged[Stdcall]<Result, Lobby*, void> callback)
    {
        MethodsPtr->CreateLobby(MethodsPtr, transaction.MethodsPtr, callback, &CreateLobbyCallbackImpl);
        transaction.MethodsPtr = null;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void UpdateLobbyCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void UpdateLobby(long lobbyId, ref LobbyTransaction transaction, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->UpdateLobby(MethodsPtr, lobbyId, transaction.MethodsPtr, callback, &UpdateLobbyCallbackImpl);
        transaction.MethodsPtr = null;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void DeleteLobbyCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void DeleteLobby(long lobbyId, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->DeleteLobby(MethodsPtr, lobbyId, callback, &DeleteLobbyCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void ConnectLobbyCallbackImpl(delegate* unmanaged[Stdcall]<Result, Lobby*, void> ptr, Result result, Lobby* lobby)
    {
        ptr(result, lobby);
    }

    public unsafe void ConnectLobby(long lobbyId, string secret, delegate* unmanaged[Stdcall]<Result, Lobby*, void> callback)
    {
        byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
        fixed (byte* pSecret = secretBytes)
        {
            MethodsPtr->ConnectLobby(MethodsPtr, lobbyId, pSecret, callback, &ConnectLobbyCallbackImpl);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void ConnectLobbyWithActivitySecretCallbackImpl(delegate* unmanaged[Stdcall]<Result, Lobby*, void> ptr, Result result, Lobby* lobby)
    {
        ptr(result, lobby);
    }

    public unsafe void ConnectLobbyWithActivitySecret(string activitySecret, delegate* unmanaged[Stdcall]<Result, Lobby*, void> callback)
    {
        byte[] activitySecretBytes = Encoding.UTF8.GetBytes(activitySecret);
        fixed(byte* pActivitySecret = activitySecretBytes)
        {
            MethodsPtr->ConnectLobbyWithActivitySecret(MethodsPtr, pActivitySecret, callback, &ConnectLobbyWithActivitySecretCallbackImpl);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void DisconnectLobbyCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void DisconnectLobby(long lobbyId, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->DisconnectLobby(MethodsPtr, lobbyId, callback, &DisconnectLobbyCallbackImpl);
    }

    public unsafe Lobby GetLobby(long lobbyId)
    {
        Lobby ret = default;
        MethodsPtr->GetLobby(MethodsPtr, lobbyId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe string GetLobbyActivitySecret(long lobbyId)
    {
        byte[] ret = new byte[128];
        fixed(byte* pRet = ret)
        {
            MethodsPtr->GetLobbyActivitySecret(MethodsPtr, lobbyId, pRet).ThrowOnFailure();
            return Encoding.UTF8.GetString(ret);
        }
    }

    public unsafe string GetLobbyMetadataValue(long lobbyId, string key)
    {
        byte[] ret = new byte[4096];
        fixed (byte* pRet = ret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed (byte* pKey = keyBytes)
            {
                MethodsPtr->GetLobbyMetadataValue(MethodsPtr, lobbyId, pKey, pRet).ThrowOnFailure();
                return Encoding.UTF8.GetString(ret);
            }
        }
    }

    public unsafe string GetLobbyMetadataKey(long lobbyId, int index)
    {
        byte[] ret = new byte[256];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetLobbyMetadataKey(MethodsPtr, lobbyId, index, pRet).ThrowOnFailure();
            return Encoding.UTF8.GetString(ret);
        }
    }

    public unsafe int LobbyMetadataCount(long lobbyId)
    {
        int ret = default;
        MethodsPtr->LobbyMetadataCount(MethodsPtr, lobbyId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe int MemberCount(long lobbyId)
    {
        int ret = default;
        MethodsPtr->MemberCount(MethodsPtr, lobbyId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe long GetMemberUserId(long lobbyId, int index)
    {
        long ret = default;
        MethodsPtr->GetMemberUserId(MethodsPtr, lobbyId, index, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe User GetMemberUser(long lobbyId, long userId)
    {
        User ret = default;
        MethodsPtr->GetMemberUser(MethodsPtr, lobbyId, userId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe string GetMemberMetadataValue(long lobbyId, long userId, string key)
    {
        byte[] ret = new byte[4096];
        fixed(byte* pRet = ret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed(byte* pKey = keyBytes)
            {
                MethodsPtr->GetMemberMetadataValue(MethodsPtr, lobbyId, userId, pKey, pRet).ThrowOnFailure();
                return Encoding.UTF8.GetString(ret);
            }
        }
    }

    public unsafe string GetMemberMetadataKey(long lobbyId, long userId, int index)
    {
        byte[] ret = new byte[256];
        fixed(byte* pRet = ret)
        {
            MethodsPtr->GetMemberMetadataKey(MethodsPtr, lobbyId, userId, index, pRet).ThrowOnFailure();
            return Encoding.UTF8.GetString(ret);
        }
    }

    public unsafe int MemberMetadataCount(long lobbyId, long userId)
    {
        int ret = default;
        MethodsPtr->MemberMetadataCount(MethodsPtr, lobbyId, userId, &ret).ThrowOnFailure();
        return ret;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void UpdateMemberCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void UpdateMember(long lobbyId, long userId, ref LobbyMemberTransaction transaction, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->UpdateMember(MethodsPtr, lobbyId, userId, transaction.MethodsPtr, callback, &UpdateMemberCallbackImpl);
        transaction.MethodsPtr = null;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void SendLobbyMessageCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void SendLobbyMessage(long lobbyId, byte[] data, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        fixed(byte* pData = data)
        {
            MethodsPtr->SendLobbyMessage(MethodsPtr, lobbyId, pData, data.Length, callback, &SendLobbyMessageCallbackImpl);
        }
    }

    public unsafe LobbySearchQuery GetSearchQuery()
    {
        LobbySearchQuery ret = default;
        MethodsPtr->GetSearchQuery(MethodsPtr, &ret.MethodsPtr).ThrowOnFailure();
        return ret;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void SearchCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void Search(ref LobbySearchQuery query, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->Search(MethodsPtr, query.MethodsPtr, callback, &SearchCallbackImpl);
        query.MethodsPtr = null;
    }

    public unsafe int LobbyCount()
    {
        int ret = default;
        MethodsPtr->LobbyCount(MethodsPtr, &ret);
        return ret;
    }

    public unsafe long GetLobbyId(int index)
    {
        long ret = default;
        MethodsPtr->GetLobbyId(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void ConnectVoiceCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void ConnectVoice(long lobbyId, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->ConnectVoice(MethodsPtr, lobbyId, callback, &ConnectVoiceCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void DisconnectVoiceCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void DisconnectVoice(long lobbyId, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->DisconnectVoice(MethodsPtr, lobbyId, callback, &DisconnectVoiceCallbackImpl);
    }

    public unsafe void ConnectNetwork(long lobbyId)
    {
        MethodsPtr->ConnectNetwork(MethodsPtr, lobbyId).ThrowOnFailure();
    }

    public unsafe void DisconnectNetwork(long lobbyId)
    {
        MethodsPtr->DisconnectNetwork(MethodsPtr, lobbyId).ThrowOnFailure();
    }

    public unsafe void FlushNetwork()
    {
        MethodsPtr->FlushNetwork(MethodsPtr).ThrowOnFailure();
    }

    public unsafe void OpenNetworkChannel(long lobbyId, byte channelId, bool reliable)
    {
        MethodsPtr->OpenNetworkChannel(MethodsPtr, lobbyId, channelId, reliable).ThrowOnFailure();
    }

    public unsafe void SendNetworkMessage(long lobbyId, long userId, byte channelId, byte[] data)
    {
        fixed(byte* pData = data)
        {
            MethodsPtr->SendNetworkMessage(MethodsPtr, lobbyId, userId, channelId, pData, data.Length).ThrowOnFailure();
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnLobbyUpdateImpl(nint ptr, long lobbyId)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        d.LobbyManagerInstance.OnLobbyUpdate?.Invoke(lobbyId);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnLobbyDeleteImpl(nint ptr, long lobbyId, uint reason)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        d.LobbyManagerInstance.OnLobbyDelete?.Invoke(lobbyId, reason);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnMemberConnectImpl(nint ptr, long lobbyId, long userId)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        d.LobbyManagerInstance.OnMemberConnect?.Invoke(lobbyId, userId);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnMemberUpdateImpl(nint ptr, long lobbyId, long userId)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        if (d.LobbyManagerInstance.OnMemberUpdate != null)
        {
            d.LobbyManagerInstance.OnMemberUpdate.Invoke(lobbyId, userId);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnMemberDisconnectImpl(nint ptr, long lobbyId, long userId)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        if (d.LobbyManagerInstance.OnMemberDisconnect != null)
        {
            d.LobbyManagerInstance.OnMemberDisconnect.Invoke(lobbyId, userId);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnLobbyMessageImpl(nint ptr, long lobbyId, long userId, nint dataPtr, int dataLen)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        if (d.LobbyManagerInstance.OnLobbyMessage != null)
        {
            byte[] data = new byte[dataLen];
            Marshal.Copy(dataPtr, data, 0, dataLen);
            d.LobbyManagerInstance.OnLobbyMessage.Invoke(lobbyId, userId, data);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnSpeakingImpl(nint ptr, long lobbyId, long userId, bool speaking)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        if (d.LobbyManagerInstance.OnSpeaking != null)
        {
            d.LobbyManagerInstance.OnSpeaking.Invoke(lobbyId, userId, speaking);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static void OnNetworkMessageImpl(nint ptr, long lobbyId, long userId, byte channelId, nint dataPtr, int dataLen)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        Discord d = (Discord)h.Target;
        if (d.LobbyManagerInstance.OnNetworkMessage != null)
        {
            byte[] data = new byte[dataLen];
            Marshal.Copy(dataPtr, data, 0, dataLen);
            d.LobbyManagerInstance.OnNetworkMessage.Invoke(lobbyId, userId, channelId, data);
        }
    }
}
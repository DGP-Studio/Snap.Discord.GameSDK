using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public class LobbyManager
{
    private readonly unsafe LobbyMethods* MethodsPtr;

    internal unsafe LobbyManager(LobbyMethods* ptr, LobbyEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(LobbyEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnLobbyUpdateImpl(nint ptr, long lobbyId)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnLobbyUpdate(lobbyId);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnLobbyDeleteImpl(nint ptr, long lobbyId, uint reason)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnLobbyDelete(lobbyId, reason);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnMemberConnectImpl(nint ptr, long lobbyId, long userId)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnMemberConnect(lobbyId, userId);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnMemberUpdateImpl(nint ptr, long lobbyId, long userId)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnMemberUpdate(lobbyId, userId);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnMemberDisconnectImpl(nint ptr, long lobbyId, long userId)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnMemberDisconnect(lobbyId, userId);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnLobbyMessageImpl(nint ptr, long lobbyId, long userId, nint dataPtr, int dataLen)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnLobbyMessage(lobbyId, userId, new((void*)dataPtr, dataLen));
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnSpeakingImpl(nint ptr, long lobbyId, long userId, bool speaking)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnSpeaking(lobbyId, userId, speaking);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnNetworkMessageImpl(nint ptr, long lobbyId, long userId, byte channelId, nint dataPtr, int dataLen)
        {
            DiscordGCHandle.Get(ptr).LobbyManagerInstance?.OnNetworkMessage(lobbyId, userId, channelId, new((void*)dataPtr, dataLen));
        }

        eventsPtr->OnLobbyUpdate = LobbyUpdateHandler.Create(&OnLobbyUpdateImpl);
        eventsPtr->OnLobbyDelete = LobbyDeleteHandler.Create(&OnLobbyDeleteImpl);
        eventsPtr->OnMemberConnect = MemberConnectHandler.Create(&OnMemberConnectImpl);
        eventsPtr->OnMemberUpdate = MemberUpdateHandler.Create(&OnMemberUpdateImpl);
        eventsPtr->OnMemberDisconnect = MemberDisconnectHandler.Create(&OnMemberDisconnectImpl);
        eventsPtr->OnLobbyMessage = LobbyMessageHandler.Create(&OnLobbyMessageImpl);
        eventsPtr->OnSpeaking = SpeakingHandler.Create(&OnSpeakingImpl);
        eventsPtr->OnNetworkMessage = NetworkMessageHandler.Create(&OnNetworkMessageImpl);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe LobbyTransaction GetLobbyCreateTransaction()
    {
        LobbyTransaction ret = default;
        MethodsPtr->GetLobbyCreateTransaction.Invoke(MethodsPtr, &ret.MethodsPtr).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe LobbyTransaction GetLobbyUpdateTransaction(long lobbyId)
    {
        LobbyTransaction ret = default;
        MethodsPtr->GetLobbyUpdateTransaction.Invoke(MethodsPtr, lobbyId, &ret.MethodsPtr).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe LobbyMemberTransaction GetMemberUpdateTransaction(long lobbyId, long userId)
    {
        LobbyMemberTransaction ret = default;
        MethodsPtr->GetMemberUpdateTransaction.Invoke(MethodsPtr, lobbyId, userId, &ret.MethodsPtr).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void CreateLobby(ref LobbyTransaction transaction, CreateLobbyHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void CreateLobbyCallbackImpl(CreateLobbyHandler ptr, Result result, Lobby* lobby)
        {
            ptr.Invoke(result, lobby);
        }

        MethodsPtr->CreateLobby.Invoke(MethodsPtr, transaction.MethodsPtr, callback, CreateLobbyCallback.Create(&CreateLobbyCallbackImpl));
        transaction.MethodsPtr = null;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void UpdateLobby(long lobbyId, ref LobbyTransaction transaction, UpdateLobbyHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void UpdateLobbyCallbackImpl(UpdateLobbyHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->UpdateLobby.Invoke(MethodsPtr, lobbyId, transaction.MethodsPtr, callback, UpdateLobbyCallback.Create(&UpdateLobbyCallbackImpl));
        transaction.MethodsPtr = null;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void DeleteLobby(long lobbyId, DeleteLobbyHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void DeleteLobbyCallbackImpl(DeleteLobbyHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->DeleteLobby.Invoke(MethodsPtr, lobbyId, callback, DeleteLobbyCallback.Create(&DeleteLobbyCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void ConnectLobby(long lobbyId, string secret, ConnectLobbyHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void ConnectLobbyCallbackImpl(ConnectLobbyHandler ptr, Result result, Lobby* lobby)
        {
            ptr.Invoke(result, lobby);
        }

        byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
        fixed (byte* pSecret = secretBytes)
        {
            MethodsPtr->ConnectLobby.Invoke(MethodsPtr, lobbyId, pSecret, callback, ConnectLobbyCallback.Create(&ConnectLobbyCallbackImpl));
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void ConnectLobbyWithActivitySecret(string activitySecret, ConnectLobbyWithActivitySecretHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void ConnectLobbyWithActivitySecretCallbackImpl(ConnectLobbyWithActivitySecretHandler ptr, Result result, Lobby* lobby)
        {
            ptr.Invoke(result, lobby);
        }

        byte[] activitySecretBytes = Encoding.UTF8.GetBytes(activitySecret);
        fixed (byte* pActivitySecret = activitySecretBytes)
        {
            MethodsPtr->ConnectLobbyWithActivitySecret.Invoke(MethodsPtr, pActivitySecret, callback, ConnectLobbyWithActivitySecretCallback.Create(&ConnectLobbyWithActivitySecretCallbackImpl));
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void DisconnectLobby(long lobbyId, DisconnectLobbyHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void DisconnectLobbyCallbackImpl(DisconnectLobbyHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->DisconnectLobby.Invoke(MethodsPtr, lobbyId, callback, DisconnectLobbyCallback.Create(&DisconnectLobbyCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe Lobby GetLobby(long lobbyId)
    {
        Lobby ret = default;
        MethodsPtr->GetLobby.Invoke(MethodsPtr, lobbyId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe string GetLobbyActivitySecret(long lobbyId)
    {
        byte[] ret = new byte[128];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetLobbyActivitySecret.Invoke(MethodsPtr, lobbyId, pRet).ThrowOnFailure();
            return Encoding.UTF8.GetString(ret);
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe string GetLobbyMetadataValue(long lobbyId, string key)
    {
        byte[] ret = new byte[4096];
        fixed (byte* pRet = ret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed (byte* pKey = keyBytes)
            {
                MethodsPtr->GetLobbyMetadataValue.Invoke(MethodsPtr, lobbyId, pKey, pRet).ThrowOnFailure();
                return Encoding.UTF8.GetString(ret);
            }
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe string GetLobbyMetadataKey(long lobbyId, int index)
    {
        byte[] ret = new byte[256];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetLobbyMetadataKey.Invoke(MethodsPtr, lobbyId, index, pRet).ThrowOnFailure();
            return Encoding.UTF8.GetString(ret);
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe int LobbyMetadataCount(long lobbyId)
    {
        int ret = default;
        MethodsPtr->LobbyMetadataCount.Invoke(MethodsPtr, lobbyId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe int MemberCount(long lobbyId)
    {
        int ret = default;
        MethodsPtr->MemberCount.Invoke(MethodsPtr, lobbyId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe long GetMemberUserId(long lobbyId, int index)
    {
        long ret = default;
        MethodsPtr->GetMemberUserId.Invoke(MethodsPtr, lobbyId, index, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe User GetMemberUser(long lobbyId, long userId)
    {
        User ret = default;
        MethodsPtr->GetMemberUser.Invoke(MethodsPtr, lobbyId, userId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe string GetMemberMetadataValue(long lobbyId, long userId, string key)
    {
        byte[] ret = new byte[4096];
        fixed (byte* pRet = ret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed (byte* pKey = keyBytes)
            {
                MethodsPtr->GetMemberMetadataValue.Invoke(MethodsPtr, lobbyId, userId, pKey, pRet).ThrowOnFailure();
                return Encoding.UTF8.GetString(ret);
            }
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe string GetMemberMetadataKey(long lobbyId, long userId, int index)
    {
        byte[] ret = new byte[256];
        fixed (byte* pRet = ret)
        {
            MethodsPtr->GetMemberMetadataKey.Invoke(MethodsPtr, lobbyId, userId, index, pRet).ThrowOnFailure();
            return Encoding.UTF8.GetString(ret);
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe int MemberMetadataCount(long lobbyId, long userId)
    {
        int ret = default;
        MethodsPtr->MemberMetadataCount.Invoke(MethodsPtr, lobbyId, userId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void UpdateMember(long lobbyId, long userId, ref LobbyMemberTransaction transaction, UpdateMemberHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void UpdateMemberCallbackImpl(UpdateMemberHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->UpdateMember.Invoke(MethodsPtr, lobbyId, userId, transaction.MethodsPtr, callback, UpdateMemberCallback.Create(&UpdateMemberCallbackImpl));
        transaction.MethodsPtr = null;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SendLobbyMessage(long lobbyId, Span<byte> data, SendLobbyMessageHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void SendLobbyMessageCallbackImpl(SendLobbyMessageHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        fixed (byte* pData = data)
        {
            MethodsPtr->SendLobbyMessage.Invoke(MethodsPtr, lobbyId, pData, data.Length, callback, SendLobbyMessageCallback.Create(&SendLobbyMessageCallbackImpl));
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe LobbySearchQuery GetSearchQuery()
    {
        LobbySearchQuery ret = default;
        MethodsPtr->GetSearchQuery.Invoke(MethodsPtr, &ret.MethodsPtr).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void Search(ref LobbySearchQuery query, SearchHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void SearchCallbackImpl(SearchHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->Search.Invoke(MethodsPtr, query.MethodsPtr, callback, SearchCallback.Create(&SearchCallbackImpl));
        query.MethodsPtr = null;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe int LobbyCount()
    {
        int ret = default;
        MethodsPtr->LobbyCount.Invoke(MethodsPtr, &ret);
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe long GetLobbyId(int index)
    {
        long ret = default;
        MethodsPtr->GetLobbyId.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void ConnectVoice(long lobbyId, ConnectVoiceHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void ConnectVoiceCallbackImpl(ConnectVoiceHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->ConnectVoice.Invoke(MethodsPtr, lobbyId, callback, ConnectVoiceCallback.Create(&ConnectVoiceCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void DisconnectVoice(long lobbyId, DisconnectVoiceHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void DisconnectVoiceCallbackImpl(DisconnectVoiceHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->DisconnectVoice.Invoke(MethodsPtr, lobbyId, callback, DisconnectVoiceCallback.Create(&DisconnectVoiceCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void ConnectNetwork(long lobbyId)
    {
        MethodsPtr->ConnectNetwork.Invoke(MethodsPtr, lobbyId).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void DisconnectNetwork(long lobbyId)
    {
        MethodsPtr->DisconnectNetwork.Invoke(MethodsPtr, lobbyId).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void FlushNetwork()
    {
        MethodsPtr->FlushNetwork.Invoke(MethodsPtr).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void OpenNetworkChannel(long lobbyId, byte channelId, bool reliable)
    {
        MethodsPtr->OpenNetworkChannel.Invoke(MethodsPtr, lobbyId, channelId, reliable).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SendNetworkMessage(long lobbyId, long userId, byte channelId, byte[] data)
    {
        fixed (byte* pData = data)
        {
            MethodsPtr->SendNetworkMessage.Invoke(MethodsPtr, lobbyId, userId, channelId, pData, data.Length).ThrowOnFailure();
        }
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnLobbyUpdate(long lobbyId)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnLobbyDelete(long lobbyId, uint reason)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnMemberConnect(long lobbyId, long userId)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnMemberUpdate(long lobbyId, long userId)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnMemberDisconnect(long lobbyId, long userId)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnLobbyMessage(long lobbyId, long userId, ReadOnlySpan<byte> data)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnSpeaking(long lobbyId, long userId, bool speaking)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnNetworkMessage(long lobbyId, long userId, byte channelId, ReadOnlySpan<byte> data)
    {
    }
}
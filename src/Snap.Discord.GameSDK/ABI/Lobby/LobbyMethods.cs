using Snap.Discord.GameSDK;

namespace ABI.Snap.Discord.GameSDK.Lobby;

internal struct LobbyMethods
{
    // Result GetLobbyCreateTransactionMethod(nint methodsPtr, ref nint transaction)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, LobbyTransactionMethods**, Result> GetLobbyCreateTransaction;

    // Result GetLobbyUpdateTransactionMethod(nint methodsPtr, long lobbyId, ref nint transaction)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, LobbyTransactionMethods**, Result> GetLobbyUpdateTransaction;

    // Result GetMemberUpdateTransactionMethod(nint methodsPtr, long lobbyId, long userId, ref nint transaction)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, long, LobbyMemberTransactionMethods**, Result> GetMemberUpdateTransaction;

    // void CreateLobbyHandler(Result result, ref Lobby lobby)
    // void CreateLobbyCallback(nint ptr, Result result, ref Lobby lobby)
    // void CreateLobbyMethod(nint methodsPtr, nint transaction, nint callbackData, CreateLobbyCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, LobbyTransactionMethods*, delegate* unmanaged[Stdcall]<Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, void> CreateLobby;

    // void UpdateLobbyHandler(Result result)
    // void UpdateLobbyCallback(nint ptr, Result result)
    // void UpdateLobbyMethod(nint methodsPtr, long lobbyId, nint transaction, nint callbackData, UpdateLobbyCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, LobbyTransactionMethods*, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> UpdateLobby;

    // void DeleteLobbyHandler(Result result)
    // void DeleteLobbyCallback(nint ptr, Result result)
    // void DeleteLobbyMethod(nint methodsPtr, long lobbyId, nint callbackData, DeleteLobbyCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> DeleteLobby;

    // void ConnectLobbyHandler(Result result, ref Lobby lobby)
    // void ConnectLobbyCallback(nint ptr, Result result, ref Lobby lobby)
    // void ConnectLobbyMethod(nint methodsPtr, long lobbyId, [MarshalAs(UnmanagedType.LPStr)] string secret, nint callbackData, ConnectLobbyCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, byte*, delegate* unmanaged[Stdcall]<Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, void> ConnectLobby;

    // void ConnectLobbyWithActivitySecretHandler(Result result, ref Lobby lobby)
    // void ConnectLobbyWithActivitySecretCallback(nint ptr, Result result, ref Lobby lobby)
    // void ConnectLobbyWithActivitySecretMethod(nint methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string activitySecret, nint callbackData, ConnectLobbyWithActivitySecretCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, byte*, delegate* unmanaged[Stdcall]<Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, Result, global::Snap.Discord.GameSDK.Lobby.Lobby*, void>, void> ConnectLobbyWithActivitySecret;

    // void DisconnectLobbyHandler(Result result)
    // void DisconnectLobbyCallback(nint ptr, Result result)
    // void DisconnectLobbyMethod(nint methodsPtr, long lobbyId, nint callbackData, DisconnectLobbyCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> DisconnectLobby;

    // Result GetLobbyMethod(nint methodsPtr, long lobbyId, ref Lobby lobby)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, global::Snap.Discord.GameSDK.Lobby.Lobby*, Result> GetLobby;

    // Result GetLobbyActivitySecretMethod(nint methodsPtr, long lobbyId, StringBuilder secret)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, byte*, Result> GetLobbyActivitySecret;

    // Result GetLobbyMetadataValueMethod(nint methodsPtr, long lobbyId, [MarshalAs(UnmanagedType.LPStr)] string key, StringBuilder value)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, byte*, byte*, Result> GetLobbyMetadataValue;

    // Result GetLobbyMetadataKeyMethod(nint methodsPtr, long lobbyId, Int32 index, StringBuilder key)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, int, byte*, Result> GetLobbyMetadataKey;

    // Result LobbyMetadataCountMethod(nint methodsPtr, long lobbyId, ref Int32 count)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, int*, Result> LobbyMetadataCount;

    // Result MemberCountMethod(nint methodsPtr, long lobbyId, ref Int32 count)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, int*, Result> MemberCount;

    // Result GetMemberUserIdMethod(nint methodsPtr, long lobbyId, Int32 index, ref long userId)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, int, long*, Result> GetMemberUserId;

    // Result GetMemberUserMethod(nint methodsPtr, long lobbyId, long userId, ref User user)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, long, User*, Result> GetMemberUser;

    // Result GetMemberMetadataValueMethod(nint methodsPtr, long lobbyId, long userId, [MarshalAs(UnmanagedType.LPStr)] string key, StringBuilder value)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, long, byte*, byte*, Result> GetMemberMetadataValue;

    // Result GetMemberMetadataKeyMethod(nint methodsPtr, long lobbyId, long userId, Int32 index, StringBuilder key)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, long, int, byte*, Result> GetMemberMetadataKey;

    // Result MemberMetadataCountMethod(nint methodsPtr, long lobbyId, long userId, ref Int32 count)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, long, int*, Result> MemberMetadataCount;

    // void UpdateMemberHandler(Result result)
    // void UpdateMemberCallback(nint ptr, Result result)
    // void UpdateMemberMethod(nint methodsPtr, long lobbyId, long userId, nint transaction, nint callbackData, UpdateMemberCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, long, LobbyMemberTransactionMethods*, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> UpdateMember;

    // void SendLobbyMessageHandler(Result result)
    // void SendLobbyMessageCallback(nint ptr, Result result)
    // void SendLobbyMessageMethod(nint methodsPtr, long lobbyId, byte[] data, Int32 dataLen, nint callbackData, SendLobbyMessageCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, byte*, int, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> SendLobbyMessage;

    // Result GetSearchQueryMethod(nint methodsPtr, ref nint query)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, LobbySearchQueryMethods**, Result> GetSearchQuery;

    // void SearchHandler(Result result)
    // void SearchCallback(nint ptr, Result result)
    // void SearchMethod(nint methodsPtr, nint query, nint callbackData, SearchCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, LobbySearchQueryMethods*, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> Search;

    // void LobbyCountMethod(nint methodsPtr, ref Int32 count)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, int*, void> LobbyCount;

    // Result GetLobbyIdMethod(nint methodsPtr, Int32 index, ref long lobbyId)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, int, long*, Result> GetLobbyId;

    // void ConnectVoiceHandler(Result result)
    // void ConnectVoiceCallback(nint ptr, Result result)
    // void ConnectVoiceMethod(nint methodsPtr, long lobbyId, nint callbackData, ConnectVoiceCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> ConnectVoice;

    // void DisconnectVoiceHandler(Result result)
    // void DisconnectVoiceCallback(nint ptr, Result result)
    // void DisconnectVoiceMethod(nint methodsPtr, long lobbyId, nint callbackData, DisconnectVoiceCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> DisconnectVoice;

    // Result ConnectNetworkMethod(nint methodsPtr, long lobbyId)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, Result> ConnectNetwork;

    // Result DisconnectNetworkMethod(nint methodsPtr, long lobbyId)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, Result> DisconnectNetwork;

    // Result FlushNetworkMethod(nint methodsPtr)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, Result> FlushNetwork;

    // Result OpenNetworkChannelMethod(nint methodsPtr, long lobbyId, byte channelId, bool reliable)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, byte, bool, Result> OpenNetworkChannel;

    // Result SendNetworkMessageMethod(nint methodsPtr, long lobbyId, long userId, byte channelId, byte[] data, Int32 dataLen)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMethods*, long, long, byte, byte*, int, Result> SendNetworkMessage;
}
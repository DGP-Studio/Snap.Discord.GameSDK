namespace ABI.Snap.Discord.GameSDK.Lobby;

internal struct LobbyEvents
{
    // void LobbyUpdateHandler(nint ptr, long lobbyId)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, void> OnLobbyUpdate;

    // void LobbyDeleteHandler(nint ptr, long lobbyId, uint reason)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, uint, void> OnLobbyDelete;

    // void MemberConnectHandler(nint ptr, long lobbyId, long userId)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, long, void> OnMemberConnect;

    // void MemberUpdateHandler(nint ptr, long lobbyId, long userId)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, long, void> OnMemberUpdate;

    // void MemberDisconnectHandler(nint ptr, long lobbyId, long userId)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, long, void> OnMemberDisconnect;

    // void LobbyMessageHandler(nint ptr, long lobbyId, long userId, nint dataPtr, int dataLen)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, long, nint, int, void> OnLobbyMessage;

    // void SpeakingHandler(nint ptr, long lobbyId, long userId, bool speaking)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, long, bool, void> OnSpeaking;

    // void NetworkMessageHandler(nint ptr, long lobbyId, long userId, byte channelId, nint dataPtr, int dataLen)
    internal unsafe delegate* unmanaged[Stdcall]<nint, long, long, byte, nint, int, void> OnNetworkMessage;
}

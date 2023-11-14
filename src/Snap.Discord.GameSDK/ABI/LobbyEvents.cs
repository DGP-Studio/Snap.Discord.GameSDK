namespace Snap.Discord.GameSDK.ABI;

internal struct LobbyEvents
{
    internal LobbyUpdateHandler OnLobbyUpdate;
    internal LobbyDeleteHandler OnLobbyDelete;
    internal MemberConnectHandler OnMemberConnect;
    internal MemberUpdateHandler OnMemberUpdate;
    internal MemberDisconnectHandler OnMemberDisconnect;
    internal LobbyMessageHandler OnLobbyMessage;
    internal SpeakingHandler OnSpeaking;
    internal NetworkMessageHandler OnNetworkMessage;
}
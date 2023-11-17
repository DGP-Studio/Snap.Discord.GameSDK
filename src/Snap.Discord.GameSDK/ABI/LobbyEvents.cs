using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct LobbyEvents
{
    [Obsolete("Deprecated by Discord")] internal LobbyUpdateHandler OnLobbyUpdate;
    [Obsolete("Deprecated by Discord")] internal LobbyDeleteHandler OnLobbyDelete;
    [Obsolete("Deprecated by Discord")] internal MemberConnectHandler OnMemberConnect;
    [Obsolete("Deprecated by Discord")] internal MemberUpdateHandler OnMemberUpdate;
    [Obsolete("Deprecated by Discord")] internal MemberDisconnectHandler OnMemberDisconnect;
    [Obsolete("Deprecated by Discord")] internal LobbyMessageHandler OnLobbyMessage;
    [Obsolete("Deprecated by Discord")] internal SpeakingHandler OnSpeaking;
    [Obsolete("Deprecated by Discord")] internal NetworkMessageHandler OnNetworkMessage;
}
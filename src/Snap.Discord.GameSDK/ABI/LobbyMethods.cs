using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct LobbyMethods
{
    [Obsolete("Deprecated by Discord")] internal GetLobbyCreateTransactionMethod GetLobbyCreateTransaction;
    [Obsolete("Deprecated by Discord")] internal GetLobbyUpdateTransactionMethod GetLobbyUpdateTransaction;
    [Obsolete("Deprecated by Discord")] internal GetMemberUpdateTransactionMethod GetMemberUpdateTransaction;
    [Obsolete("Deprecated by Discord")] internal CreateLobbyMethod CreateLobby;
    [Obsolete("Deprecated by Discord")] internal UpdateLobbyMethod UpdateLobby;
    [Obsolete("Deprecated by Discord")] internal DeleteLobbyMethod DeleteLobby;
    [Obsolete("Deprecated by Discord")] internal ConnectLobbyMethod ConnectLobby;
    [Obsolete("Deprecated by Discord")] internal ConnectLobbyWithActivitySecretMethod ConnectLobbyWithActivitySecret;
    [Obsolete("Deprecated by Discord")] internal DisconnectLobbyMethod DisconnectLobby;
    [Obsolete("Deprecated by Discord")] internal GetLobbyMethod GetLobby;
    [Obsolete("Deprecated by Discord")] internal GetLobbyActivitySecretMethod GetLobbyActivitySecret;
    [Obsolete("Deprecated by Discord")] internal GetLobbyMetadataValueMethod GetLobbyMetadataValue;
    [Obsolete("Deprecated by Discord")] internal GetLobbyMetadataKeyMethod GetLobbyMetadataKey;
    [Obsolete("Deprecated by Discord")] internal LobbyMetadataCountMethod LobbyMetadataCount;
    [Obsolete("Deprecated by Discord")] internal MemberCountMethod MemberCount;
    [Obsolete("Deprecated by Discord")] internal GetMemberUserIdMethod GetMemberUserId;
    [Obsolete("Deprecated by Discord")] internal GetMemberUserMethod GetMemberUser;
    [Obsolete("Deprecated by Discord")] internal GetMemberMetadataValueMethod GetMemberMetadataValue;
    [Obsolete("Deprecated by Discord")] internal GetMemberMetadataKeyMethod GetMemberMetadataKey;
    [Obsolete("Deprecated by Discord")] internal MemberMetadataCountMethod MemberMetadataCount;
    [Obsolete("Deprecated by Discord")] internal UpdateMemberMethod UpdateMember;
    [Obsolete("Deprecated by Discord")] internal SendLobbyMessageMethod SendLobbyMessage;
    [Obsolete("Deprecated by Discord")] internal GetSearchQueryMethod GetSearchQuery;
    [Obsolete("Deprecated by Discord")] internal SearchMethod Search;
    [Obsolete("Deprecated by Discord")] internal LobbyCountMethod LobbyCount;
    [Obsolete("Deprecated by Discord")] internal GetLobbyIdMethod GetLobbyId;
    [Obsolete("Deprecated by Discord")] internal ConnectVoiceMethod ConnectVoice;
    [Obsolete("Deprecated by Discord")] internal DisconnectVoiceMethod DisconnectVoice;
    [Obsolete("Deprecated by Discord")] internal ConnectNetworkMethod ConnectNetwork;
    [Obsolete("Deprecated by Discord")] internal DisconnectNetworkMethod DisconnectNetwork;
    [Obsolete("Deprecated by Discord")] internal FlushNetworkMethod FlushNetwork;
    [Obsolete("Deprecated by Discord")] internal OpenNetworkChannelMethod OpenNetworkChannel;
    [Obsolete("Deprecated by Discord")] internal SendNetworkMessageMethod SendNetworkMessage;
}
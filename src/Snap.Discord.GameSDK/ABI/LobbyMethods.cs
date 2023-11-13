namespace Snap.Discord.GameSDK.ABI;

public struct LobbyMethods
{
    internal GetLobbyCreateTransactionMethod GetLobbyCreateTransaction;
    internal GetLobbyUpdateTransactionMethod GetLobbyUpdateTransaction;
    internal GetMemberUpdateTransactionMethod GetMemberUpdateTransaction;
    internal CreateLobbyMethod CreateLobby;
    internal UpdateLobbyMethod UpdateLobby;
    internal DeleteLobbyMethod DeleteLobby;
    internal ConnectLobbyMethod ConnectLobby;
    internal ConnectLobbyWithActivitySecretMethod ConnectLobbyWithActivitySecret;
    internal DisconnectLobbyMethod DisconnectLobby;
    internal GetLobbyMethod GetLobby;
    internal GetLobbyActivitySecretMethod GetLobbyActivitySecret;
    internal GetLobbyMetadataValueMethod GetLobbyMetadataValue;
    internal GetLobbyMetadataKeyMethod GetLobbyMetadataKey;
    internal LobbyMetadataCountMethod LobbyMetadataCount;
    internal MemberCountMethod MemberCount;
    internal GetMemberUserIdMethod GetMemberUserId;
    internal GetMemberUserMethod GetMemberUser;
    internal GetMemberMetadataValueMethod GetMemberMetadataValue;
    internal GetMemberMetadataKeyMethod GetMemberMetadataKey;
    internal MemberMetadataCountMethod MemberMetadataCount;
    internal UpdateMemberMethod UpdateMember;
    internal SendLobbyMessageMethod SendLobbyMessage;
    internal GetSearchQueryMethod GetSearchQuery;
    internal SearchMethod Search;
    internal LobbyCountMethod LobbyCount;
    internal GetLobbyIdMethod GetLobbyId;
    internal ConnectVoiceMethod ConnectVoice;
    internal DisconnectVoiceMethod DisconnectVoice;
    internal ConnectNetworkMethod ConnectNetwork;
    internal DisconnectNetworkMethod DisconnectNetwork;
    internal FlushNetworkMethod FlushNetwork;
    internal OpenNetworkChannelMethod OpenNetworkChannel;
    internal SendNetworkMessageMethod SendNetworkMessage;
}
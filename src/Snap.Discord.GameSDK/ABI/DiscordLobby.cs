namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordLobby
{
    [NativeTypeName("DiscordLobbyId")]
    public long id;

    [NativeTypeName("enum EDiscordLobbyType")]
    public DiscordLobbyType type;

    [NativeTypeName("DiscordUserId")]
    public long owner_id;

    [NativeTypeName("DiscordLobbySecret")]
    public fixed sbyte secret[128];

    [NativeTypeName("uint32_t")]
    public uint capacity;

    [NativeTypeName("bool")]
    public byte locked;
}

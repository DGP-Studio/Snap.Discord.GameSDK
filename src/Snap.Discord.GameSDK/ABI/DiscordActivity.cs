namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordActivity
{
    [NativeTypeName("enum EDiscordActivityType")]
    public DiscordActivityType type;

    [NativeTypeName("int64_t")]
    public long application_id;

    [NativeTypeName("char[128]")]
    public fixed sbyte name[128];

    [NativeTypeName("char[128]")]
    public fixed sbyte state[128];

    [NativeTypeName("char[128]")]
    public fixed sbyte details[128];

    [NativeTypeName("struct DiscordActivityTimestamps")]
    public DiscordActivityTimestamps timestamps;

    [NativeTypeName("struct DiscordActivityAssets")]
    public DiscordActivityAssets assets;

    [NativeTypeName("struct DiscordActivityParty")]
    public DiscordActivityParty party;

    [NativeTypeName("struct DiscordActivitySecrets")]
    public DiscordActivitySecrets secrets;

    [NativeTypeName("bool")]
    public byte instance;

    [NativeTypeName("uint32_t")]
    public uint supported_platforms;
}

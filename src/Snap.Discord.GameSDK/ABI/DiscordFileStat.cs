namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordFileStat
{
    [NativeTypeName("char[260]")]
    public fixed sbyte filename[260];

    [NativeTypeName("uint64_t")]
    public ulong size;

    [NativeTypeName("uint64_t")]
    public ulong last_modified;
}

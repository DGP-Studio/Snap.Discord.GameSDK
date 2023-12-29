namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordImageManager
{
    [NativeTypeName("void (*)(struct IDiscordImageManager *, struct DiscordImageHandle, bool, void *, void (*)(void *, enum EDiscordResult, struct DiscordImageHandle))")]
    public delegate* unmanaged[Cdecl]<IDiscordImageManager*, DiscordImageHandle, byte, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, DiscordImageHandle, void>, void> fetch;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordImageManager *, struct DiscordImageHandle, struct DiscordImageDimensions *)")]
    public delegate* unmanaged[Cdecl]<IDiscordImageManager*, DiscordImageHandle, DiscordImageDimensions*, DiscordResult> get_dimensions;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordImageManager *, struct DiscordImageHandle, uint8_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordImageManager*, DiscordImageHandle, byte*, uint, DiscordResult> get_data;
}

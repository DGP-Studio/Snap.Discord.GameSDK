namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordStoreEvents
{
    [NativeTypeName("void (*)(void *, struct DiscordEntitlement *)")]
    public delegate* unmanaged[Cdecl]<void*, DiscordEntitlement*, void> on_entitlement_create;

    [NativeTypeName("void (*)(void *, struct DiscordEntitlement *)")]
    public delegate* unmanaged[Cdecl]<void*, DiscordEntitlement*, void> on_entitlement_delete;
}

namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordRelationshipEvents
{
    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> on_refresh;

    [NativeTypeName("void (*)(void *, struct DiscordRelationship *)")]
    public delegate* unmanaged[Cdecl]<void*, DiscordRelationship*, void> on_relationship_update;
}

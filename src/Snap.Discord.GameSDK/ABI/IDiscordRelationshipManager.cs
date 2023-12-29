namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordRelationshipManager
{
    [NativeTypeName("void (*)(struct IDiscordRelationshipManager *, void *, bool (*)(void *, struct DiscordRelationship *))")]
    public delegate* unmanaged[Cdecl]<IDiscordRelationshipManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordRelationship*, byte>, void> filter;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordRelationshipManager *, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordRelationshipManager*, int*, DiscordResult> count;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordRelationshipManager *, DiscordUserId, struct DiscordRelationship *)")]
    public delegate* unmanaged[Cdecl]<IDiscordRelationshipManager*, long, DiscordRelationship*, DiscordResult> get;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordRelationshipManager *, uint32_t, struct DiscordRelationship *)")]
    public delegate* unmanaged[Cdecl]<IDiscordRelationshipManager*, uint, DiscordRelationship*, DiscordResult> get_at;
}

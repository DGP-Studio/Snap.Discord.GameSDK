namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordLobbySearchQuery
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbySearchQuery *, char *, enum EDiscordLobbySearchComparison, enum EDiscordLobbySearchCast, char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbySearchQuery*, sbyte, DiscordLobbySearchComparison, DiscordLobbySearchCast, sbyte, DiscordResult> filter;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbySearchQuery *, char *, enum EDiscordLobbySearchCast, char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbySearchQuery*, sbyte, DiscordLobbySearchCast, sbyte, DiscordResult> sort;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbySearchQuery *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbySearchQuery*, uint, DiscordResult> limit;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbySearchQuery *, enum EDiscordLobbySearchDistance)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbySearchQuery*, DiscordLobbySearchDistance, DiscordResult> distance;
}

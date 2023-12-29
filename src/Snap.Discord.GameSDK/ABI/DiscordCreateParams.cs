namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordCreateParams
{
    [NativeTypeName("DiscordClientId")]
    public long client_id;

    [NativeTypeName("uint64_t")]
    public ulong flags;

    [NativeTypeName("IDiscordCoreEvents *")]
    public void** events;

    public void* event_data;

    [NativeTypeName("IDiscordApplicationEvents *")]
    public void** application_events;

    [NativeTypeName("DiscordVersion")]
    public int application_version;

    [NativeTypeName("struct IDiscordUserEvents *")]
    public IDiscordUserEvents* user_events;

    [NativeTypeName("DiscordVersion")]
    public int user_version;

    [NativeTypeName("IDiscordImageEvents *")]
    public void** image_events;

    [NativeTypeName("DiscordVersion")]
    public int image_version;

    [NativeTypeName("struct IDiscordActivityEvents *")]
    public IDiscordActivityEvents* activity_events;

    [NativeTypeName("DiscordVersion")]
    public int activity_version;

    [NativeTypeName("struct IDiscordRelationshipEvents *")]
    public IDiscordRelationshipEvents* relationship_events;

    [NativeTypeName("DiscordVersion")]
    public int relationship_version;

    [NativeTypeName("struct IDiscordLobbyEvents *")]
    public IDiscordLobbyEvents* lobby_events;

    [NativeTypeName("DiscordVersion")]
    public int lobby_version;

    [NativeTypeName("struct IDiscordNetworkEvents *")]
    public IDiscordNetworkEvents* network_events;

    [NativeTypeName("DiscordVersion")]
    public int network_version;

    [NativeTypeName("struct IDiscordOverlayEvents *")]
    public IDiscordOverlayEvents* overlay_events;

    [NativeTypeName("DiscordVersion")]
    public int overlay_version;

    [NativeTypeName("IDiscordStorageEvents *")]
    public void** storage_events;

    [NativeTypeName("DiscordVersion")]
    public int storage_version;

    [NativeTypeName("struct IDiscordStoreEvents *")]
    public IDiscordStoreEvents* store_events;

    [NativeTypeName("DiscordVersion")]
    public int store_version;

    [NativeTypeName("struct IDiscordVoiceEvents *")]
    public IDiscordVoiceEvents* voice_events;

    [NativeTypeName("DiscordVersion")]
    public int voice_version;

    [NativeTypeName("struct IDiscordAchievementEvents *")]
    public IDiscordAchievementEvents* achievement_events;

    [NativeTypeName("DiscordVersion")]
    public int achievement_version;
}

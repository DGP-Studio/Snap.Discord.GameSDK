using Snap.Discord.GameSDK.ABI;

namespace Snap.Discord.GameSDK;

internal struct DiscordCreateParams
{
    internal long ClientId;

    internal CreateFlags Flags;

    internal unsafe DiscordEvents* Events;

    internal nint EventData;

    internal unsafe ApplicationEvents* ApplicationEvents;

    internal uint ApplicationVersion;

    internal nint UserEvents;

    internal uint UserVersion;

    internal nint ImageEvents;

    internal uint ImageVersion;

    internal nint ActivityEvents;

    internal uint ActivityVersion;

    internal nint RelationshipEvents;

    internal uint RelationshipVersion;

    internal nint LobbyEvents;

    internal uint LobbyVersion;

    internal nint NetworkEvents;

    internal uint NetworkVersion;

    internal nint OverlayEvents;

    internal uint OverlayVersion;

    internal nint StorageEvents;

    internal uint StorageVersion;

    internal nint StoreEvents;

    internal uint StoreVersion;

    internal nint VoiceEvents;

    internal uint VoiceVersion;

    internal nint AchievementEvents;

    internal uint AchievementVersion;
}
using Snap.Discord.GameSDK.Core;

namespace Snap.Discord.GameSDK;

internal partial struct DiscordCreateParams
{
    internal long ClientId;

    internal CreateFlags Flags;

    internal nint Events;

    internal nint EventData;

    internal nint ApplicationEvents;

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
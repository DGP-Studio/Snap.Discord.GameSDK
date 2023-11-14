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

    internal unsafe UserEvents* UserEvents;

    internal uint UserVersion;

    internal unsafe ImageEvents* ImageEvents;

    internal uint ImageVersion;

    internal unsafe ActivityEvents* ActivityEvents;

    internal uint ActivityVersion;

    internal unsafe RelationshipEvents* RelationshipEvents;

    internal uint RelationshipVersion;

    internal unsafe LobbyEvents* LobbyEvents;

    internal uint LobbyVersion;

    internal unsafe NetworkEvents* NetworkEvents;

    internal uint NetworkVersion;

    internal unsafe OverlayEvents* OverlayEvents;

    internal uint OverlayVersion;

    internal unsafe StorageEvents* StorageEvents;

    internal uint StorageVersion;

    internal unsafe StoreEvents* StoreEvents;

    internal uint StoreVersion;

    internal unsafe VoiceEvents* VoiceEvents;

    internal uint VoiceVersion;

    internal unsafe AchievementEvents* AchievementEvents;

    internal uint AchievementVersion;
}
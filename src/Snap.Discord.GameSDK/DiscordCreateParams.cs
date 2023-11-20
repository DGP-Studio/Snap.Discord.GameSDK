using Snap.Discord.GameSDK.ABI;

namespace Snap.Discord.GameSDK;

public struct DiscordCreateParams
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

    public static unsafe DiscordCreateParams Create(long clientId, CreateFlags flags, DiscordAllEvents* AllEventsPtr, nint eventData)
    {
        DiscordCreateParams createParams = default;
        createParams.ClientId = clientId;
        createParams.Flags = flags;

        createParams.Events = &AllEventsPtr->DiscordEvents;
        createParams.EventData = eventData;

        createParams.ApplicationEvents = &AllEventsPtr->ApplicationEvents;
        createParams.ApplicationVersion = 1;

        createParams.UserEvents = &AllEventsPtr->UserEvents;
        createParams.UserVersion = 1;

        createParams.ImageEvents = &AllEventsPtr->ImageEvents;
        createParams.ImageVersion = 1;

        createParams.ActivityEvents = &AllEventsPtr->ActivityEvents;
        createParams.ActivityVersion = 1;

        createParams.RelationshipEvents = &AllEventsPtr->RelationshipEvents;
        createParams.RelationshipVersion = 1;

        createParams.LobbyEvents = &AllEventsPtr->LobbyEvents;
        createParams.LobbyVersion = 1;

        createParams.NetworkEvents = &AllEventsPtr->NetworkEvents;
        createParams.NetworkVersion = 1;

        createParams.OverlayEvents = &AllEventsPtr->OverlayEvents;
        createParams.OverlayVersion = 2;

        createParams.StorageEvents = &AllEventsPtr->StorageEvents;
        createParams.StorageVersion = 1;

        createParams.StoreEvents = &AllEventsPtr->StoreEvents;
        createParams.StoreVersion = 1;

        createParams.VoiceEvents = &AllEventsPtr->VoiceEvents;
        createParams.VoiceVersion = 1;

        createParams.AchievementEvents = &AllEventsPtr->AchievementEvents;
        createParams.AchievementVersion = 1;

        return createParams;
    }
}
using Snap.Discord.GameSDK.ABI;

namespace Snap.Discord.GameSDK;

public struct DiscordAllEvents
{
    internal DiscordEvents DiscordEvents;
    internal ApplicationEvents ApplicationEvents;
    internal UserEvents UserEvents;
    internal ImageEvents ImageEvents;
    internal ActivityEvents ActivityEvents;
    internal RelationshipEvents RelationshipEvents;
    internal LobbyEvents LobbyEvents;
    internal NetworkEvents NetworkEvents;
    internal OverlayEvents OverlayEvents;
    internal StorageEvents StorageEvents;
    internal StoreEvents StoreEvents;
    internal VoiceEvents VoiceEvents;
    internal AchievementEvents AchievementEvents;
}
using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct NetworkEvents
{
    [Obsolete("Deprecated by Discord")] internal MessageHandler OnMessage;
    [Obsolete("Deprecated by Discord")] internal RouteUpdateHandler OnRouteUpdate;
}
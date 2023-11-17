using System;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public struct Lobby
{
    [Obsolete("Deprecated by Discord")] public long Id;
    [Obsolete("Deprecated by Discord")] public LobbyType Type;
    [Obsolete("Deprecated by Discord")] public long OwnerId;
    [Obsolete("Deprecated by Discord")] public unsafe fixed byte Secret[128];
    [Obsolete("Deprecated by Discord")] public uint Capacity;
    [Obsolete("Deprecated by Discord")] public bool Locked;
}
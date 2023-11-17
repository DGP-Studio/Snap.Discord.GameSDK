using System;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public struct FileStat
{
    [Obsolete("Deprecated by Discord")] public unsafe fixed byte Filename[260];
    [Obsolete("Deprecated by Discord")] public ulong Size;
    [Obsolete("Deprecated by Discord")] public ulong LastModified;
}
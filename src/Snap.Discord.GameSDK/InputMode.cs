using System;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public struct InputMode
{
    [Obsolete("Deprecated by Discord")] public InputModeType Type;
    [Obsolete("Deprecated by Discord")] public unsafe fixed byte Shortcut[256];
}
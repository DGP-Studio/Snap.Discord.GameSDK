using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct VoiceMethods
{
    [Obsolete("Deprecated by Discord")] internal GetInputModeMethod GetInputMode;
    [Obsolete("Deprecated by Discord")] internal SetInputModeMethod SetInputMode;
    [Obsolete("Deprecated by Discord")] internal IsSelfMuteMethod IsSelfMute;
    [Obsolete("Deprecated by Discord")] internal SetSelfMuteMethod SetSelfMute;
    [Obsolete("Deprecated by Discord")] internal IsSelfDeafMethod IsSelfDeaf;
    [Obsolete("Deprecated by Discord")] internal SetSelfDeafMethod SetSelfDeaf;
    [Obsolete("Deprecated by Discord")] internal IsLocalMuteMethod IsLocalMute;
    [Obsolete("Deprecated by Discord")] internal SetLocalMuteMethod SetLocalMute;
    [Obsolete("Deprecated by Discord")] internal GetLocalVolumeMethod GetLocalVolume;
    [Obsolete("Deprecated by Discord")] internal SetLocalVolumeMethod SetLocalVolume;
}
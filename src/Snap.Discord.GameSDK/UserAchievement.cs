using System;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public struct UserAchievement
{
    [Obsolete("Deprecated by Discord")] public long UserId;
    [Obsolete("Deprecated by Discord")] public long AchievementId;
    [Obsolete("Deprecated by Discord")] public byte PercentComplete;
    [Obsolete("Deprecated by Discord")] public unsafe fixed byte UnlockedAt[64];
}
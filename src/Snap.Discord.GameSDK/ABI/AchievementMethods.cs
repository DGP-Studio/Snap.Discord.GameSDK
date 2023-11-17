using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct AchievementMethods
{
    [Obsolete("Deprecated by Discord")] internal SetUserAchievementMethod SetUserAchievement;
    [Obsolete("Deprecated by Discord")] internal FetchUserAchievementsMethod FetchUserAchievements;
    [Obsolete("Deprecated by Discord")] internal CountUserAchievementsMethod CountUserAchievements;
    [Obsolete("Deprecated by Discord")] internal GetUserAchievementMethod GetUserAchievement;
    [Obsolete("Deprecated by Discord")] internal GetUserAchievementAtMethod GetUserAchievementAt;
}
using Snap.Discord.GameSDK.Achievement;

namespace ABI.Snap.Discord.GameSDK.Achievement;

internal struct AchievementEvents
{
    // void UserAchievementUpdateHandler(nint ptr, ref UserAchievement userAchievement)
    internal unsafe delegate* unmanaged[Stdcall]<nint, UserAchievement*, void> OnUserAchievementUpdate;
}
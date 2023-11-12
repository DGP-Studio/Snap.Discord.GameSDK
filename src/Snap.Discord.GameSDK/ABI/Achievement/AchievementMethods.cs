using Snap.Discord.GameSDK;
using Snap.Discord.GameSDK.Achievement;

namespace ABI.Snap.Discord.GameSDK.Achievement;

internal struct AchievementMethods
{
    // void SetUserAchievementHandler(Result result)
    // void SetUserAchievementCallback(nint ptr, Result result)
    // void SetUserAchievementMethod(nint methodsPtr, Int64 achievementId, byte percentComplete, nint callbackData, SetUserAchievementCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<AchievementMethods*, long, byte, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> SetUserAchievement;

    // void FetchUserAchievementsHandler(Result result)
    // void FetchUserAchievementsCallback(nint ptr, Result result)
    // void FetchUserAchievementsMethod(nint methodsPtr, nint callbackData, FetchUserAchievementsCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<AchievementMethods*, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> FetchUserAchievements;

    // void CountUserAchievementsMethod(nint methodsPtr, ref Int32 count)
    internal unsafe delegate* unmanaged[Stdcall]<AchievementMethods*, int*, void> CountUserAchievements;

    // Result GetUserAchievementMethod(nint methodsPtr, Int64 userAchievementId, ref UserAchievement userAchievement)
    internal unsafe delegate* unmanaged[Stdcall]<AchievementMethods*, long, UserAchievement*, Result> GetUserAchievement;

    // Result GetUserAchievementAtMethod(nint methodsPtr, Int32 index, ref UserAchievement userAchievement)
    internal unsafe delegate* unmanaged[Stdcall]<AchievementMethods*, int, UserAchievement*, Result> GetUserAchievementAt;
}
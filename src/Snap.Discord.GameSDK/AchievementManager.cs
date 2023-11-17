using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public class AchievementManager
{
    private unsafe readonly AchievementMethods* MethodsPtr;

    internal unsafe AchievementManager(AchievementMethods* ptr, AchievementEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(AchievementEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnUserAchievementUpdateImpl(nint ptr, UserAchievement* userAchievement)
        {
            DiscordGCHandle.Get(ptr).AchievementManagerInstance?.OnUserAchievementUpdate(ref *userAchievement);
        }

        eventsPtr->OnUserAchievementUpdate = UserAchievementUpdateHandler.Create(&OnUserAchievementUpdateImpl);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetUserAchievement(long achievementId, byte percentComplete, SetUserAchievementHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void SetUserAchievementCallbackImpl(SetUserAchievementHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->SetUserAchievement.Invoke(MethodsPtr, achievementId, percentComplete, callback, SetUserAchievementCallback.Create(&SetUserAchievementCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void FetchUserAchievements(FetchUserAchievementsHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void FetchUserAchievementsCallbackImpl(FetchUserAchievementsHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->FetchUserAchievements.Invoke(MethodsPtr, callback, FetchUserAchievementsCallback.Create(&FetchUserAchievementsCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe int CountUserAchievements()
    {
        int ret = default;
        MethodsPtr->CountUserAchievements.Invoke(MethodsPtr, &ret);
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe UserAchievement GetUserAchievement(long userAchievementId)
    {
        UserAchievement ret = default;
        MethodsPtr->GetUserAchievement.Invoke(MethodsPtr, userAchievementId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe UserAchievement GetUserAchievementAt(int index)
    {
        UserAchievement ret = default;
        MethodsPtr->GetUserAchievementAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnUserAchievementUpdate(ref UserAchievement userAchievement)
    {
    }
}
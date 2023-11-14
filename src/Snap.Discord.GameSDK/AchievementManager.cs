using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class AchievementManager
{
    private unsafe readonly AchievementMethods* MethodsPtr;

    internal unsafe AchievementManager(AchievementMethods* ptr, AchievementEvents* eventsPtr, ref AchievementEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(AchievementEvents* eventsPtr, ref AchievementEvents events)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnUserAchievementUpdateImpl(nint ptr, UserAchievement* userAchievement)
        {
            DiscordGCHandle.Get(ptr).AchievementManagerInstance.OnUserAchievementUpdate(ref *userAchievement);
        }

        events.OnUserAchievementUpdate = UserAchievementUpdateHandler.Create(&OnUserAchievementUpdateImpl);
        *eventsPtr = events;
    }

    public unsafe void SetUserAchievement(long achievementId, byte percentComplete, SetUserAchievementHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void SetUserAchievementCallbackImpl(SetUserAchievementHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->SetUserAchievement.Invoke(MethodsPtr, achievementId, percentComplete, callback, SetUserAchievementCallback.Create(&SetUserAchievementCallbackImpl));
    }

    public unsafe void FetchUserAchievements(FetchUserAchievementsHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void FetchUserAchievementsCallbackImpl(FetchUserAchievementsHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->FetchUserAchievements.Invoke(MethodsPtr, callback, FetchUserAchievementsCallback.Create(&FetchUserAchievementsCallbackImpl));
    }

    public unsafe int CountUserAchievements()
    {
        int ret = default;
        MethodsPtr->CountUserAchievements.Invoke(MethodsPtr, &ret);
        return ret;
    }

    public unsafe UserAchievement GetUserAchievement(long userAchievementId)
    {
        UserAchievement ret = default;
        MethodsPtr->GetUserAchievement.Invoke(MethodsPtr, userAchievementId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe UserAchievement GetUserAchievementAt(int index)
    {
        UserAchievement ret = default;
        MethodsPtr->GetUserAchievementAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    protected virtual void OnUserAchievementUpdate(ref UserAchievement userAchievement)
    {
    }
}
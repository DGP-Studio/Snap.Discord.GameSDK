using ABI.Snap.Discord.GameSDK.Achievement;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK.Achievement;

public class AchievementManager
{
    private unsafe readonly AchievementMethods* MethodsPtr;

    public event DiscordAction<UserAchievement>? OnUserAchievementUpdate;

    internal unsafe AchievementManager(AchievementMethods* ptr, nint eventsPtr, ref AchievementEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(nint eventsPtr, ref AchievementEvents events)
    {
        events.OnUserAchievementUpdate = &OnUserAchievementUpdateImpl;
        *(AchievementEvents*)eventsPtr = events;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void SetUserAchievementCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void SetUserAchievement(long achievementId, byte percentComplete, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->SetUserAchievement(MethodsPtr, achievementId, percentComplete, callback, &SetUserAchievementCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void FetchUserAchievementsCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void FetchUserAchievements(delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->FetchUserAchievements(MethodsPtr, callback, &FetchUserAchievementsCallbackImpl);
    }

    public unsafe int CountUserAchievements()
    {
        int ret = default;
        MethodsPtr->CountUserAchievements(MethodsPtr, &ret);
        return ret;
    }

    public unsafe UserAchievement GetUserAchievement(long userAchievementId)
    {
        UserAchievement ret = default;
        MethodsPtr->GetUserAchievement(MethodsPtr, userAchievementId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe UserAchievement GetUserAchievementAt(int index)
    {
        UserAchievement ret = default;
        MethodsPtr->GetUserAchievementAt(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnUserAchievementUpdateImpl(nint ptr, UserAchievement* userAchievement)
    {
        Discord d = DiscordGCHandle.Get(ptr);
        d.AchievementManagerInstance.OnUserAchievementUpdate?.Invoke(ref *userAchievement);
    }
}
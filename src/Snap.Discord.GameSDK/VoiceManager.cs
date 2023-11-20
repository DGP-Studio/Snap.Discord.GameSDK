using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public class VoiceManager
{
    private unsafe readonly VoiceMethods* MethodsPtr;

    internal unsafe VoiceManager(VoiceMethods* ptr, VoiceEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(VoiceEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnSettingsUpdateImpl(nint ptr)
        {
            DiscordGCHandle.Get(ptr).VoiceManagerInstance?.OnSettingsUpdate();
        }

        eventsPtr->OnSettingsUpdate = SettingsUpdateHandler.Create(&OnSettingsUpdateImpl);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe InputMode GetInputMode()
    {
        InputMode ret = default;
        MethodsPtr->GetInputMode.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetInputMode(InputMode inputMode, SetInputModeHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void SetInputModeCallbackImpl(SetInputModeHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->SetInputMode.Invoke(MethodsPtr, inputMode, callback, SetInputModeCallback.Create(&SetInputModeCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe bool IsSelfMute()
    {
        bool ret = default;
        MethodsPtr->IsSelfMute.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetSelfMute(bool mute)
    {
        MethodsPtr->SetSelfMute.Invoke(MethodsPtr, mute).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe bool IsSelfDeaf()
    {
        bool ret = default;
        MethodsPtr->IsSelfDeaf.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetSelfDeaf(bool deaf)
    {
        MethodsPtr->SetSelfDeaf.Invoke(MethodsPtr, deaf).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe bool IsLocalMute(long userId)
    {
        bool ret = default;
        MethodsPtr->IsLocalMute.Invoke(MethodsPtr, userId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetLocalMute(long userId, bool mute)
    {
        MethodsPtr->SetLocalMute.Invoke(MethodsPtr, userId, mute).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe byte GetLocalVolume(long userId)
    {
        byte ret = default;
        MethodsPtr->GetLocalVolume.Invoke(MethodsPtr, userId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetLocalVolume(long userId, byte volume)
    {
        MethodsPtr->SetLocalVolume.Invoke(MethodsPtr, userId, volume).ThrowOnFailure();
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnSettingsUpdate()
    {
    }
}
using ABI.Snap.Discord.GameSDK.Activity;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK.Activity;

public class ActivityManager
{
    private unsafe readonly ActivityMethods* MethodsPtr;

    public event Action<string>? OnActivityJoin;

    public event Action<string>? OnActivitySpectate;

    public event DiscordAction<User>? OnActivityJoinRequest;

    public event DiscordAction<ActivityActionType, User, Activity>? OnActivityInvite;

    internal unsafe ActivityManager(ActivityMethods* ptr, nint eventsPtr, ref ActivityEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(nint eventsPtr, ref ActivityEvents events)
    {
        events.OnActivityJoin = &OnActivityJoinImpl;
        events.OnActivitySpectate = &OnActivitySpectateImpl;
        events.OnActivityJoinRequest = &OnActivityJoinRequestImpl;
        events.OnActivityInvite = &OnActivityInviteImpl;
        *(ActivityEvents*)eventsPtr = events;
    }

    public unsafe void RegisterCommand(string command)
    {
        byte[] commandBytes = Encoding.UTF8.GetBytes(command);
        fixed (byte* pCommand = commandBytes)
        {
            MethodsPtr->RegisterCommand(MethodsPtr, pCommand).ThrowOnFailure();
        }
    }

    public unsafe void RegisterSteam(uint steamId)
    {
        MethodsPtr->RegisterSteam(MethodsPtr, steamId).ThrowOnFailure();
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void UpdateActivityCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void UpdateActivity(Activity activity, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->UpdateActivity(MethodsPtr, &activity, callback, &UpdateActivityCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void ClearActivityCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void ClearActivity(delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->ClearActivity(MethodsPtr, callback, &ClearActivityCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void SendRequestReplyCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void SendRequestReply(long userId, ActivityJoinRequestReply reply, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->SendRequestReply(MethodsPtr, userId, reply, callback, &SendRequestReplyCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void SendInviteCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void SendInvite(long userId, ActivityActionType type, string content, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        byte[] contentBytes = Encoding.UTF8.GetBytes(content);
        fixed (byte* pContent = contentBytes)
        {
            MethodsPtr->SendInvite(MethodsPtr, userId, type, pContent, callback, &SendInviteCallbackImpl);
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void AcceptInviteCallbackImpl(delegate* unmanaged[Stdcall]<Result, void> ptr, Result result)
    {
        ptr(result);
    }

    public unsafe void AcceptInvite(long userId, delegate* unmanaged[Stdcall]<Result, void> callback)
    {
        MethodsPtr->AcceptInvite(MethodsPtr, userId, callback, &AcceptInviteCallbackImpl);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivityJoinImpl(nint ptr, byte* secret)
    {
        ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
        string secretString = Encoding.UTF8.GetString(bytes);

        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivityJoin?.Invoke(secretString);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivitySpectateImpl(nint ptr, byte* secret)
    {
        ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
        string secretString = Encoding.UTF8.GetString(bytes);

        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivitySpectate?.Invoke(secretString);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivityJoinRequestImpl(nint ptr, User* user)
    {
        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivityJoinRequest?.Invoke(ref *user);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivityInviteImpl(nint ptr, ActivityActionType type, User* user, Activity* activity)
    {
        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivityInvite?.Invoke(type, ref *user, ref *activity);
    }
}
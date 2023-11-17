using Snap.Discord.GameSDK.ABI;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Snap.Discord.GameSDK;

/// <summary>
/// The overlay is only supported on Windows for DirectX or OpenGL games. Linux, Mac, and games using Vulkan are not supported.
/// <para>
/// Discord comes with an awesome built-in overlay, and you may want to make use of it for your game. This manager will help you do just that! It:
/// </para>
/// <para>Gives you the current state of the overlay for the user: Locked, enabled, unlocked, open, closed, etc.</para>
/// <para>Allows you to change that state</para>
/// </summary>
public partial class OverlayManager
{
    private unsafe readonly OverlayMethods* MethodsPtr;
    private static readonly ConcurrentDictionary<string, ManualResetEventSlim> callbackWaits = [];
    private static readonly ConcurrentDictionary<string, Result> callbackResults = [];

    public unsafe OverlayManager(OverlayMethods* ptr, OverlayEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(OverlayEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnToggleImpl(nint ptr, bool locked)
        {
            DiscordGCHandle.Get(ptr).OverlayManagerInstance?.OnToggle(locked);
        }

        eventsPtr->OnToggle = ToggleHandler.Create(&OnToggleImpl);
    }

    /// <summary>
    /// Check whether the user has the overlay enabled or disabled. If the overlay is disabled, all the functionality in this manager will still work. The calls will instead focus the Discord client and show the modal there instead.
    /// </summary>
    /// <returns></returns>
    public unsafe bool IsEnabled()
    {
        bool ret = default;
        MethodsPtr->IsEnabled.Invoke(MethodsPtr, &ret);
        return ret;
    }

    /// <summary>
    /// Check if the overlay is currently locked or unlocked
    /// </summary>
    /// <returns></returns>
    public unsafe bool IsLocked()
    {
        bool ret = default;
        MethodsPtr->IsLocked.Invoke(MethodsPtr, &ret);
        return ret;
    }

    /// <summary>
    /// Locks or unlocks input in the overlay. Calling SetLocked(true); will also close any modals in the overlay or in-app from things like IAP purchase flows and disallow input.
    /// </summary>
    /// <param name="locked">lock or unlock the overlay</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void SetLocked(bool locked, SetLockedHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void SetLockedCallbackImpl(SetLockedHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->SetLocked.Invoke(MethodsPtr, locked, callback, SetLockedCallback.Create(&SetLockedCallbackImpl));
    }

    /// <summary>
    /// Opens the overlay modal for sending game invitations to users, channels, and servers. If you do not have a valid activity with all the required fields, this call will error. See <see href="https://discord.com/developers/docs/game-sdk/activities#activity-action-field-requirements">Activity Action Field Requirements</see> for the fields required to have join and spectate invites function properly.
    /// </summary>
    /// <param name="type">what type of invite to send</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void OpenActivityInvite(ActivityActionType type, OpenActivityInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OpenActivityInviteCallbackImpl(OpenActivityInviteHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        GCHandle wrapped = GCHandle.Alloc(callback);
        MethodsPtr->OpenActivityInvite.Invoke(MethodsPtr, type, callback, OpenActivityInviteCallback.Create(&OpenActivityInviteCallbackImpl));
    }

    /// <summary>
    /// Opens the overlay modal for joining a Discord guild, given its invite code. An invite code for a server may look something like fortnite for a verified server—the full invite being
    /// <code>discord.gg/fortnite</code>
    /// —or something like rjEeUJq for a non-verified server, the full invite being
    /// <code>discord.gg/rjEeUJq</code>
    /// <para>
    /// Returns a Discord.Result via callback. Note that a successful Discord.Result response does not necessarily mean that the user has joined the guild. If you want more granular control over and knowledge about users joining your guild, you may want to look into implementing the
    /// <see href="https://discord.com/developers/docs/topics/oauth2#authorization-code-grant">guilds.join OAuth2 scope in an authorization code grant</see> in conjunction with the
    /// <see href="https://discord.com/developers/docs/resources/guild#add-guild-member">Add Guild Members endpoint.</see>
    /// </para>
    /// </summary>
    /// <param name="code">an invite code for a guild</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void OpenGuildInvite(string code, OpenGuildInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OpenGuildInviteCallbackImpl(OpenGuildInviteHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        byte[] codeBytes = Encoding.UTF8.GetBytes(code);
        fixed (byte* pCode = codeBytes)
        {
            MethodsPtr->OpenGuildInvite.Invoke(MethodsPtr, pCode, callback, OpenGuildInviteCallback.Create(&OpenGuildInviteCallbackImpl));
        }
    }

    /// <summary>
    /// <para>
    /// Opens the overlay widget for voice settings for the currently connected application. These settings are unique to each user within the context of your application. That means that a user can have different favorite voice settings for each of their games!
    /// </para>
    /// <para>
    /// Also, when connected to a lobby's voice channel, the overlay will show a widget that allows users to locally mute, deafen, and adjust the volume of others.
    /// </para>
    /// </summary>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void OpenVoiceSettings(OpenVoiceSettingsHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OpenVoiceSettingsCallbackImpl(OpenVoiceSettingsHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->OpenVoiceSettings.Invoke(MethodsPtr, callback, OpenVoiceSettingsCallback.Create(&OpenVoiceSettingsCallbackImpl));
    }

    public unsafe void InitDrawingDxgi(nint swapchain, bool useMessageForwarding)
    {
        MethodsPtr->InitDrawingDxgi.Invoke(MethodsPtr, swapchain, useMessageForwarding).ThrowOnFailure();
    }

    public unsafe void OnPresent()
    {
        MethodsPtr->OnPresent.Invoke(MethodsPtr);
    }

    public unsafe void ForwardMessage(nint message)
    {
        MethodsPtr->ForwardMessage.Invoke(MethodsPtr, message);
    }

    public unsafe void KeyEvent(bool down, string keyCode, KeyVariant variant)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(keyCode);
        fixed (byte* pKeyCode = bytes)
        {
            MethodsPtr->KeyEvent.Invoke(MethodsPtr, down, pKeyCode, variant);
        }
    }

    public unsafe void CharEvent(string character)
    {
        byte[] characterBytes = Encoding.UTF8.GetBytes(character);
        fixed (byte* pCharacter = characterBytes)
        {
            MethodsPtr->CharEvent.Invoke(MethodsPtr, pCharacter);
        }
    }

    public unsafe void MouseButtonEvent(byte down, int clickCount, MouseButton which, int x, int y)
    {
        MethodsPtr->MouseButtonEvent.Invoke(MethodsPtr, down, clickCount, which, x, y);
    }

    public unsafe void MouseMotionEvent(int x, int y)
    {
        MethodsPtr->MouseMotionEvent.Invoke(MethodsPtr, x, y);
    }

    public unsafe void ImeCommitText(string text)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        fixed (byte* pText = textBytes)
        {
            MethodsPtr->ImeCommitText.Invoke(MethodsPtr, pText);
        }
    }

    public unsafe void ImeSetComposition(string text, ImeUnderline underlines, int from, int to)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        fixed (byte* pText = textBytes)
        {
            MethodsPtr->ImeSetComposition.Invoke(MethodsPtr, pText, &underlines, from, to);
        }
    }

    public unsafe void ImeCancelComposition()
    {
        MethodsPtr->ImeCancelComposition.Invoke(MethodsPtr);
    }

    public unsafe void SetImeCompositionRangeCallback(SetImeCompositionRangeCallbackHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void SetImeCompositionRangeCallbackCallbackImpl(SetImeCompositionRangeCallbackHandler ptr, int from, int to, Rect* bounds)
        {
            ptr.Invoke(from, to, bounds);
        }

        MethodsPtr->SetImeCompositionRangeCallback.Invoke(MethodsPtr, callback, SetImeCompositionRangeCallbackCallback.Create(&SetImeCompositionRangeCallbackCallbackImpl));
    }

    public unsafe void SetImeSelectionBoundsCallback(SetImeSelectionBoundsCallbackHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void SetImeSelectionBoundsCallbackCallbackImpl(SetImeSelectionBoundsCallbackHandler ptr, Rect anchor, Rect focus, bool isAnchorFirst)
        {
            ptr.Invoke(anchor, focus, isAnchorFirst);
        }

        MethodsPtr->SetImeSelectionBoundsCallback.Invoke(MethodsPtr, callback, SetImeSelectionBoundsCallbackCallback.Create(&SetImeSelectionBoundsCallbackCallbackImpl));
    }

    public unsafe bool IsPointInsideClickZone(int x, int y)
    {
        return MethodsPtr->IsPointInsideClickZone.Invoke(MethodsPtr, x, y);
    }

    /// <summary>
    /// Fires when the overlay is locked or unlocked (a.k.a. opened or closed)
    /// </summary>
    /// <param name="locked"></param>
    protected virtual void OnToggle(bool locked)
    {
    }
}
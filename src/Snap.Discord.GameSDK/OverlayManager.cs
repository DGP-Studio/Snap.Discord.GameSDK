using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public partial class OverlayManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIEvents
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ToggleHandler(IntPtr ptr, bool locked);

        internal ToggleHandler OnToggle;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void IsEnabledMethod(IntPtr methodsPtr, ref bool enabled);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void IsLockedMethod(IntPtr methodsPtr, ref bool locked);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetLockedCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetLockedMethod(IntPtr methodsPtr, bool locked, IntPtr callbackData, SetLockedCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void OpenActivityInviteCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void OpenActivityInviteMethod(IntPtr methodsPtr, ActivityActionType type, IntPtr callbackData, OpenActivityInviteCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void OpenGuildInviteCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void OpenGuildInviteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string code, IntPtr callbackData, OpenGuildInviteCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void OpenVoiceSettingsCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void OpenVoiceSettingsMethod(IntPtr methodsPtr, IntPtr callbackData, OpenVoiceSettingsCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result InitDrawingDxgiMethod(IntPtr methodsPtr, IntPtr swapchain, bool useMessageForwarding);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void OnPresentMethod(IntPtr methodsPtr);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ForwardMessageMethod(IntPtr methodsPtr, IntPtr message);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void KeyEventMethod(IntPtr methodsPtr, bool down, [MarshalAs(UnmanagedType.LPStr)] string keyCode, KeyVariant variant);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void CharEventMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string character);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void MouseButtonEventMethod(IntPtr methodsPtr, byte down, Int32 clickCount, MouseButton which, Int32 x, Int32 y);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void MouseMotionEventMethod(IntPtr methodsPtr, Int32 x, Int32 y);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ImeCommitTextMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string text);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ImeSetCompositionMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string text, ref ImeUnderline underlines, Int32 from, Int32 to);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ImeCancelCompositionMethod(IntPtr methodsPtr);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetImeCompositionRangeCallbackCallback(IntPtr ptr, Int32 from, Int32 to, ref Rect bounds);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetImeCompositionRangeCallbackMethod(IntPtr methodsPtr, IntPtr callbackData, SetImeCompositionRangeCallbackCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetImeSelectionBoundsCallbackCallback(IntPtr ptr, Rect anchor, Rect focus, bool isAnchorFirst);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetImeSelectionBoundsCallbackMethod(IntPtr methodsPtr, IntPtr callbackData, SetImeSelectionBoundsCallbackCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate bool IsPointInsideClickZoneMethod(IntPtr methodsPtr, Int32 x, Int32 y);

        internal IsEnabledMethod IsEnabled;

        internal IsLockedMethod IsLocked;

        internal SetLockedMethod SetLocked;

        internal OpenActivityInviteMethod OpenActivityInvite;

        internal OpenGuildInviteMethod OpenGuildInvite;

        internal OpenVoiceSettingsMethod OpenVoiceSettings;

        internal InitDrawingDxgiMethod InitDrawingDxgi;

        internal OnPresentMethod OnPresent;

        internal ForwardMessageMethod ForwardMessage;

        internal KeyEventMethod KeyEvent;

        internal CharEventMethod CharEvent;

        internal MouseButtonEventMethod MouseButtonEvent;

        internal MouseMotionEventMethod MouseMotionEvent;

        internal ImeCommitTextMethod ImeCommitText;

        internal ImeSetCompositionMethod ImeSetComposition;

        internal ImeCancelCompositionMethod ImeCancelComposition;

        internal SetImeCompositionRangeCallbackMethod SetImeCompositionRangeCallback;

        internal SetImeSelectionBoundsCallbackMethod SetImeSelectionBoundsCallback;

        internal IsPointInsideClickZoneMethod IsPointInsideClickZone;
    }

    public delegate void SetLockedHandler(Result result);

    public delegate void OpenActivityInviteHandler(Result result);

    public delegate void OpenGuildInviteHandler(Result result);

    public delegate void OpenVoiceSettingsHandler(Result result);

    public delegate void SetImeCompositionRangeCallbackHandler(Int32 from, Int32 to, ref Rect bounds);

    public delegate void SetImeSelectionBoundsCallbackHandler(Rect anchor, Rect focus, bool isAnchorFirst);

    public delegate void ToggleHandler(bool locked);

    private IntPtr MethodsPtr;

    private Object MethodsStructure;

    private FFIMethods Methods
    {
        get
        {
            if (MethodsStructure == null)
            {
                MethodsStructure = Marshal.PtrToStructure(MethodsPtr, typeof(FFIMethods));
            }
            return (FFIMethods)MethodsStructure;
        }

    }

    public event ToggleHandler OnToggle;

    internal OverlayManager(IntPtr ptr, IntPtr eventsPtr, ref FFIEvents events)
    {
        if (eventsPtr == IntPtr.Zero)
        {
            throw new ResultException(Result.InternalError);
        }
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
        if (MethodsPtr == IntPtr.Zero)
        {
            throw new ResultException(Result.InternalError);
        }
    }

    private void InitEvents(IntPtr eventsPtr, ref FFIEvents events)
    {
        events.OnToggle = OnToggleImpl;
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    public bool IsEnabled()
    {
        var ret = new bool();
        Methods.IsEnabled(MethodsPtr, ref ret);
        return ret;
    }

    public bool IsLocked()
    {
        var ret = new bool();
        Methods.IsLocked(MethodsPtr, ref ret);
        return ret;
    }

    [MonoPInvokeCallback]
    private static void SetLockedCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        SetLockedHandler callback = (SetLockedHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void SetLocked(bool locked, SetLockedHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.SetLocked(MethodsPtr, locked, GCHandle.ToIntPtr(wrapped), SetLockedCallbackImpl);
    }

    [MonoPInvokeCallback]
    private static void OpenActivityInviteCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        OpenActivityInviteHandler callback = (OpenActivityInviteHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void OpenActivityInvite(ActivityActionType type, OpenActivityInviteHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.OpenActivityInvite(MethodsPtr, type, GCHandle.ToIntPtr(wrapped), OpenActivityInviteCallbackImpl);
    }

    [MonoPInvokeCallback]
    private static void OpenGuildInviteCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        OpenGuildInviteHandler callback = (OpenGuildInviteHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void OpenGuildInvite(string code, OpenGuildInviteHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.OpenGuildInvite(MethodsPtr, code, GCHandle.ToIntPtr(wrapped), OpenGuildInviteCallbackImpl);
    }

    [MonoPInvokeCallback]
    private static void OpenVoiceSettingsCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        OpenVoiceSettingsHandler callback = (OpenVoiceSettingsHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void OpenVoiceSettings(OpenVoiceSettingsHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.OpenVoiceSettings(MethodsPtr, GCHandle.ToIntPtr(wrapped), OpenVoiceSettingsCallbackImpl);
    }

    public void InitDrawingDxgi(IntPtr swapchain, bool useMessageForwarding)
    {
        var res = Methods.InitDrawingDxgi(MethodsPtr, swapchain, useMessageForwarding);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    public void OnPresent()
    {
        Methods.OnPresent(MethodsPtr);
    }

    public void ForwardMessage(IntPtr message)
    {
        Methods.ForwardMessage(MethodsPtr, message);
    }

    public void KeyEvent(bool down, string keyCode, KeyVariant variant)
    {
        Methods.KeyEvent(MethodsPtr, down, keyCode, variant);
    }

    public void CharEvent(string character)
    {
        Methods.CharEvent(MethodsPtr, character);
    }

    public void MouseButtonEvent(byte down, Int32 clickCount, MouseButton which, Int32 x, Int32 y)
    {
        Methods.MouseButtonEvent(MethodsPtr, down, clickCount, which, x, y);
    }

    public void MouseMotionEvent(Int32 x, Int32 y)
    {
        Methods.MouseMotionEvent(MethodsPtr, x, y);
    }

    public void ImeCommitText(string text)
    {
        Methods.ImeCommitText(MethodsPtr, text);
    }

    public void ImeSetComposition(string text, ImeUnderline underlines, Int32 from, Int32 to)
    {
        Methods.ImeSetComposition(MethodsPtr, text, ref underlines, from, to);
    }

    public void ImeCancelComposition()
    {
        Methods.ImeCancelComposition(MethodsPtr);
    }

    [MonoPInvokeCallback]
    private static void SetImeCompositionRangeCallbackCallbackImpl(IntPtr ptr, Int32 from, Int32 to, ref Rect bounds)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        SetImeCompositionRangeCallbackHandler callback = (SetImeCompositionRangeCallbackHandler)h.Target;
        h.Free();
        callback(from, to, ref bounds);
    }

    public void SetImeCompositionRangeCallback(SetImeCompositionRangeCallbackHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.SetImeCompositionRangeCallback(MethodsPtr, GCHandle.ToIntPtr(wrapped), SetImeCompositionRangeCallbackCallbackImpl);
    }

    [MonoPInvokeCallback]
    private static void SetImeSelectionBoundsCallbackCallbackImpl(IntPtr ptr, Rect anchor, Rect focus, bool isAnchorFirst)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        SetImeSelectionBoundsCallbackHandler callback = (SetImeSelectionBoundsCallbackHandler)h.Target;
        h.Free();
        callback(anchor, focus, isAnchorFirst);
    }

    public void SetImeSelectionBoundsCallback(SetImeSelectionBoundsCallbackHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.SetImeSelectionBoundsCallback(MethodsPtr, GCHandle.ToIntPtr(wrapped), SetImeSelectionBoundsCallbackCallbackImpl);
    }

    public bool IsPointInsideClickZone(Int32 x, Int32 y)
    {
        return Methods.IsPointInsideClickZone(MethodsPtr, x, y);
    }

    [MonoPInvokeCallback]
    private static void OnToggleImpl(IntPtr ptr, bool locked)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.OverlayManagerInstance.OnToggle != null)
        {
            d.OverlayManagerInstance.OnToggle.Invoke(locked);
        }
    }
}

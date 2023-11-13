using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class VoiceManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct FFIEvents
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SettingsUpdateHandler(IntPtr ptr);

        internal SettingsUpdateHandler OnSettingsUpdate;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct FFIMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetInputModeMethod(IntPtr methodsPtr, ref InputMode inputMode);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetInputModeCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void SetInputModeMethod(IntPtr methodsPtr, InputMode inputMode, IntPtr callbackData, SetInputModeCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result IsSelfMuteMethod(IntPtr methodsPtr, ref bool mute);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result SetSelfMuteMethod(IntPtr methodsPtr, bool mute);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result IsSelfDeafMethod(IntPtr methodsPtr, ref bool deaf);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result SetSelfDeafMethod(IntPtr methodsPtr, bool deaf);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result IsLocalMuteMethod(IntPtr methodsPtr, Int64 userId, ref bool mute);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result SetLocalMuteMethod(IntPtr methodsPtr, Int64 userId, bool mute);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetLocalVolumeMethod(IntPtr methodsPtr, Int64 userId, ref byte volume);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result SetLocalVolumeMethod(IntPtr methodsPtr, Int64 userId, byte volume);

        internal GetInputModeMethod GetInputMode;

        internal SetInputModeMethod SetInputMode;

        internal IsSelfMuteMethod IsSelfMute;

        internal SetSelfMuteMethod SetSelfMute;

        internal IsSelfDeafMethod IsSelfDeaf;

        internal SetSelfDeafMethod SetSelfDeaf;

        internal IsLocalMuteMethod IsLocalMute;

        internal SetLocalMuteMethod SetLocalMute;

        internal GetLocalVolumeMethod GetLocalVolume;

        internal SetLocalVolumeMethod SetLocalVolume;
    }

    public delegate void SetInputModeHandler(Result result);

    public delegate void SettingsUpdateHandler();

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

    public event SettingsUpdateHandler OnSettingsUpdate;

    internal VoiceManager(IntPtr ptr, IntPtr eventsPtr, ref FFIEvents events)
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
        events.OnSettingsUpdate = OnSettingsUpdateImpl;
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    public InputMode GetInputMode()
    {
        var ret = new InputMode();
        var res = Methods.GetInputMode(MethodsPtr, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    [MonoPInvokeCallback]
    private static void SetInputModeCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        SetInputModeHandler callback = (SetInputModeHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void SetInputMode(InputMode inputMode, SetInputModeHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.SetInputMode(MethodsPtr, inputMode, GCHandle.ToIntPtr(wrapped), SetInputModeCallbackImpl);
    }

    public bool IsSelfMute()
    {
        var ret = new bool();
        var res = Methods.IsSelfMute(MethodsPtr, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public void SetSelfMute(bool mute)
    {
        var res = Methods.SetSelfMute(MethodsPtr, mute);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    public bool IsSelfDeaf()
    {
        var ret = new bool();
        var res = Methods.IsSelfDeaf(MethodsPtr, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public void SetSelfDeaf(bool deaf)
    {
        var res = Methods.SetSelfDeaf(MethodsPtr, deaf);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    public bool IsLocalMute(Int64 userId)
    {
        var ret = new bool();
        var res = Methods.IsLocalMute(MethodsPtr, userId, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public void SetLocalMute(Int64 userId, bool mute)
    {
        var res = Methods.SetLocalMute(MethodsPtr, userId, mute);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    public byte GetLocalVolume(Int64 userId)
    {
        var ret = new byte();
        var res = Methods.GetLocalVolume(MethodsPtr, userId, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public void SetLocalVolume(Int64 userId, byte volume)
    {
        var res = Methods.SetLocalVolume(MethodsPtr, userId, volume);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    [MonoPInvokeCallback]
    private static void OnSettingsUpdateImpl(IntPtr ptr)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.VoiceManagerInstance.OnSettingsUpdate != null)
        {
            d.VoiceManagerInstance.OnSettingsUpdate.Invoke();
        }
    }
}

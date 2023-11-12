using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

public partial class StorageManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIEvents
    {

    }

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result ReadMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, Int32 dataLen, ref UInt32 read);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ReadAsyncCallback(IntPtr ptr, Result result, IntPtr dataPtr, Int32 dataLen);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ReadAsyncMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, IntPtr callbackData, ReadAsyncCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ReadAsyncPartialCallback(IntPtr ptr, Result result, IntPtr dataPtr, Int32 dataLen);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void ReadAsyncPartialMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, UInt64 offset, UInt64 length, IntPtr callbackData, ReadAsyncPartialCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result WriteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, Int32 dataLen);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void WriteAsyncCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void WriteAsyncMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, byte[] data, Int32 dataLen, IntPtr callbackData, WriteAsyncCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result DeleteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result ExistsMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref bool exists);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void CountMethod(IntPtr methodsPtr, ref Int32 count);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result StatMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string name, ref FileStat stat);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result StatAtMethod(IntPtr methodsPtr, Int32 index, ref FileStat stat);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetPathMethod(IntPtr methodsPtr, StringBuilder path);

        internal ReadMethod Read;

        internal ReadAsyncMethod ReadAsync;

        internal ReadAsyncPartialMethod ReadAsyncPartial;

        internal WriteMethod Write;

        internal WriteAsyncMethod WriteAsync;

        internal DeleteMethod Delete;

        internal ExistsMethod Exists;

        internal CountMethod Count;

        internal StatMethod Stat;

        internal StatAtMethod StatAt;

        internal GetPathMethod GetPath;
    }

    public delegate void ReadAsyncHandler(Result result, byte[] data);

    public delegate void ReadAsyncPartialHandler(Result result, byte[] data);

    public delegate void WriteAsyncHandler(Result result);

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

    internal StorageManager(IntPtr ptr, IntPtr eventsPtr, ref FFIEvents events)
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
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    public UInt32 Read(string name, byte[] data)
    {
        var ret = new UInt32();
        var res = Methods.Read(MethodsPtr, name, data, data.Length, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    [MonoPInvokeCallback]
    private static void ReadAsyncCallbackImpl(IntPtr ptr, Result result, IntPtr dataPtr, Int32 dataLen)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        ReadAsyncHandler callback = (ReadAsyncHandler)h.Target;
        h.Free();
        byte[] data = new byte[dataLen];
        Marshal.Copy(dataPtr, data, 0, (int)dataLen);
        callback(result, data);
    }

    public void ReadAsync(string name, ReadAsyncHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.ReadAsync(MethodsPtr, name, GCHandle.ToIntPtr(wrapped), ReadAsyncCallbackImpl);
    }

    [MonoPInvokeCallback]
    private static void ReadAsyncPartialCallbackImpl(IntPtr ptr, Result result, IntPtr dataPtr, Int32 dataLen)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        ReadAsyncPartialHandler callback = (ReadAsyncPartialHandler)h.Target;
        h.Free();
        byte[] data = new byte[dataLen];
        Marshal.Copy(dataPtr, data, 0, (int)dataLen);
        callback(result, data);
    }

    public void ReadAsyncPartial(string name, UInt64 offset, UInt64 length, ReadAsyncPartialHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.ReadAsyncPartial(MethodsPtr, name, offset, length, GCHandle.ToIntPtr(wrapped), ReadAsyncPartialCallbackImpl);
    }

    public void Write(string name, byte[] data)
    {
        var res = Methods.Write(MethodsPtr, name, data, data.Length);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    [MonoPInvokeCallback]
    private static void WriteAsyncCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        WriteAsyncHandler callback = (WriteAsyncHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void WriteAsync(string name, byte[] data, WriteAsyncHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.WriteAsync(MethodsPtr, name, data, data.Length, GCHandle.ToIntPtr(wrapped), WriteAsyncCallbackImpl);
    }

    public void Delete(string name)
    {
        var res = Methods.Delete(MethodsPtr, name);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    public bool Exists(string name)
    {
        var ret = new bool();
        var res = Methods.Exists(MethodsPtr, name, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public Int32 Count()
    {
        var ret = new Int32();
        Methods.Count(MethodsPtr, ref ret);
        return ret;
    }

    public FileStat Stat(string name)
    {
        var ret = new FileStat();
        var res = Methods.Stat(MethodsPtr, name, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public FileStat StatAt(Int32 index)
    {
        var ret = new FileStat();
        var res = Methods.StatAt(MethodsPtr, index, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public string GetPath()
    {
        var ret = new StringBuilder(4096);
        var res = Methods.GetPath(MethodsPtr, ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret.ToString();
    }
}

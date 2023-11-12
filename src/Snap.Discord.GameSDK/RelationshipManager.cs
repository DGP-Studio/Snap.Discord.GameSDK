using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public partial class RelationshipManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIEvents
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void RefreshHandler(IntPtr ptr);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void RelationshipUpdateHandler(IntPtr ptr, ref Relationship relationship);

        internal RefreshHandler OnRefresh;

        internal RelationshipUpdateHandler OnRelationshipUpdate;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate bool FilterCallback(IntPtr ptr, ref Relationship relationship);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void FilterMethod(IntPtr methodsPtr, IntPtr callbackData, FilterCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result CountMethod(IntPtr methodsPtr, ref Int32 count);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetMethod(IntPtr methodsPtr, Int64 userId, ref Relationship relationship);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetAtMethod(IntPtr methodsPtr, UInt32 index, ref Relationship relationship);

        internal FilterMethod Filter;

        internal CountMethod Count;

        internal GetMethod Get;

        internal GetAtMethod GetAt;
    }

    public delegate bool FilterHandler(ref Relationship relationship);

    public delegate void RefreshHandler();

    public delegate void RelationshipUpdateHandler(ref Relationship relationship);

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

    public event RefreshHandler OnRefresh;

    public event RelationshipUpdateHandler OnRelationshipUpdate;

    internal RelationshipManager(IntPtr ptr, IntPtr eventsPtr, ref FFIEvents events)
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
        events.OnRefresh = OnRefreshImpl;
        events.OnRelationshipUpdate = OnRelationshipUpdateImpl;
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    [MonoPInvokeCallback]
    private static bool FilterCallbackImpl(IntPtr ptr, ref Relationship relationship)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        FilterHandler callback = (FilterHandler)h.Target;
        return callback(ref relationship);
    }

    public void Filter(FilterHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.Filter(MethodsPtr, GCHandle.ToIntPtr(wrapped), FilterCallbackImpl);
        wrapped.Free();
    }

    public Int32 Count()
    {
        var ret = new Int32();
        var res = Methods.Count(MethodsPtr, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public Relationship Get(Int64 userId)
    {
        var ret = new Relationship();
        var res = Methods.Get(MethodsPtr, userId, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public Relationship GetAt(UInt32 index)
    {
        var ret = new Relationship();
        var res = Methods.GetAt(MethodsPtr, index, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    [MonoPInvokeCallback]
    private static void OnRefreshImpl(IntPtr ptr)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.RelationshipManagerInstance.OnRefresh != null)
        {
            d.RelationshipManagerInstance.OnRefresh.Invoke();
        }
    }

    [MonoPInvokeCallback]
    private static void OnRelationshipUpdateImpl(IntPtr ptr, ref Relationship relationship)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.RelationshipManagerInstance.OnRelationshipUpdate != null)
        {
            d.RelationshipManagerInstance.OnRelationshipUpdate.Invoke(ref relationship);
        }
    }
}

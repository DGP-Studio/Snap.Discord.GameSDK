using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public partial class StoreManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIEvents
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void EntitlementCreateHandler(IntPtr ptr, ref Entitlement entitlement);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void EntitlementDeleteHandler(IntPtr ptr, ref Entitlement entitlement);

        internal EntitlementCreateHandler OnEntitlementCreate;

        internal EntitlementDeleteHandler OnEntitlementDelete;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal partial struct FFIMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void FetchSkusCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void FetchSkusMethod(IntPtr methodsPtr, IntPtr callbackData, FetchSkusCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void CountSkusMethod(IntPtr methodsPtr, ref Int32 count);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetSkuMethod(IntPtr methodsPtr, Int64 skuId, ref Sku sku);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetSkuAtMethod(IntPtr methodsPtr, Int32 index, ref Sku sku);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void FetchEntitlementsCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void FetchEntitlementsMethod(IntPtr methodsPtr, IntPtr callbackData, FetchEntitlementsCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void CountEntitlementsMethod(IntPtr methodsPtr, ref Int32 count);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetEntitlementMethod(IntPtr methodsPtr, Int64 entitlementId, ref Entitlement entitlement);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetEntitlementAtMethod(IntPtr methodsPtr, Int32 index, ref Entitlement entitlement);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result HasSkuEntitlementMethod(IntPtr methodsPtr, Int64 skuId, ref bool hasEntitlement);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void StartPurchaseCallback(IntPtr ptr, Result result);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void StartPurchaseMethod(IntPtr methodsPtr, Int64 skuId, IntPtr callbackData, StartPurchaseCallback callback);

        internal FetchSkusMethod FetchSkus;

        internal CountSkusMethod CountSkus;

        internal GetSkuMethod GetSku;

        internal GetSkuAtMethod GetSkuAt;

        internal FetchEntitlementsMethod FetchEntitlements;

        internal CountEntitlementsMethod CountEntitlements;

        internal GetEntitlementMethod GetEntitlement;

        internal GetEntitlementAtMethod GetEntitlementAt;

        internal HasSkuEntitlementMethod HasSkuEntitlement;

        internal StartPurchaseMethod StartPurchase;
    }

    public delegate void FetchSkusHandler(Result result);

    public delegate void FetchEntitlementsHandler(Result result);

    public delegate void StartPurchaseHandler(Result result);

    public delegate void EntitlementCreateHandler(ref Entitlement entitlement);

    public delegate void EntitlementDeleteHandler(ref Entitlement entitlement);

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

    public event EntitlementCreateHandler OnEntitlementCreate;

    public event EntitlementDeleteHandler OnEntitlementDelete;

    internal StoreManager(IntPtr ptr, IntPtr eventsPtr, ref FFIEvents events)
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
        events.OnEntitlementCreate = OnEntitlementCreateImpl;
        events.OnEntitlementDelete = OnEntitlementDeleteImpl;
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    [MonoPInvokeCallback]
    private static void FetchSkusCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        FetchSkusHandler callback = (FetchSkusHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void FetchSkus(FetchSkusHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.FetchSkus(MethodsPtr, GCHandle.ToIntPtr(wrapped), FetchSkusCallbackImpl);
    }

    public Int32 CountSkus()
    {
        var ret = new Int32();
        Methods.CountSkus(MethodsPtr, ref ret);
        return ret;
    }

    public Sku GetSku(Int64 skuId)
    {
        var ret = new Sku();
        var res = Methods.GetSku(MethodsPtr, skuId, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public Sku GetSkuAt(Int32 index)
    {
        var ret = new Sku();
        var res = Methods.GetSkuAt(MethodsPtr, index, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    [MonoPInvokeCallback]
    private static void FetchEntitlementsCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        FetchEntitlementsHandler callback = (FetchEntitlementsHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void FetchEntitlements(FetchEntitlementsHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.FetchEntitlements(MethodsPtr, GCHandle.ToIntPtr(wrapped), FetchEntitlementsCallbackImpl);
    }

    public Int32 CountEntitlements()
    {
        var ret = new Int32();
        Methods.CountEntitlements(MethodsPtr, ref ret);
        return ret;
    }

    public Entitlement GetEntitlement(Int64 entitlementId)
    {
        var ret = new Entitlement();
        var res = Methods.GetEntitlement(MethodsPtr, entitlementId, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public Entitlement GetEntitlementAt(Int32 index)
    {
        var ret = new Entitlement();
        var res = Methods.GetEntitlementAt(MethodsPtr, index, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public bool HasSkuEntitlement(Int64 skuId)
    {
        var ret = new bool();
        var res = Methods.HasSkuEntitlement(MethodsPtr, skuId, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    [MonoPInvokeCallback]
    private static void StartPurchaseCallbackImpl(IntPtr ptr, Result result)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        StartPurchaseHandler callback = (StartPurchaseHandler)h.Target;
        h.Free();
        callback(result);
    }

    public void StartPurchase(Int64 skuId, StartPurchaseHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.StartPurchase(MethodsPtr, skuId, GCHandle.ToIntPtr(wrapped), StartPurchaseCallbackImpl);
    }

    [MonoPInvokeCallback]
    private static void OnEntitlementCreateImpl(IntPtr ptr, ref Entitlement entitlement)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.StoreManagerInstance.OnEntitlementCreate != null)
        {
            d.StoreManagerInstance.OnEntitlementCreate.Invoke(ref entitlement);
        }
    }

    [MonoPInvokeCallback]
    private static void OnEntitlementDeleteImpl(IntPtr ptr, ref Entitlement entitlement)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.StoreManagerInstance.OnEntitlementDelete != null)
        {
            d.StoreManagerInstance.OnEntitlementDelete.Invoke(ref entitlement);
        }
    }
}

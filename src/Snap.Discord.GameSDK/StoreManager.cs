using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class StoreManager
{
    private unsafe StoreMethods* MethodsPtr;

    internal unsafe StoreManager(StoreMethods* ptr, StoreEvents* eventsPtr, ref StoreEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(StoreEvents* eventsPtr, ref StoreEvents events)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnEntitlementCreateImpl(nint ptr, Entitlement* entitlement)
        {
            DiscordGCHandle.Get(ptr).StoreManagerInstance.OnEntitlementCreate(ref *entitlement);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnEntitlementDeleteImpl(nint ptr, Entitlement* entitlement)
        {
            DiscordGCHandle.Get(ptr).StoreManagerInstance.OnEntitlementDelete(ref *entitlement);
        }

        events.OnEntitlementCreate = EntitlementCreateHandler.Create(&OnEntitlementCreateImpl);
        events.OnEntitlementDelete = EntitlementDeleteHandler.Create(&OnEntitlementDeleteImpl);
        *eventsPtr = events;
    }

    public unsafe void FetchSkus(FetchSkusHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void FetchSkusCallbackImpl(FetchSkusHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->FetchSkus.Invoke(MethodsPtr, callback, FetchSkusCallback.Create(&FetchSkusCallbackImpl));
    }

    public unsafe int CountSkus()
    {
        int ret = default;
        MethodsPtr->CountSkus.Invoke(MethodsPtr, &ret);
        return ret;
    }

    public unsafe Sku GetSku(long skuId)
    {
        Sku ret = default;
        MethodsPtr->GetSku.Invoke(MethodsPtr, skuId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe Sku GetSkuAt(int index)
    {
        Sku ret = default;
        MethodsPtr->GetSkuAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe void FetchEntitlements(FetchEntitlementsHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void FetchEntitlementsCallbackImpl(FetchEntitlementsHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->FetchEntitlements.Invoke(MethodsPtr, callback, FetchEntitlementsCallback.Create(&FetchEntitlementsCallbackImpl));
    }

    public unsafe int CountEntitlements()
    {
        int ret = default;
        MethodsPtr->CountEntitlements.Invoke(MethodsPtr, &ret);
        return ret;
    }

    public unsafe Entitlement GetEntitlement(long entitlementId)
    {
        Entitlement ret = default;
        MethodsPtr->GetEntitlement.Invoke(MethodsPtr, entitlementId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe Entitlement GetEntitlementAt(int index)
    {
        Entitlement ret = default;
        MethodsPtr->GetEntitlementAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe bool HasSkuEntitlement(long skuId)
    {
        bool ret = default;
        MethodsPtr->HasSkuEntitlement.Invoke(MethodsPtr, skuId, &ret);
        return ret;
    }

    public unsafe void StartPurchase(long skuId, StartPurchaseHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void StartPurchaseCallbackImpl(StartPurchaseHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->StartPurchase.Invoke(MethodsPtr, skuId, callback, StartPurchaseCallback.Create(&StartPurchaseCallbackImpl));
    }

    protected virtual void OnEntitlementCreate(ref Entitlement entitlement)
    {
    }

    protected virtual void OnEntitlementDelete(ref Entitlement entitlement)
    {
    }
}
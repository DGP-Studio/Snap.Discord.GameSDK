using Snap.Discord.GameSDK.ABI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public class StoreManager
{
    private unsafe readonly StoreMethods* MethodsPtr;

    internal unsafe StoreManager(StoreMethods* ptr, StoreEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(StoreEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnEntitlementCreateImpl(nint ptr, Entitlement* entitlement)
        {
            DiscordGCHandle.Get(ptr).StoreManagerInstance?.OnEntitlementCreate(ref *entitlement);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnEntitlementDeleteImpl(nint ptr, Entitlement* entitlement)
        {
            DiscordGCHandle.Get(ptr).StoreManagerInstance?.OnEntitlementDelete(ref *entitlement);
        }

        eventsPtr->OnEntitlementCreate = EntitlementCreateHandler.Create(&OnEntitlementCreateImpl);
        eventsPtr->OnEntitlementDelete = EntitlementDeleteHandler.Create(&OnEntitlementDeleteImpl);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void FetchSkus(FetchSkusHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void FetchSkusCallbackImpl(FetchSkusHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->FetchSkus.Invoke(MethodsPtr, callback, FetchSkusCallback.Create(&FetchSkusCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe int CountSkus()
    {
        int ret = default;
        MethodsPtr->CountSkus.Invoke(MethodsPtr, &ret);
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe Sku GetSku(long skuId)
    {
        Sku ret = default;
        MethodsPtr->GetSku.Invoke(MethodsPtr, skuId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe Sku GetSkuAt(int index)
    {
        Sku ret = default;
        MethodsPtr->GetSkuAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void FetchEntitlements(FetchEntitlementsHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void FetchEntitlementsCallbackImpl(FetchEntitlementsHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->FetchEntitlements.Invoke(MethodsPtr, callback, FetchEntitlementsCallback.Create(&FetchEntitlementsCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe int CountEntitlements()
    {
        int ret = default;
        MethodsPtr->CountEntitlements.Invoke(MethodsPtr, &ret);
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe Entitlement GetEntitlement(long entitlementId)
    {
        Entitlement ret = default;
        MethodsPtr->GetEntitlement.Invoke(MethodsPtr, entitlementId, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe Entitlement GetEntitlementAt(int index)
    {
        Entitlement ret = default;
        MethodsPtr->GetEntitlementAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe bool HasSkuEntitlement(long skuId)
    {
        bool ret = default;
        MethodsPtr->HasSkuEntitlement.Invoke(MethodsPtr, skuId, &ret);
        return ret;
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void StartPurchase(long skuId, StartPurchaseHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void StartPurchaseCallbackImpl(StartPurchaseHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->StartPurchase.Invoke(MethodsPtr, skuId, callback, StartPurchaseCallback.Create(&StartPurchaseCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnEntitlementCreate(ref Entitlement entitlement)
    {
    }

    [Obsolete("Deprecated by Discord")]
    protected virtual void OnEntitlementDelete(ref Entitlement entitlement)
    {
    }
}
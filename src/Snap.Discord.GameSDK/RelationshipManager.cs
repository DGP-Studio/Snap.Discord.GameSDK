using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class RelationshipManager
{
    private unsafe readonly RelationshipMethods* MethodsPtr;

    internal unsafe RelationshipManager(RelationshipMethods* ptr, RelationshipEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(RelationshipEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void OnRefreshImpl(nint ptr)
        {
            DiscordGCHandle.Get(ptr).RelationshipManagerInstance?.OnRefresh();
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnRelationshipUpdateImpl(nint ptr, Relationship* relationship)
        {
            DiscordGCHandle.Get(ptr).RelationshipManagerInstance?.OnRelationshipUpdate(ref *relationship);
        }

        eventsPtr->OnRefresh = RefreshHandler.Create(&OnRefreshImpl);
        eventsPtr->OnRelationshipUpdate = RelationshipUpdateHandler.Create(&OnRelationshipUpdateImpl);
    }

    public unsafe void Filter(FilterHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe bool FilterCallbackImpl(FilterHandler ptr, Relationship* relationship)
        {
            return ptr.Invoke(relationship);
        }

        MethodsPtr->Filter.Invoke(MethodsPtr, callback, FilterCallback.Create(&FilterCallbackImpl));
    }

    public unsafe int Count()
    {
        int ret = default;
        MethodsPtr->Count.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe Relationship Get(long userId)
    {
        Relationship ret = default;
        MethodsPtr->Get.Invoke(MethodsPtr, userId, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe Relationship GetAt(uint index)
    {
        Relationship ret = default;
        MethodsPtr->GetAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    protected virtual void OnRefresh()
    {
    }

    protected virtual void OnRelationshipUpdate(ref Relationship relationship)
    {
    }
}
using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

/// <summary>
/// This manager helps you access the relationships your players have made on Discord. Unfortunately, it won't help them make relationships IRL. They're on their own for that. It lets you:
/// <para>Access a user's relationships</para>
/// <para>Filter those relationships based on a given criteria</para>
/// <para>Build a user's friends list</para>
/// <para><b>First Notes</b></para>
/// <para>Relationships on Discord change often; people start and stop playing games, go online, offline, invisible, or otherwise change state. Therefore, there are some important factors to remember when working with this manager. When you are first getting a list of a user's relationships, before you can <see cref="Filter(FilterHandler)"/>, you need to wait for the <see cref="OnRefresh"/> callback to fire. This is your indicator that Discord has successfully taken a snapshot of the state of all your relationships at a given moment. Now that you have this snapshot, you can <see cref="Filter(FilterHandler)"/> it to build the list that you want, and then iterate over that list to do whatever your game needs to do. Use this to build your initial social graph for a user.</para>
/// <para>As relationships change, the <see cref="OnRelationshipUpdate(ref Snap.Discord.GameSDK.Relationship)"/> event will fire. You can use this to update the user's social graph, changing the status of the other Discord users that you chose to filter, e.g. someone is now online, or now playing the game, or no longer playing.</para>
/// </summary>
public class RelationshipManager
{
    private unsafe readonly RelationshipMethods* MethodsPtr;

    public unsafe RelationshipManager(RelationshipMethods* ptr, RelationshipEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(RelationshipEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnRefreshImpl(nint ptr)
        {
            DiscordGCHandle.Get(ptr).RelationshipManagerInstance?.OnRefresh();
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnRelationshipUpdateImpl(nint ptr, Relationship* relationship)
        {
            DiscordGCHandle.Get(ptr).RelationshipManagerInstance?.OnRelationshipUpdate(ref *relationship);
        }

        eventsPtr->OnRefresh = RefreshHandler.Create(&OnRefreshImpl);
        eventsPtr->OnRelationshipUpdate = RelationshipUpdateHandler.Create(&OnRelationshipUpdateImpl);
    }

    /// <summary>
    /// Filters a user's relationship list by a boolean condition.
    /// </summary>
    /// <param name="callback"></param>
    public unsafe void Filter(FilterHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe bool FilterCallbackImpl(FilterHandler ptr, Relationship* relationship)
        {
            return ptr.Invoke(relationship);
        }

        MethodsPtr->Filter.Invoke(MethodsPtr, callback, FilterCallback.Create(&FilterCallbackImpl));
    }

    /// <summary>
    /// Get the number of relationships that match your <see cref="Filter(FilterHandler)"/>
    /// </summary>
    /// <returns></returns>
    public unsafe int Count()
    {
        int ret = default;
        MethodsPtr->Count.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    /// <summary>
    /// Get the relationship between the current user and a given user by id.
    /// </summary>
    /// <param name="userId">the id of the user to fetch</param>
    /// <returns></returns>
    public unsafe Relationship Get(long userId)
    {
        Relationship ret = default;
        MethodsPtr->Get.Invoke(MethodsPtr, userId, &ret).ThrowOnFailure();
        return ret;
    }

    /// <summary>
    /// Get the relationship at a given index when iterating over a list of relationships.
    /// </summary>
    /// <param name="index">index in the list</param>
    /// <returns></returns>
    public unsafe Relationship GetAt(uint index)
    {
        Relationship ret = default;
        MethodsPtr->GetAt.Invoke(MethodsPtr, index, &ret).ThrowOnFailure();
        return ret;
    }

    /// <summary>
    /// Fires at initialization when Discord has cached a snapshot of the current status of all your relationships. Wait for this to fire before calling Filter within its callback.
    /// </summary>
    protected virtual void OnRefresh()
    {
    }

    /// <summary>
    /// Fires when a relationship in the filtered list changes, like an updated presence or user attribute.
    /// </summary>
    /// <param name="relationship"></param>
    protected virtual void OnRelationshipUpdate(ref Relationship relationship)
    {
    }
}
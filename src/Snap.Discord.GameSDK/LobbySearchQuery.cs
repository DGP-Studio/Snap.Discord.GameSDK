using Snap.Discord.GameSDK.ABI;
using System;
using System.Text;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public struct LobbySearchQuery
{
    [Obsolete("Deprecated by Discord")]
    internal unsafe LobbySearchQueryMethods* MethodsPtr;

    [Obsolete("Deprecated by Discord")]
    public unsafe void Filter(string key, LobbySearchComparison comparison, LobbySearchCast cast, string value)
    {
        if (MethodsPtr is not null)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed (byte* pKey = keyBytes)
            {
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);
                fixed (byte* pValue = valueBytes)
                {
                    MethodsPtr->Filter.Invoke(MethodsPtr, pKey, comparison, cast, pValue).ThrowOnFailure();
                }
            }
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void Sort(string key, LobbySearchCast cast, string value)
    {
        if (MethodsPtr is not null)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed (byte* pKey = keyBytes)
            {
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);
                fixed (byte* pValue = valueBytes)
                {
                    MethodsPtr->Sort.Invoke(MethodsPtr, pKey, cast, pValue).ThrowOnFailure();
                }
            }
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void Limit(uint limit)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->Limit.Invoke(MethodsPtr, limit).ThrowOnFailure();
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void Distance(LobbySearchDistance distance)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->Distance.Invoke(MethodsPtr, distance).ThrowOnFailure();
        }
    }
}
using ABI.Snap.Discord.GameSDK.Lobby;
using Snap.Discord.GameSDK.Lobby;
using System.Text;

namespace Snap.Discord.GameSDK;

public struct LobbySearchQuery
{
    internal unsafe LobbySearchQueryMethods* MethodsPtr;

    public unsafe void Filter(string key, LobbySearchComparison comparison, LobbySearchCast cast, string value)
    {
        if (MethodsPtr is not null)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed(byte* pKey = keyBytes)
            {
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);
                fixed(byte* pValue = valueBytes)
                {
                    Result res = MethodsPtr->Filter(MethodsPtr, pKey, comparison, cast, pValue);
                    ResultException.ThrowOnFailure(res);
                }
            }
        }
    }

    public unsafe void Sort(string key, LobbySearchCast cast, string value)
    {
        if (MethodsPtr is not null)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed(byte* pKey = keyBytes)
            {
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);
                fixed(byte* pValue = valueBytes)
                {
                    Result res = MethodsPtr->Sort(MethodsPtr, pKey, cast, pValue);
                    ResultException.ThrowOnFailure(res);
                }
            }
        }
    }

    public unsafe void Limit(uint limit)
    {
        if (MethodsPtr is not null)
        {
            Result res = MethodsPtr->Limit(MethodsPtr, limit);
            ResultException.ThrowOnFailure(res);
        }
    }

    public unsafe void Distance(LobbySearchDistance distance)
    {
        if (MethodsPtr is not null)
        {
            Result res = MethodsPtr->Distance(MethodsPtr, distance);
            ResultException.ThrowOnFailure(res);
        }
    }
}
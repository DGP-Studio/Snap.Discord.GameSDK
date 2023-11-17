using Snap.Discord.GameSDK.ABI;
using System;
using System.Text;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public struct LobbyTransaction
{
    [Obsolete("Deprecated by Discord")]
    internal unsafe LobbyTransactionMethods* MethodsPtr;

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetType(LobbyType type)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetType.Invoke(MethodsPtr, type).ThrowOnFailure();
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetOwner(long ownerId)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetOwner.Invoke(MethodsPtr, ownerId).ThrowOnFailure();
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetCapacity(uint capacity)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetCapacity.Invoke(MethodsPtr, capacity).ThrowOnFailure();
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetMetadata(string key, string value)
    {
        if (MethodsPtr is not null)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed (byte* pKey = keyBytes)
            {
                byte[] valueBytes = Encoding.UTF8.GetBytes(value);
                fixed (byte* pValue = valueBytes)
                {
                    MethodsPtr->SetMetadata.Invoke(MethodsPtr, pKey, pValue).ThrowOnFailure();
                }
            }
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void DeleteMetadata(string key)
    {
        if (MethodsPtr is not null)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            fixed (byte* pKey = keyBytes)
            {
                MethodsPtr->DeleteMetadata.Invoke(MethodsPtr, pKey).ThrowOnFailure();
            }
        }
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe void SetLocked(bool locked)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetLocked.Invoke(MethodsPtr, locked).ThrowOnFailure();
        }
    }
}
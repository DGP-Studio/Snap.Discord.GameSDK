using Snap.Discord.GameSDK.ABI;
using System.Text;

namespace Snap.Discord.GameSDK;

public struct LobbyTransaction
{
    internal unsafe LobbyTransactionMethods* MethodsPtr;

    public unsafe void SetType(LobbyType type)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetType.Invoke(MethodsPtr, type).ThrowOnFailure();
        }
    }

    public unsafe void SetOwner(long ownerId)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetOwner.Invoke(MethodsPtr, ownerId).ThrowOnFailure();
        }
    }

    public unsafe void SetCapacity(uint capacity)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetCapacity.Invoke(MethodsPtr, capacity).ThrowOnFailure();
        }
    }

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

    public unsafe void SetLocked(bool locked)
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->SetLocked.Invoke(MethodsPtr, locked).ThrowOnFailure();
        }
    }
}
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
            Result res = MethodsPtr->SetType(MethodsPtr, type);
            ResultException.ThrowOnFailure(res);
        }
    }

    public unsafe void SetOwner(long ownerId)
    {
        if (MethodsPtr is not null)
        {
            Result res = MethodsPtr->SetOwner(MethodsPtr, ownerId);
            ResultException.ThrowOnFailure(res);
        }
    }

    public unsafe void SetCapacity(uint capacity)
    {
        if (MethodsPtr is not null)
        {
            Result res = MethodsPtr->SetCapacity(MethodsPtr, capacity);
            ResultException.ThrowOnFailure(res);
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
                    Result res = MethodsPtr->SetMetadata(MethodsPtr, pKey, pValue);
                    ResultException.ThrowOnFailure(res);
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
                Result res = MethodsPtr->DeleteMetadata(MethodsPtr, pKey);
                ResultException.ThrowOnFailure(res);
            }
        }
    }

    public unsafe void SetLocked(bool locked)
    {
        if (MethodsPtr is not null)
        {
            Result res = MethodsPtr->SetLocked(MethodsPtr, locked);
            ResultException.ThrowOnFailure(res);
        }
    }
}
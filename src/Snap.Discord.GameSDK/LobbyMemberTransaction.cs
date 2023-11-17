using Snap.Discord.GameSDK.ABI;
using System;
using System.Text;

namespace Snap.Discord.GameSDK;

[Obsolete("Deprecated by Discord")]
public struct LobbyMemberTransaction
{
    [Obsolete("Deprecated by Discord")]
    internal unsafe LobbyMemberTransactionMethods* MethodsPtr;

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
}
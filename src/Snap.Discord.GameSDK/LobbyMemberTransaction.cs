using Snap.Discord.GameSDK.ABI;
using System.Text;

namespace Snap.Discord.GameSDK;

public struct LobbyMemberTransaction
{
    internal unsafe LobbyMemberTransactionMethods* MethodsPtr;

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
}
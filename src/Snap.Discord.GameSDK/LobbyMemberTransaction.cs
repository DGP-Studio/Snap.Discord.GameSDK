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
}
using System;
using System.Runtime.CompilerServices;

namespace Snap.Discord.GameSDK;

public class ResultException : Exception
{
    public readonly Result Result;

    public ResultException(Result result)
        : base(result.ToString())
    {
    }

    internal static unsafe void ThrowIfNull(void* ptr)
    {
        if (ptr is null)
        {
            throw new ResultException(Result.InternalError);
        }
    }
}
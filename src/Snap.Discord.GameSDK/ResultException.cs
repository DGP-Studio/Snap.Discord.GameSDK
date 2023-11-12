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

    [Obsolete]
    public static void ThrowOnFailure(Result result)
    {
        if (result is not Result.Ok)
        {
            throw new ResultException(result);
        }
    }

    public static unsafe void ThrowIfNull(void* ptr)
    {
        if (ptr is null)
        {
            throw new ResultException(Result.InternalError);
        }
    }
}

public static class ResultExtension
{
    public static void ThrowOnFailure(this Result result)
    {
        if (result is not Result.Ok)
        {
            throw new ResultException(result);
        }
    }
}
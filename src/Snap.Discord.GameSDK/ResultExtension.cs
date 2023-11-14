namespace Snap.Discord.GameSDK;

internal static class ResultExtension
{
    internal static void ThrowOnFailure(this Result result)
    {
        if (result is not Result.Ok)
        {
            throw new ResultException(result);
        }
    }
}
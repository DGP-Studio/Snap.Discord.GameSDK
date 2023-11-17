using System;

namespace Snap.Discord.GameSDK;

internal static class ThrowHelper
{
    public static Exception InvalidOperation(string? message = default)
    {
        throw new InvalidOperationException(message);
    }

    public static void InvalidOperationIf(bool condition, string? message = default)
    {
        if (condition)
        {
            throw new InvalidOperationException(message);
        }
    }
}
namespace Snap.Discord.GameSDK;

/// <summary>
/// For more details about the activity types, <see href="https://discord.com/developers/docs/topics/gateway-events">see Gateway documentation.</see>
/// <para></para>
/// <see cref="ActivityType"/> is strictly for the purpose of handling events that you receive from Discord; though the SDK/our API will not reject a payload with an ActivityType sent, it will be discarded and will not change anything in the client.
/// </summary>
public enum ActivityType
{
    Playing,
    Streaming,
    Listening,
    Watching,
    Custom,
    Competing,
}
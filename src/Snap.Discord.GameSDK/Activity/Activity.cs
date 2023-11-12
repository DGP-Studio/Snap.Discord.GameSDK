namespace Snap.Discord.GameSDK.Activity;

public struct Activity
{
    public ActivityType Type;

    public long ApplicationId;

    public unsafe fixed byte Name[128];

    public unsafe fixed byte State[128];

    public unsafe fixed byte Details[128];

    public ActivityTimestamps Timestamps;

    public ActivityAssets Assets;

    public ActivityParty Party;

    public ActivitySecrets Secrets;

    public bool Instance;

    public uint SupportedPlatforms;
}
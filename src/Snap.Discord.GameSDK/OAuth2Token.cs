namespace Snap.Discord.GameSDK;

public struct OAuth2Token
{
    public unsafe fixed byte AccessToken[128];

    public unsafe fixed byte Scopes[1024];

    public long Expires;
}
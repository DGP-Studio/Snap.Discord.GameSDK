namespace Snap.Discord.GameSDK.Application;

public partial struct OAuth2Token
{
    public unsafe fixed byte AccessToken[128];

    public unsafe fixed byte Scopes[1024];

    public long Expires;
}
﻿namespace Snap.Discord.GameSDK.Activity;

public partial struct ActivitySecrets
{
    public unsafe fixed byte Match[128];

    public unsafe fixed byte Join[128];

    public unsafe fixed byte Spectate[128];
}
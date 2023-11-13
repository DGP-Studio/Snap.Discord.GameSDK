namespace Snap.Discord.GameSDK;

public struct Lobby
{
    public long Id;

    public LobbyType Type;

    public long OwnerId;

    public unsafe fixed byte Secret[128];

    public uint Capacity;

    public bool Locked;
}
using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct NetworkMethods
{
    [Obsolete("Deprecated by Discord")] internal GetPeerIdMethod GetPeerId;
    [Obsolete("Deprecated by Discord")] internal FlushMethod Flush;
    [Obsolete("Deprecated by Discord")] internal OpenPeerMethod OpenPeer;
    [Obsolete("Deprecated by Discord")] internal UpdatePeerMethod UpdatePeer;
    [Obsolete("Deprecated by Discord")] internal ClosePeerMethod ClosePeer;
    [Obsolete("Deprecated by Discord")] internal OpenChannelMethod OpenChannel;
    [Obsolete("Deprecated by Discord")] internal CloseChannelMethod CloseChannel;
    [Obsolete("Deprecated by Discord")] internal SendMessageMethod SendMessage;
}
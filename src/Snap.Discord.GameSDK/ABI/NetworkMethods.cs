namespace Snap.Discord.GameSDK.ABI;

internal struct NetworkMethods
{
    internal GetPeerIdMethod GetPeerId;
    internal FlushMethod Flush;
    internal OpenPeerMethod OpenPeer;
    internal UpdatePeerMethod UpdatePeer;
    internal ClosePeerMethod ClosePeer;
    internal OpenChannelMethod OpenChannel;
    internal CloseChannelMethod CloseChannel;
    internal SendMessageMethod SendMessage;
}
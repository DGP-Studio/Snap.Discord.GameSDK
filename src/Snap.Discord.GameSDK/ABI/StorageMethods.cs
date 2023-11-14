namespace Snap.Discord.GameSDK.ABI;

internal struct StorageMethods
{
    internal ReadMethod Read;
    internal ReadAsyncMethod ReadAsync;
    internal ReadAsyncPartialMethod ReadAsyncPartial;
    internal WriteMethod Write;
    internal WriteAsyncMethod WriteAsync;
    internal DeleteMethod Delete;
    internal ExistsMethod Exists;
    internal CountMethod Count;
    internal StatMethod Stat;
    internal StatAtMethod StatAt;
    internal GetPathMethod GetPath;
}
using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct StorageMethods
{
    [Obsolete("Deprecated by Discord")] internal ReadMethod Read;
    [Obsolete("Deprecated by Discord")] internal ReadAsyncMethod ReadAsync;
    [Obsolete("Deprecated by Discord")] internal ReadAsyncPartialMethod ReadAsyncPartial;
    [Obsolete("Deprecated by Discord")] internal WriteMethod Write;
    [Obsolete("Deprecated by Discord")] internal WriteAsyncMethod WriteAsync;
    [Obsolete("Deprecated by Discord")] internal DeleteMethod Delete;
    [Obsolete("Deprecated by Discord")] internal ExistsMethod Exists;
    [Obsolete("Deprecated by Discord")] internal CountMethod Count;
    [Obsolete("Deprecated by Discord")] internal StatMethod Stat;
    [Obsolete("Deprecated by Discord")] internal StatAtMethod StatAt;
    [Obsolete("Deprecated by Discord")] internal GetPathMethod GetPath;
}
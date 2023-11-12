namespace Snap.Discord.GameSDK;

public delegate void DiscordAction<T>(ref T t);

public delegate void DiscordAction<T1, T2, T3>(T1 t1, ref T2 t2, ref T3 t3);
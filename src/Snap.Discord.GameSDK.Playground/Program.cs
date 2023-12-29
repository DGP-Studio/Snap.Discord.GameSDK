using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK.Playground;

internal class Program
{
    static unsafe void Main()
    {
        ABI.DiscordCreateParams @params = default;
        Methods.DiscordCreateParamsSetDefault(&@params);
        @params.client_id = 1173950861647552623L;
        @params.flags = (uint)DiscordCreateFlags.Default;
        IDiscordCore* core;
        Methods.DiscordCreate(3, &@params, &core);

        ThreadPool.QueueUserWorkItem(obj =>
        {
            IDiscordCore* d = (IDiscordCore*)(nint)obj!;
            while (true)
            {
                Thread.Sleep(100);
                d->run_callbacks(d);
            }
        }, (nint)core);

        IDiscordActivityManager* discordActivityManager = core->get_activity_manager(core);

        DiscordActivity activity1 = default;
        SByteString.Set(activity1.state, 128, "服务器：天空岛");
        SByteString.Set(activity1.details, 128, "在提瓦特大陆中探索");
        activity1.timestamps.start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        SByteString.Set(activity1.assets.large_image, 128, "icon");
        SByteString.Set(activity1.assets.large_text, 128, "原神");
        SByteString.Set(activity1.assets.small_image, 128, "hutaoicon2");
        SByteString.Set(activity1.assets.small_text, 128, "使用 Snap Hutao 启动");
        discordActivityManager->update_activity(discordActivityManager, &activity1, null, &UpdateActivity);

        Console.ReadKey();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void UpdateActivity(void* any, DiscordResult result)
    {
        Console.WriteLine($"UpdateActivity Result: {result}");
    }
}
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK.ABI;

public static unsafe partial class Methods
{
    public static void DiscordCreateParamsSetDefault([NativeTypeName("struct DiscordCreateParams *")] DiscordCreateParams* @params)
    {
        Unsafe.InitBlockUnaligned(@params, 0, (uint)sizeof(DiscordCreateParams));
        @params->application_version = 1;
        @params->user_version = 1;
        @params->image_version = 1;
        @params->activity_version = 1;
        @params->relationship_version = 1;
        @params->lobby_version = 1;
        @params->network_version = 1;
        @params->overlay_version = 2;
        @params->storage_version = 1;
        @params->store_version = 1;
        @params->voice_version = 1;
        @params->achievement_version = 1;
    }

    [DllImport("discord_game_sdk.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("enum EDiscordResult")]
    public static extern DiscordResult DiscordCreate([NativeTypeName("DiscordVersion")] int version, [NativeTypeName("struct DiscordCreateParams *")] DiscordCreateParams* @params, [NativeTypeName("struct IDiscordCore **")] IDiscordCore** result);
}

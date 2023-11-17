using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct ApplicationMethods
{
    [Obsolete("Deprecated by Discord")] internal ValidateOrExitMethod ValidateOrExit;
    [Obsolete("Deprecated by Discord")] internal GetCurrentLocaleMethod GetCurrentLocale;
    [Obsolete("Deprecated by Discord")] internal GetCurrentBranchMethod GetCurrentBranch;
    [Obsolete("Deprecated by Discord")] internal GetOAuth2TokenMethod GetOAuth2Token;
    [Obsolete("Deprecated by Discord")] internal GetTicketMethod GetTicket;
}
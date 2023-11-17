using System;

namespace Snap.Discord.GameSDK.ABI;

[Obsolete("Deprecated by Discord")]
internal struct StoreMethods
{
    [Obsolete("Deprecated by Discord")] internal FetchSkusMethod FetchSkus;
    [Obsolete("Deprecated by Discord")] internal CountSkusMethod CountSkus;
    [Obsolete("Deprecated by Discord")] internal GetSkuMethod GetSku;
    [Obsolete("Deprecated by Discord")] internal GetSkuAtMethod GetSkuAt;
    [Obsolete("Deprecated by Discord")] internal FetchEntitlementsMethod FetchEntitlements;
    [Obsolete("Deprecated by Discord")] internal CountEntitlementsMethod CountEntitlements;
    [Obsolete("Deprecated by Discord")] internal GetEntitlementMethod GetEntitlement;
    [Obsolete("Deprecated by Discord")] internal GetEntitlementAtMethod GetEntitlementAt;
    [Obsolete("Deprecated by Discord")] internal HasSkuEntitlementMethod HasSkuEntitlement;
    [Obsolete("Deprecated by Discord")] internal StartPurchaseMethod StartPurchase;
}
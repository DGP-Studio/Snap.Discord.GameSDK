﻿using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public partial struct Relationship
{
    public RelationshipType Type;

    public User User;

    public Presence Presence;
}

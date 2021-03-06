﻿using System;

namespace Throne.World.Structures.World
{
    [Flags]
    public enum MapAttribute : long
    {
        Normal = 0,
        PkField = 1 << 0,
        ChangeMapDisable = 1 << 1,
        RecordDisable = 1 << 2,
        PkDisable = 1 << 3,
        BoothEnable = 1 << 4,
        TeamDisable = 1 << 5,
        TeleportDisable = 1 << 6,
        GuildMap = 1 << 7,
        PrisonMap = 1 << 8,
        FlyingDisabled = 1 << 9,
        House = 1 << 10,
        MineField = 1 << 11,
        NewbieSafetyArea = 1 << 12,
        RebornNowEnable = 1 << 13,
        NewbieProtect = 1 << 14,
        TrainingDisable = 1 << 15,
        MountMap = 1 << 25,
        GuildMapActive = 1 << 26,
        GuildCTFMap = 1 << 28,
        TakeItemsInAttack = 1 << 43
    }

    [Flags]
    public enum CellType : byte
    {
        None = 0x0,
        Open = 0x1,
        Portal = 0x2, 
        Item = 0x4,
        Npc = 0x8,
        Monster = 0x10,
        Terrain = 0x20
    }

    public enum MapObjectType
    {
        Scenery = 1,
        DDSOverlay = 4,
        Effect = 10,
        Sound = 15
    }

    public enum MapIds : uint
    {
        TwinCity = 1002,
        BeginnersVillage = 1010
    }
}
﻿using System;
using Throne.Framework.Network.Connectivity;
using Throne.Framework.Network.Handling;
using Throne.Framework.Network.Transmission;
using Throne.World.Structures.Objects;

namespace Throne.World.Network.Messages
{
    [Handling.WorldPacketHandler(PacketTypes.LoadMap)]
    public class LoadMap : WorldPacket
    {
        /// <summary>
        ///     Incoming constructor.
        /// </summary>
        /// <param name="array">Incoming byte array.</param>
        public LoadMap(Byte[] array) : base(array)
        {
        }

        public LoadMap(Character @c)
            : base(PacketTypes.LoadMap, 24)
        {
            WriteInt(Environment.TickCount);
            WriteInt(0); //progress
            WriteInt(1); //ready
            WriteUInt(@c.Location.Map.Document);
        }

        public override bool Read(IClient client)
        {
            return true;
        }
    }
}
﻿using System;
using Throne.Login.Accounts;
using Throne.Login.Network.Handling;
using Throne.Login.Properties;
using Throne.Framework.Network.Connectivity;
using Throne.Framework.Network.Transmission;

namespace Throne.Login.Network.Messages
{
    [AuthenticationPacketHandler(PacketTypes.SRP6ProtocolAuthenticationReqeust)]
    public sealed class SRP6ProtocolAuthenticationRequest : AuthenticationPacket
    {
        private const int LENGTH = 312;

        private String MacAddress;
        private String Password;
        private String Server;
        private String Username;

        /// <summary>
        ///     Incoming constructor.
        /// </summary>
        /// <param name="array">Incoming byte array.</param>
        public SRP6ProtocolAuthenticationRequest(byte[] array)
            : base(array)
        {
        }

        public override bool Read(IClient client)
        {
            if (ArrayLength != LENGTH)
                InvalidValue(client, "Length", ArrayLength, LENGTH);

            Username = Seek(8).ReadString(64);
            Password = ReadString(64);
            Server = ReadString(16);
            MacAddress = ReadString(12);

            //Unknown byte = SeekForward(30)
            //SRP6 info = SeekForward(49), 64 bytes
            return true;
        }

        public override void Handle(IClient client)
        {
            //TODO: Better method for exchange IDs
            //TODO: Send account already logged in

            client.UserData = AccountManager.Instance.FindAccount(x => x.Username == Username);

            if (client.UserData != null && client.UserData.Password.Equals(Password) && !client.UserData.Online)
            {
                client.UserData.MacAddress = MacAddress;
                client.UserData.LastLogin = DateTime.Now;
                client.UserData.LastIP = client.ClientAddress;

                using (
                    var packet = new AuthenticationAction(client.UserData.Guid, client.UserData.Password.GetHashCode(),
                        GlobalDefaults.Default.TestServerPort, GlobalDefaults.Default.TestServerIp))
                    client.Send(packet);
            }
            else

                using (var packet = new AuthenticationAction((int) AuthenticationAction.Type.InvalidCredentials))
                    client.Send(packet);
        }
    }
}
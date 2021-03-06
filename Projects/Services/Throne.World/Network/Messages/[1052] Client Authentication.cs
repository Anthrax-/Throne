﻿using Throne.Framework.Network.Connectivity;
using Throne.Framework.Network.Transmission;
using Throne.Framework.Network.Transmission.Stream;
using Throne.Framework.Security.Permissions;
using Throne.World.Network.Handling;
using Throne.World.Properties;
using Throne.World.Records;

namespace Throne.World.Network.Messages
{
    [WorldPacketHandler(PacketTypes.AuthenticateClient)]
    public class ClientAuthentication : WorldPacket
    {
        private int _password, _session;

        /// <summary>
        ///     Incoming constructor.
        /// </summary>
        /// <param name="array">Incoming byte array.</param>
        public ClientAuthentication(byte[] array) : base(array)
        {
        }

        public override bool Read(IClient client)
        {
            _password = ReadInt();
            _session = ReadInt();
            return true;
        }

        public override async void Handle(IClient client)
        {
            await WorldServer.Instance.AccountService.PostWait(
                asvc => asvc.Call(accService =>
                {
                    if (!accService.Authorize(_session, _password))
                        return;

                    client.UserData = accService.GetAccount(_session);
                    client.AddPermission(new AuthenticatedPermission());
                }));

            if (client.HasPermission(typeof (AuthenticatedPermission)))
            {
                CharacterRecord @char = CharacterManager.Instance.FindCharacterRecord(client);
                if (@char == null)
                    using (new Stream()
                           + Constants.LoginMessages.NewRole
                           + Constants.LoginMessages.ServerInfo
                           > client) return;

                ((WorldClient) client).SetCharacter(CharacterManager.Instance.InitiaizeCharacter(client, @char));
            }
            else
                client.DisconnectWithMessage(Constants.LoginMessages.BadAuthentication);
        }
    }
}
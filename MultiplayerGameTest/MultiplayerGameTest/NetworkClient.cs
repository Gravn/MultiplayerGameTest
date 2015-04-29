using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace MultiplayerGameTest
{
    class NetworkClient
    {
        static NetClient Client;

        static List<GameObject> Players;

        enum PacketType
        { 
            LOGIN,
            MOVE,
            WORLDSTATE
        }

        public NetworkClient()
        {
            NetPeerConfiguration Config = new NetPeerConfiguration("testGame");

            Client = new NetClient(Config);

            NetOutgoingMessage outmsg = Client.CreateMessage();

            Client.Start();

            outmsg.Write((byte)PacketType.LOGIN);
            outmsg.Write("PlayerName");

            Client.Connect("localhost", 14242, outmsg);

            Players = new List<GameObject>();
        }

        public void WaitForStartingInfo()
        {
            bool CanStart = false;

            NetIncomingMessage inc;

            while (!CanStart)
            {
                if ((inc = Client.ReadMessage()) != null)
                {
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                            if (inc.ReadByte() == (byte)PacketType.WORLDSTATE)
                            {
                                Players.Clear();
                                int count = 0;

                                count = inc.ReadInt32();

                                for (int i = 0; i < count; i++)
                                { 
                                    //add other players ships to gamemanager.
                                }
                                CanStart = true;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }

        }
    }
}

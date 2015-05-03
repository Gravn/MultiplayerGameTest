using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Lidgren.Network;

namespace MultiplayerGameTest
{
    public class NetworkClient
    {
        public NetClient Client;

        //static List<GameObject> Players;

        enum PacketType
        { 
            LOGIN,
            MOVE,
            WORLDSTATE
        }

        public NetworkClient()
        {
            NetPeerConfiguration Config = new NetPeerConfiguration("TestGame");

            Client = new NetClient(Config);

            NetOutgoingMessage outmsg = Client.CreateMessage();

            Client.Start();

            outmsg.Write((byte)PacketType.LOGIN);
            outmsg.Write("PlayerName");

            Client.Connect("localhost", 14242, outmsg);
            WaitForStartingInfo();

            //Players = new List<GameObject>();
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
                                //Players.Clear();
                                int count = 0;

                                count = inc.ReadInt32();

                                for (int i = 0; i < count; i++)
                                {
                                    //add other players ships to gamemanager.
                                    GameObject player = new Ship(new Vector2(900-32,450-32), Vector2.Zero, Vector2.Zero, GameManager.spr_ship);
                                    inc.ReadAllProperties(player);
                                    GameManager.GameObjects.Add(player);
                                    //Players.Add(player);
                                    
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

        public void CheckServerMessages()
        {
            NetIncomingMessage inc;

            while ((inc = Client.ReadMessage()) != null)
            {
                if (inc.MessageType == NetIncomingMessageType.Data)
                {
                    if (inc.ReadByte() == (byte)PacketType.WORLDSTATE)
                    {
                        int i = 0;
                        i = inc.ReadInt32();
                        for (int j = 0; j < i; j++)
                        {
                            GameObject player = new Ship(new Vector2(0,0),Vector2.Zero,Vector2.Zero,GameManager.spr_ship);
                            inc.ReadAllProperties(player);
                            GameManager.GameObjects.Add(player);
                        }
                    }
                }
            }
        }

        public void GetInputAndSendItToServer()
        { 
            //Send position and velocity.
        }
    }
}

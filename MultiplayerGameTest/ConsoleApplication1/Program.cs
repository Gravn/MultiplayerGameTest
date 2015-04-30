using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;

namespace ConsoleApplication1
{
    class Program
    {
        static NetServer Server;

        static NetPeerConfiguration Config;

        static void Main(string[] args)
        {
            Config = new NetPeerConfiguration("TestGame");

            Config.Port = 14242;

            Config.MaximumConnections = 16;

            Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            Server = new NetServer(Config);

            Server.Start();
            Console.WriteLine("Server Started");

            List<PlayerObject> Players = new List<PlayerObject>();

            NetIncomingMessage inc;
            DateTime time = DateTime.Now;

            TimeSpan timetopass = new TimeSpan(0, 0, 0, 0, 30);

            while (true)
            { 
                if((inc =Server.ReadMessage()) != null)
                {
                    switch(inc.MessageType)
                    {
                        case NetIncomingMessageType.ConnectionApproval:
                            if(inc.ReadByte() == (byte)PacketType.LOGIN)
                            {
                                inc.SenderConnection.Approve();
                                Random r = new Random();
                                Players.Add(new PlayerObject(900-32,450-32,0,0,0,0,inc.SenderConnection));
                                NetOutgoingMessage outmsg = Server.CreateMessage();
                                Console.WriteLine("Player joined");
                                outmsg.Write((byte)PacketType.WORLDSTATE);

                                outmsg.Write(Players.Count);

                                foreach (PlayerObject player in Players)
                                {
                                    outmsg.WriteAllProperties(player);
                                }

                                Server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered,0);
                            }
                            break;
                        case NetIncomingMessageType.Data:
                            if (inc.ReadByte() == (byte)PacketType.MOVE)
                            {
                                foreach (PlayerObject player in Players)
                                {
                                    if (player.Connection != inc.SenderConnection)
                                    {
                                        continue;
                                    }

                                    byte b = inc.ReadByte();

                                    //handle input


                                    //end

                                    NetOutgoingMessage outmsg = Server.CreateMessage();

                                    outmsg.Write((byte)PacketType.WORLDSTATE);

                                    outmsg.Write(Players.Count);

                                    foreach (PlayerObject ch2 in Players)
                                    {
                                        outmsg.WriteAllProperties(ch2);
                                    }

                                    Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                                    break;
                                }
                            }
                            break;

                        case NetIncomingMessageType.StatusChanged:
                            if(inc.SenderConnection.Status == NetConnectionStatus.Disconnected || inc.SenderConnection.Status == NetConnectionStatus.Disconnecting)
                            {
                                foreach(PlayerObject cha in Players )
                                {
                                       Players.Remove(cha);
                                        break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                if ((time + timetopass) < DateTime.Now)
                {
                    if (Server.ConnectionsCount != 0)
                    {
                        NetOutgoingMessage outmsg = Server.CreateMessage();

                        outmsg.Write((byte)PacketType.WORLDSTATE);

                        outmsg.Write(Players.Count);

                        foreach (PlayerObject ch2 in Players)
                        {
                            outmsg.WriteAllProperties(ch2);
                        }

                        Server.SendMessage(outmsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                    }
                    time = DateTime.Now;
                }
            }
        }

        enum PacketType
        { 
            LOGIN,
            MOVE,
            WORLDSTATE
        }
    }

    class PlayerObject
    {
        //Position
        public int xPos { get; set; }
        public int yPos { get; set; }

        //Velocity
        public int xVel { get; set; }
        public int yVel { get; set; }

        //Direction
        public int xDir { get; set; }
        public int yDir { get; set; }


        public NetConnection Connection { get; set; }
        public PlayerObject(int xPos, int yPos, int xVel, int yVel, int xDir, int yDir, NetConnection conn)
        {
            this.xPos = xPos;
            this.yPos = yPos;

            this.xVel = xVel;
            this.yVel = yVel;

            this.xDir = xDir;
            this.yDir = yDir;
            Connection = conn;
        }
    }
}

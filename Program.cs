using System;
using System.Threading;
using WindBot.Game;
using WindBot.Game.AI;
using WindBot.Game.Data;

namespace WindBot
{
    public class Program
    {
        public const short ProVersion = 0x1334;

        public static Random Rand;

        public static void Main(string[] args)
        {
#if !DEBUG
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex);
            }
#else
            Run(args[0], args[1], args[2], int.Parse(args[3]), args[4]);
#endif
        }

        private static void Run(String username, String deck, String serverIP, int serverPort,String password)
        {
            Rand = new Random();
            CardsManager.Init();
            DecksManager.Init();

            // Start two clients and connect them to the same room. Which deck is gonna win?
            GameClient clientA = new GameClient(username, deck, serverIP, serverPort, password);
            clientA.Start();
            while (clientA.Connection.IsConnected)
            {
                clientA.Tick();
                Thread.Sleep(1);
            }
        }
    }
}

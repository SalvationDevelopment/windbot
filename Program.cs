using OCGWrapper;
using System;
using System.IO;
using System.Threading;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot
{
    public class Program
    {
        public const short ProVersion = 0x1338;

        internal static Random Rand;
        
        public static void Main(string[] args)
        {
#if !DEBUG
            try
            {
                private static void Run(String username, String deck, String serverIP, int serverPort,String password);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex);
            }
#else
           private static void Run(String username, String deck, String serverIP, int serverPort,String password);
#endif
        }

        public static void Init(string databasePath)
        {
            Rand = new Random();
            DecksManager.Init();
            InitCardsManager(databasePath);
        }
        
        private static void Run()
        {
            Init("cards.cdb");

            // Start one client and connect it to the the requested server.
            GameClient clientA = new GameClient(username, deck, serverIP, serverPort, password);
            clientA.Start();
           
            while (clientA.Connection.IsConnected || clientB.Connection.IsConnected)
            {
                clientA.Tick();
                Thread.Sleep(1);
            }
        }

        private static void InitCardsManager(string databasePath)
        {
            string currentPath = Path.GetFullPath(".");
            string absolutePath = Path.Combine(currentPath, databasePath);
            NamedCardsManager.Init(absolutePath);
        }
    }
}

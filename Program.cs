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

        public static Random Rand;

        public static void Main(string[] args)
        {
#if !DEBUG
            try
            {
                Run(args[0], args[1], args[2], int.Parse(args[3]), args[4]);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex);
            }
#else
            Run(args[0], args[1], args[2], int.Parse(args[3]), args[4]);
#endif
        }

        public static void Init(string databasePath)
        {
            Rand = new Random();
            DecksManager.Init();
            InitCardsManager(databasePath);
        }
        
        private static void Run(String username, String deck, String serverIP, int serverPort, String password)
        {
            Init("cards.cdb");


            // Start one client and connect it to the the requested server.
            GameClient clientA = new GameClient(username, deck, serverIP, serverPort, password);
            clientA.Start();
         
            while (clientA.Connection.IsConnected)
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

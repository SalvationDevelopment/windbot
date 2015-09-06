using System;
using System.Threading;
using System.Runtime.InteropServices;
using WindBot.Game;
using WindBot.Game.AI;
using WindBot.Game.Data;

public static class Lua
{
    //some Lua defines
    /* option for multiple returns in `lua_pcall' and `lua_call' */
    public const int LUA_MULTRET = (-1);

    //the first of any calls to Lua.
    //get a valid Lua state to operate on
    [DllImport("lua514.dll")]
    public static extern IntPtr luaL_newstate();

    //open all Lua libraries
    [DllImport("lua.dll")]
    public static extern void luaL_openlibs(IntPtr lua_State);

    //close Lua
    [DllImport("lua.dll")]
    public static extern void lua_close(IntPtr lua_State);

    //load a Lua script string into the Lua state
    [DllImport("lua.dll")]
    public static extern int luaL_loadstring(IntPtr lua_State, string s);

    //call a lua function, a function can be a Lua script loaded into the Lua state
    [DllImport("lua.dll")]
    public static extern int lua_pcall(IntPtr lua_State, int nargs, int nresults, int errfunc);

    //simplify the execution of a Lua script
    public static int luaL_dostring(IntPtr lua_State, string s)
    {
        if (luaL_loadstring(lua_State, s) != 0)
            return 1;
        return lua_pcall(lua_State, 0, LUA_MULTRET, 0);
    }

    /*
    public static void LogError()
    {
       //log error
       string errMsg = null;
       if (Lua.lua_isstring(m_lua_State, -1) &gt; 0)
           errMsg = Lua.lua_tostring(m_lua_State, -1);
  
       //clear the Lua stack
       Lua.lua_settop(m_lua_State, 0);
          
       //log or show the error somewhere in your program
    }
    */
}
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

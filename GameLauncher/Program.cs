using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Owlboy;
using Owlboy.ScreenManagement;

namespace OwlboyAutoSplitter
{
    internal class Program
    {
        private static Game1 game1 = new Game1();

        [STAThread]
        private static void Main(string[] args)
        {
            UInt32 state = getState();
            Thread thread = new Thread(new Program().Watch);
            thread.Start();
            game1.Run();
            thread.Abort();
        }

        [STAThread]
        public void Watch()
        {
            Thread.Sleep(5000);
            Console.WriteLine("# Owlboy AutoSplitter " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("# Made by OsteHovel");
            Console.WriteLine("# Improved by ViresMajores");
            Console.WriteLine("#");
            Console.WriteLine("# LiveSplit integration for removal of loading time");
            Console.WriteLine("#");

            State state = State.Startup;
            while (true)
            {
                Thread.Sleep(16);
                bool isLoading = false;
                bool isAtMenuScreen = false;


                ScreenManager screenManager = Game1.screenManager;
                if (screenManager != null)
                {
                    foreach (object screen in screenManager.GetScreens())
                    {
                        string name = screen.GetType().Name;
                        if (name.Equals("LoadingScreen"))
                            isLoading = true;
                        else if (name.Equals("MenuScreen"))
                            isAtMenuScreen = true;
                    }

                    if (isAtMenuScreen)
                    {
                        state = State.MainMenu;
                    }
                    else
                    {
                        if (!isLoading && state == State.MainMenu)
                        {
                            Console.WriteLine("Starting the timer");
                            state = State.Ingame;
                            setState(1U);
                        }
                        else if (isLoading && state == State.Ingame)
                        {
                            Console.WriteLine("Pausing gametime");
                            state = State.Loading;
                            setState(2U);
                        }
                        else if (!isLoading && state == State.Loading)
                        {
                            Console.WriteLine("Unpause gametime");
                            state = State.Ingame;
                            setState(3U);
                        }
                    }
                }
            }
        }

        internal enum State
        {
            Startup,
            MainMenu,
            Loading,
            Ingame
        }

        [DllImport("OwlBoyLiveSplitLibrary.dll", CharSet = CharSet.Unicode)]
        private static extern UInt32 getState();

        [DllImport("OwlBoyLiveSplitLibrary.dll", CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.Cdecl)]
        private static extern void setState(UInt32 state);
    }
}
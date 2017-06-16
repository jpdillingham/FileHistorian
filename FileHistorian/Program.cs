using System;
using System.ServiceProcess;

namespace FileHistorian
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "--install")
                {
                    Utility.ModifyService("install");
                }
                else if (args[0] == "--uninstall")
                {
                    Utility.ModifyService("uninstall");
                }
            }
            else
            {
                // if the platform is Windows and Environment.UserInteractive is false,
                // the application is being started as a service.
                if (Utility.IsWindows() && (!Environment.UserInteractive))
                {
                    ServiceBase.Run(new Service());
                }
                else
                {
                    // the application is being run under user mode.
                    Start(args);
                    Stop();
                }
            }
        }

        /// <summary>
        ///     Contains the application's startup logic.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        internal static void Start(string[] args)
        {
            Console.WriteLine("Started!");

            // application logic here
        }

        /// <summary>
        ///     Contains the application's shutdown logic.
        /// </summary>
        internal static void Stop()
        {
            Console.WriteLine("Stopped.");
        }
    }
}
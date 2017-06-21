using FileHistorian.Data;
using FileHistorian.Data.Entities;
using System;
using System.Collections.Generic;
using System.ServiceProcess;

namespace FileHistorian
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    internal class Program
    {
        #region Internal Methods

        /// <summary>
        ///     Contains the application's startup logic.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        internal static void Start(string[] args)
        {
            Console.WriteLine("Started!");

            try
            {
                using (Context context = new Context())
                {
                    Console.WriteLine("Adding scan...");

                    Scan scan = new Scan();

                    scan.Start = DateTime.Now;
                    scan.End = DateTime.Now;

                    scan.Files = new List<File>();

                    scan.Files.Add(new File() { FullName = @"\path\to\file.ext", CreatedOn = DateTime.Now, ModifiedOn = DateTime.Now, AccessedOn = DateTime.Now });

                    Console.WriteLine("Scan constructed.  Adding to context...");

                    context.Scans.Add(scan);

                    context.SaveChanges();

                    Console.WriteLine("Added scan!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Contains the application's shutdown logic.
        /// </summary>
        internal static void Stop()
        {
            Console.WriteLine("Stopped.");
        }

        #endregion Internal Methods

        #region Private Methods

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
                // if the platform is Windows and Environment.UserInteractive is false, the application is being started as a service.
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

        #endregion Private Methods
    }
}
/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using FileHistorian.CommandLine;
using FileHistorian.Data;
using FileHistorian.Data.Entities;
using FileHistorian.Services;
using NLog;

namespace FileHistorian
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    internal class Program
    {
        #region Private Fields

        private static List<string> directories = new List<string>();

        /// <summary>
        ///     The logger for the class.
        /// </summary>
        private static Logger log = LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Private Properties

        [Argument('i', "install-service")]
        private static bool InstallService { get; set; }

        [Argument('o', "run-once")]
        private static bool RunOnce { get; set; }

        [Argument('u', "uninstall-service")]
        private static bool UninstallService { get; set; }

        #endregion Private Properties

        #region Internal Methods

        /// <summary>
        ///     Contains the application's startup logic.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        internal static void Start(string[] args)
        {
            log.Info("Application Started.");

            try
            {
                Task.Run(() => Scan(directories));

                Console.ReadKey();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        ///     Contains the application's shutdown logic.
        /// </summary>
        internal static void Stop()
        {
            log.Info("Application stopped.");
        }

        #endregion Internal Methods

        #region Private Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        private static void Main(string[] args)
        {
            Arguments.Populate();

            if (InstallService)
            {
                Utility.ModifyService("install");
            }
            else if (UninstallService)
            {
                Utility.ModifyService("uninstall");
            }
            else
            {
                LoadConfiguration();

                // if the platform is Windows and Environment.UserInteractive is false, the application is being started as a service.
                if (Utility.IsWindows() && (!Environment.UserInteractive))
                {
                    ServiceBase.Run(new Service());
                }
                else
                {
                    // the application is being run under user mode. if the RunOnce flag is specified, perform one scan, then quit.
                    // Otherwise, start the application.
                    if (RunOnce)
                    {
                        Task.Run(() => Scan(directories)).GetAwaiter().GetResult();
                    }
                    else
                    {
                        Start(args);
                        Stop();
                    }
                }
            }
        }

        #endregion Private Methods

        /// <summary>
        ///     Loads the application configuration from App.config.
        /// </summary>
        private static void LoadConfiguration()
        {
            var section = (FileHistorianConfigurationSection)ConfigurationManager.GetSection("fileHistorian");
            directories = section.Directories.Select(d => d.Path).ToList();
        }

        private static async Task Scan(List<string> directories)
        {
            log.Info("Starting scan...");

            using (Context context = new Context())
            {
                log.Info("Initializing scanner...");

                Scanner scanner = new Scanner();

                log.Info("Initiating scan...");
                Scan scan = await scanner.ScanAsync(directories);

                log.Info("Saving scan results...");
                context.Scans.Add(scan);
                await context.SaveChangesAsync();
            }

            log.Info("Scan complete.");
        }
    }
}
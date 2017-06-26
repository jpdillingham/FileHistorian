/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System;
using System.Collections.Generic;
using System.ServiceProcess;
using FileHistorian.Data;
using FileHistorian.Data.Entities;
using NLog;
using System.Configuration;
using System.Linq;
using FileHistorian.CommandLine;
using System.Threading.Tasks;
using FileHistorian.Services;

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

        [Argument('i', "install-service")]
        private static bool InstallService { get; set; }

        [Argument('u', "uninstall-service")]
        private static bool UninstallService { get; set; }

        [Argument('o', "run-once")]
        private static bool RunOnce { get; set; }

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

                if (RunOnce)
                {
                    Task.Run(() => Scan(directories, false)).GetAwaiter().GetResult();
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
        }

        #endregion Private Methods

        private static void LoadConfiguration()
        {
            var section = (FileHistorianConfigurationSection)ConfigurationManager.GetSection("fileHistorian");

            directories = section.Directories.Select(d => d.Path).ToList();
        }

        private static async Task Scan(List<string> directories, bool async = true)
        {
            log.Info("Starting scan...");

            using (Context context = new Context())
            {
                Scanner scanner = new Scanner();

                Scan scan = new Scan();

                if (async)
                {
                    scan = await scanner.ScanAsync(directories);
                }
                else
                {
                    scan = scanner.Scan(directories);
                }

                context.Scans.Add(scan);

                if (async)
                {
                    await context.SaveChangesAsync();
                }
                else
                {
                    context.SaveChanges();
                }
            }

            log.Info("Scan complete.");
        }
    }
}
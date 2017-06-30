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
using System.Data.Entity.Migrations;

namespace FileHistorian
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    internal class Program
    {
        #region Private Fields

        /// <summary>
        ///     The database context for the application.
        /// </summary>
        private static Context context = new Context();

        /// <summary>
        ///     The list of directories specified in App.config.
        /// </summary>
        private static List<string> directories = new List<string>();

        /// <summary>
        ///     The logger for the class.
        /// </summary>
        private static Logger log = LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Private Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the Windows service should be installed.
        /// </summary>
        /// <remarks>Populated from command-line arguments.</remarks>
        [Argument('i', "install-service")]
        private static bool InstallService { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether a single scan should be executed, then the application stopped.
        /// </summary>
        /// <remarks>Populated from command-line arguments.</remarks>
        [Argument('o', "run-once")]
        private static bool RunOnce { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the Windows service should be uninstalled.
        /// </summary>
        /// <remarks>Populated from command-line arguments.</remarks>
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

                Console.WriteLine("Press any key to exit.");
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
        ///     Initializes the database context for the application and applies any outstanding migration(s).
        /// </summary>
        private static void InitializeContext()
        {
            context.Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

            log.Info("Initializing database...");

            context.Database.Initialize(true);

            DbMigrator migrator = new DbMigrator(new Data.Migrations.Configuration());

            if (migrator.GetPendingMigrations().Count() > 0)
            {
                log.Debug("Performing migration(s)...");

                migrator.Update();

                log.Debug("Migration(s) complete.");
            }
            else
            {
                log.Debug("Database is up to date.");
            }

            log.Info("Initialization complete.");
        }

        /// <summary>
        ///     Loads the application configuration from App.config.
        /// </summary>
        private static void LoadConfiguration()
        {
            var section = (FileHistorianConfigurationSection)ConfigurationManager.GetSection("fileHistorian");
            directories = section.Directories.Select(d => d.Path).ToList();
        }

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
                InitializeContext();

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

        /// <summary>
        ///     Executes a scan of each of the specified directories and saves the result to the database.
        /// </summary>
        /// <param name="directories">The list of directories to scan.</param>
        /// <returns>The asynchronous operation containing the scan.</returns>
        private static async Task Scan(List<string> directories)
        {
            log.Info("Starting scan...");

            log.Debug("Initializing scanner...");

            Scanner scanner = new Scanner();

            log.Debug("Initiating scan...");

            Scan scan = await scanner.ScanAsync(directories);

            log.Debug("Saving scan results...");

            context.Scans.Add(scan);
            await context.SaveChangesAsync();

            log.Info("Scan complete.");
        }

        #endregion Private Methods
    }
}
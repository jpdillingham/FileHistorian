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

namespace FileHistorian
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    internal class Program
    {
        #region Private Fields

        /// <summary>
        ///     The logger for the class.
        /// </summary>
        private static Logger log = LogManager.GetCurrentClassLogger();

        #endregion Private Fields

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
                using (Context context = new Context())
                {
                    log.Info("Adding scan...");

                    Scan scan = new Scan();

                    scan.Start = DateTime.Now;
                    scan.End = DateTime.Now;

                    scan.Files = new FileScanner().Scan(@"c:\pkg\");

                    log.Info("Scan constructed.  Adding to context...");

                    context.Scans.Add(scan);

                    context.SaveChanges();

                    log.Info("Added scan!");
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
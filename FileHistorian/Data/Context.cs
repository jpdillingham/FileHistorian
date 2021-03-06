﻿/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System.Configuration;
using System.Data.Entity;
using FileHistorian.Data.Entities;
using NLog;

namespace FileHistorian.Data
{
    /// <summary>
    ///     The database context for the application.
    /// </summary>
    public class Context : DbContext
    {
        #region Private Fields

        /// <summary>
        ///     The logger for the class.
        /// </summary>
        private static Logger log = LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
            : base("FileHistorian")
        {
            Configuration.AutoDetectChangesEnabled = false;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Gets or sets the list of <see cref="Exception"/> records for the context.
        /// </summary>
        public DbSet<Exception> Exceptions { get; set; }

        /// <summary>
        ///     Gets or sets the list of <see cref="File"/> records for the context.
        /// </summary>
        public DbSet<File> Files { get; set; }

        /// <summary>
        ///     Gets or sets the list of <see cref="Scan"/> records for the context.
        /// </summary>
        public DbSet<Scan> Scans { get; set; }

        #endregion Public Properties
    }
}
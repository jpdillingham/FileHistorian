/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System.Data.Entity.Migrations;

namespace FileHistorian.Data.Migrations
{
    /// <summary>
    ///     Entity Framework Migration configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "FileHistorian.Data.Context";
        }

        #endregion Public Constructors

        #region Protected Methods

        /// <summary>
        ///     Seeds the database with initial data.
        /// </summary>
        /// <param name="context">The Context for which data is to be seeded.</param>
        protected override void Seed(Context context)
        {
        }

        #endregion Protected Methods
    }
}
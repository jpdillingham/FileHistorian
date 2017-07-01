/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System.Data.Entity.Migrations;

namespace FileHistorian.Data.Migrations
{
    /// <summary>
    ///     A database migration.
    /// </summary>
    public partial class Datetime2Refactor : DbMigration
    {
        #region Public Methods

        /// <summary>
        ///     Downgrades the database to the previous state
        /// </summary>
        public override void Down()
        {
            AlterColumn("dbo.Files", "AccessedOn", c => c.DateTime());
            AlterColumn("dbo.Files", "ModifiedOn", c => c.DateTime());
            AlterColumn("dbo.Files", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Scans", "End", c => c.DateTime());
            AlterColumn("dbo.Scans", "Start", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Exceptions", "Timestamp", c => c.DateTime(nullable: false));
        }

        /// <summary>
        ///     Upgrades the database to the current state
        /// </summary>
        public override void Up()
        {
            AlterColumn("dbo.Exceptions", "Timestamp", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Scans", "Start", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Scans", "End", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Files", "CreatedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Files", "ModifiedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Files", "AccessedOn", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }

        #endregion Public Methods
    }
}
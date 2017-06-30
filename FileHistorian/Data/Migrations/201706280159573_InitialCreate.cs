using System.Data.Entity.Migrations;

namespace FileHistorian.Data.Migrations
{
    /// <summary>
    ///     A database migration.
    /// </summary>
    public partial class InitialCreate : DbMigration
    {
        #region Public Methods

        /// <summary>
        ///     Downgrades the database to the previous state
        /// </summary>
        public override void Down()
        {
            DropForeignKey("dbo.Exceptions", "ScanID", "dbo.Scans");
            DropForeignKey("dbo.Files", "ScanID", "dbo.Scans");
            DropIndex("dbo.Files", new[] { "ScanID" });
            DropIndex("dbo.Exceptions", new[] { "ScanID" });
            DropTable("dbo.Files");
            DropTable("dbo.Scans");
            DropTable("dbo.Exceptions");
        }

        /// <summary>
        ///     Upgrades the database to the current state
        /// </summary>
        public override void Up()
        {
            CreateTable(
                "dbo.Exceptions",
                c => new
                {
                    ScanID = c.Guid(nullable: false),
                    Timestamp = c.DateTime(nullable: false),
                    Message = c.String(maxLength: 500),
                })
                .PrimaryKey(t => new { t.ScanID, t.Timestamp })
                .ForeignKey("dbo.Scans", t => t.ScanID, cascadeDelete: true)
                .Index(t => t.ScanID);

            CreateTable(
                "dbo.Scans",
                c => new
                {
                    ScanID = c.Guid(nullable: false),
                    Start = c.DateTime(nullable: false),
                    End = c.DateTime(),
                })
                .PrimaryKey(t => t.ScanID);

            CreateTable(
                "dbo.Files",
                c => new
                {
                    ScanID = c.Guid(nullable: false),
                    FullName = c.String(nullable: false, maxLength: 260),
                    Size = c.Long(nullable: false),
                    CreatedOn = c.DateTime(),
                    ModifiedOn = c.DateTime(),
                    AccessedOn = c.DateTime(),
                })
                .PrimaryKey(t => new { t.ScanID, t.FullName })
                .ForeignKey("dbo.Scans", t => t.ScanID, cascadeDelete: true)
                .Index(t => t.ScanID);
        }

        #endregion Public Methods
    }
}
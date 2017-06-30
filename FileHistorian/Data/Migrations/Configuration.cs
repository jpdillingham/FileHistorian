using System.Data.Entity.Migrations;

namespace FileHistorian.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        #region Public Constructors

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "FileHistorian.Data.Context";
        }

        #endregion Public Constructors

        #region Protected Methods

        protected override void Seed(Context context)
        {
        }

        #endregion Protected Methods
    }
}
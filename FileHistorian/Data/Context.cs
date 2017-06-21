using FileHistorian.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Data
{
    public class Context : DbContext
    {
        #region Public Constructors

        public Context()
        {
            Database.Connection.ConnectionString = "Server=SQL;Database=FileHistorian;Trusted_Connection=True;";
        }

        #endregion Public Constructors

        #region Public Properties

        public DbSet<File> Files { get; set; }
        public DbSet<Scan> Scans { get; set; }

        #endregion Public Properties
    }
}
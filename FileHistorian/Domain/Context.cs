using FileHistorian.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Domain
{
    public class Context : DbContext
    {
        #region Public Properties

        public DbSet<File> Files { get; set; }
        public DbSet<Scan> Scans { get; set; }

        #endregion Public Properties
    }
}
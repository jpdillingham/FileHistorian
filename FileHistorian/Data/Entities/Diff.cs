using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHistorian.Data.Entities
{
    [NotMapped]
    public class Diff
    {
        #region Public Properties

        public List<File> Created { get; set; }
        public List<File> Deleted { get; set; }
        public List<File> Modified { get; set; }

        #endregion Public Properties
    }
}
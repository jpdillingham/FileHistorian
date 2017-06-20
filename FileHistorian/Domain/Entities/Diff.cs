using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Domain.Entities
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Data.Entities
{
    public class Scan
    {
        #region Public Constructors

        public Scan()
        {
            ScanID = Guid.NewGuid();
            Files = new List<File>();
        }

        #endregion Public Constructors

        #region Public Properties

        [Column(Order = 2)]
        public DateTime End { get; set; }

        public virtual List<File> Files { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid ScanID { get; set; }

        [Column(Order = 1)]
        public DateTime Start { get; set; }

        #endregion Public Properties
    }
}
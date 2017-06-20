﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Domain.Entities
{
    public class File
    {
        #region Public Properties

        [Column(Order = 5)]
        public DateTime AccessedOn { get; set; }

        [Column(Order = 3)]
        public DateTime CreatedOn { get; set; }

        [Key]
        [Required]
        [Column(Order = 1)]
        public string FullName { get; set; }

        [Column(Order = 4)]
        public DateTime ModifiedOn { get; set; }

        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(FullName);
            }
        }

        public string Path
        {
            get
            {
                return System.IO.Path.GetFullPath(FullName);
            }
        }

        public virtual Scan Scan { get; set; }

        [Key]
        [ForeignKey("Scan")]
        [Column(Order = 0)]
        public Guid ScanID { get; set; }

        [Column(Order = 2)]
        public long Size { get; set; }

        #endregion Public Properties
    }
}
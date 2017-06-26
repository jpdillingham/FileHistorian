﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHistorian.Data.Entities
{
    public class Exception
    {
        #region Public Properties

        [Key]
        [Column(Order = 1)]
        [MaxLength(260)]
        public string FullName { get; set; }

        [Column(Order = 2)]
        [MaxLength(500)]
        public string Message { get; set; }

        public virtual Scan Scan { get; set; }

        [Key]
        [ForeignKey("Scan")]
        [Column(Order = 0)]
        public Guid ScanID { get; set; }

        #endregion Public Properties
    }
}
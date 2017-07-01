/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHistorian.Data.Entities
{
    /// <summary>
    ///     A record of an Exception encountered during a scan of the filesystem.
    /// </summary>
    public class Exception
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the Exception message.
        /// </summary>
        [Column(Order = 2)]
        [MaxLength(500)]
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Scan"/> during which the Exception was encountered.
        /// </summary>
        public virtual Scan Scan { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Scan.ScanID"/> of the <see cref="Scan"/> during which the file was discovered.
        /// </summary>
        [ForeignKey("Scan")]
        [Column(Order = 0)]
        public Guid ScanID { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the time at which the Exception was encountered.
        /// </summary>
        [Column(Order = 1)]
        public DateTime Timestamp { get; set; }

        #endregion Public Properties
    }
}
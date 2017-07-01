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
        #region Public Fields

        /// <summary>
        ///     The maximum length of an Exception message.
        /// </summary>
        public const int MaxLength = 1000;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Exception"/> class.
        /// </summary>
        public Exception()
        {
            ExceptionID = Guid.NewGuid();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Gets or sets the unique identifer of the Exception.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public Guid ExceptionID { get; set; }

        /// <summary>
        ///     Gets or sets the Exception message.
        /// </summary>
        [Column(Order = 3)]
        [MaxLength(MaxLength)]
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Scan"/> during which the Exception was encountered.
        /// </summary>
        public virtual Scan Scan { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Scan.ScanID"/> of the <see cref="Scan"/> during which the file was discovered.
        /// </summary>
        [ForeignKey("Scan")]
        [Column(Order = 1)]
        public Guid ScanID { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the time at which the Exception was encountered.
        /// </summary>
        [Column(Order = 2)]
        public DateTime Timestamp { get; set; }

        #endregion Public Properties
    }
}
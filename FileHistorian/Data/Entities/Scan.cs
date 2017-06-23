/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHistorian.Data.Entities
{
    /// <summary>
    ///     Contains the results from a scan of the filesystem.
    /// </summary>
    public class Scan
    {
        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Scan"/> class.
        /// </summary>
        public Scan()
        {
            ScanID = Guid.NewGuid();
            Start = DateTime.Now;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Gets or sets the timestamp of the end of the scan.
        /// </summary>
        [Column(Order = 2)]
        public DateTime? End { get; set; }

        /// <summary>
        ///     Gets or sets the list of files found during the scan.
        /// </summary>
        public virtual List<File> Files { get; set; }

        /// <summary>
        ///     Gets or sets the unique identifier of the scan.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public Guid ScanID { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp of the start of the scan.
        /// </summary>
        [Column(Order = 1)]
        public DateTime Start { get; set; }

        #endregion Public Properties
    }
}
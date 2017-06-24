/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHistorian.Data.Entities
{
    /// <summary>
    ///     Provides lists of Files that have been newly created, deleted or modified between two Scans.
    /// </summary>
    [NotMapped]
    public class Diff
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the list of <see cref="File"/> records that have been newly created.
        /// </summary>
        public List<File> Created { get; set; }

        /// <summary>
        ///     Gets or sets the list of <see cref="File"/> records that have been deleted.
        /// </summary>
        public List<File> Deleted { get; set; }

        /// <summary>
        ///     Gets or sets the list of <see cref="File"/> records that have been modified.
        /// </summary>
        public List<File> Modified { get; set; }

        #endregion Public Properties
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileHistorian.Data.Entities
{
    /// <summary>
    ///     A record of a file found during a scan of the filesystem.
    /// </summary>
    public class File
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the timestamp at which the file was last accessed.
        /// </summary>
        [Column(Order = 5)]
        public DateTime? AccessedOn { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp at which the file was created.
        /// </summary>
        [Column(Order = 3)]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        ///     Gets or sets the full name and path of the file.
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [MaxLength(260)]
        public string FullName { get; set; }

        /// <summary>
        ///     Gets or sets the timestamp at which the file was last modified.
        /// </summary>
        [Column(Order = 4)]
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        ///     Gets the name of the file.
        /// </summary>
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(FullName);
            }
        }

        /// <summary>
        ///     Gets the path of the file.
        /// </summary>
        public string Path
        {
            get
            {
                return System.IO.Path.GetFullPath(FullName);
            }
        }

        /// <summary>
        ///     Gets or sets the <see cref="Scan"/> during which the file was discovered.
        /// </summary>
        public virtual Scan Scan { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Scan.ScanID"/> of the <see cref="Scan"/> during which the file was discovered.
        /// </summary>
        [Key]
        [ForeignKey("Scan")]
        [Column(Order = 0)]
        public Guid ScanID { get; set; }

        /// <summary>
        ///     Gets or sets the size of the file.
        /// </summary>
        [Column(Order = 2)]
        public long Size { get; set; }

        #endregion Public Properties
    }
}
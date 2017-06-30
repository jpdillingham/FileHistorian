/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace FileHistorian
{
    /// <summary>
    ///     The <see cref="ConfigurationElement"/> used to store directories in the application configuration.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]
    public class DirectoryElement : ConfigurationElement
    {
        #region Public Properties

        /// <summary>
        ///     Gets the path of the directory.
        /// </summary>
        [ConfigurationProperty("path", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string)this["path"]; }
        }

        #endregion Public Properties
    }

    /// <summary>
    ///     The collection of <see cref="DirectoryElement"/> used to store directories in the application configuration.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]
    public class DirectoryElementCollection : ConfigurationElementCollection, IEnumerable<DirectoryElement>
    {
        #region Public Methods

        /// <summary>
        ///     Returns an enumerator for the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public new IEnumerator<DirectoryElement> GetEnumerator()
        {
            foreach (var key in this.BaseGetAllKeys())
            {
                yield return (DirectoryElement)BaseGet(key);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        ///     Creates a new <see cref="ConfigurationElement"/> .
        /// </summary>
        /// <returns>The newly created ConfigurationElement.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new DirectoryElement();
        }

        /// <summary>
        ///     Retrieves the key for the specified <see cref="ConfigurationElement"/> .
        /// </summary>
        /// <param name="element">The ConfigurationElement for which the key is to be retrieved.</param>
        /// <returns>The retrieved key.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DirectoryElement)element).Path;
        }

        #endregion Protected Methods
    }

    /// <summary>
    ///     The custom <see cref="ConfigurationSection"/> for the application.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]
    public class FileHistorianConfigurationSection : ConfigurationSection
    {
        #region Public Properties

        /// <summary>
        ///     Gets the collection of <see cref="DirectoryElement"/> used to store directories in the application configuration.
        /// </summary>
        [ConfigurationProperty("Directories")]
        public DirectoryElementCollection Directories
        {
            get { return (DirectoryElementCollection)this["Directories"]; }
        }

        /// <summary>
        ///     Gets the <see cref="ConfigurationElement"/> used to store the scan time configuration.
        /// </summary>
        [ConfigurationProperty("ScanTime")]
        public ScanTimeElement ScanTime
        {
            get { return (ScanTimeElement)this["ScanTime"]; }
        }

        #endregion Public Properties
    }

    /// <summary>
    ///     The <see cref="ConfigurationElement"/> used to store the scan time configuration.
    /// </summary>
    public class ScanTimeElement : ConfigurationElement
    {
        #region Public Properties

        /// <summary>
        ///     Gets a TimeSpan representing the offset from midnight at which the scan should be started.
        /// </summary>
        [ConfigurationProperty("midnightOffset", IsRequired = true)]
        [TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "23:59:59")]
        public TimeSpan MidnightOffset
        {
            get { return (TimeSpan)this["midnightOffset"]; }
        }

        #endregion Public Properties
    }
}
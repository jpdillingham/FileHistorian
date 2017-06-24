/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using FileHistorian.Data.Entities;

namespace FileHistorian.Data.Services
{
    /// <summary>
    ///     Given two <see cref="Scan"/> objects, compares them and returns a <see cref="Diff"/> containing the differences.
    /// </summary>
    public class DiffFactory : IDiffFactory
    {
        #region Public Methods

        /// <summary>
        ///     Computes the difference between the two provided <see cref="Scan"/> objects and returns a <see cref="Diff"/>
        ///     containing the differences.
        /// </summary>
        /// <param name="older">The older of the two Scans to compare.</param>
        /// <param name="newer">The newer of the two Scans to compare.</param>
        /// <returns>A Diff containing the differences between the two provided Scans.</returns>
        public Diff GetDiff(Scan older, Scan newer)
        {
            // TODO: implement this
            return new Diff();
        }

        #endregion Public Methods
    }
}
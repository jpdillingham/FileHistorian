﻿using FileHistorian.Data.Entities;

namespace FileHistorian.Data.Services
{
    public class DiffFactory : IDiffFactory
    {
        #region Public Methods

        public Diff GetDiff(Scan left, Scan right)
        {
            return new Diff();
        }

        #endregion Public Methods
    }
}
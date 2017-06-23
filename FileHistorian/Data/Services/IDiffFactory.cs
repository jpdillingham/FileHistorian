/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using FileHistorian.Data.Entities;

namespace FileHistorian.Data.Services
{
    public interface IDiffFactory
    {
        #region Public Methods

        Diff GetDiff(Scan left, Scan right);

        #endregion Public Methods
    }
}
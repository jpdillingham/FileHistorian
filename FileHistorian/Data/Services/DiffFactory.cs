using FileHistorian.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
using FileHistorian.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Domain.Services
{
    public interface IDiffFactory
    {
        #region Public Methods

        Diff GetDiff(Scan left, Scan right);

        #endregion Public Methods
    }
}
using FileHistorian.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian
{
    public class FileScanner
    {
        #region Public Methods

        public List<File> Scan(string directory)
        {
            List<File> retVal = new List<File>();

            string[] fileList = System.IO.Directory.GetFiles(directory, "*", System.IO.SearchOption.AllDirectories);

            foreach (string file in fileList)
            {
                retVal.Add(CreateFile(file));
            }

            return retVal;
        }

        public Task<List<Data.Entities.File>> ScanAsync(string directory)
        {
            return new Task<List<Data.Entities.File>>(() => Scan(directory));
        }

        #endregion Public Methods

        #region Private Methods

        private File CreateFile(string file)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

            File retVal = new File();
            retVal.FullName = fileInfo.FullName;
            retVal.Size = fileInfo.Length;
            retVal.CreatedOn = fileInfo.CreationTime;
            retVal.ModifiedOn = fileInfo.LastWriteTime;
            retVal.AccessedOn = fileInfo.LastAccessTime;

            return retVal;
        }

        #endregion Private Methods
    }
}
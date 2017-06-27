using System.Collections.Generic;
using System.Threading.Tasks;
using FileHistorian.Data.Entities;

namespace FileHistorian.Services
{
    public class Scanner
    {
        #region Public Methods

        public Scan Scan(List<string> directories)
        {
            Scan scan = new Scan();
            scan.Start = System.DateTime.Now;
            scan.Files = new List<File>();
            scan.Exceptions = new List<Exception>();

            try
            {
                foreach (string directory in directories)
                {
                    string[] fileList = System.IO.Directory.GetFiles(directory, "*", System.IO.SearchOption.AllDirectories);

                    foreach (string file in fileList)
                    {
                        scan.Files.Add(GetFile(file));
                    }
                }
            }
            catch (System.Exception ex)
            {
                scan.Exceptions.Add(new Exception() { Timestamp = System.DateTime.Now, Message = ex.Message });
            }

            return scan;
        }

        public Task<Scan> ScanAsync(List<string> directories)
        {
            return new Task<Scan>(() => Scan(directories));
        }

        #endregion Public Methods

        #region Private Methods

        private File GetFile(string filename)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filename);

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
using System.Collections.Generic;
using System.Threading.Tasks;
using FileHistorian.Data.Entities;
using NLog;

namespace FileHistorian.Services
{
    public class Scanner
    {
        #region Private Fields

        /// <summary>
        ///     The logger for the class.
        /// </summary>
        private static Logger log = LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Public Methods

        public Scan Scan(List<string> directories)
        {
            Scan scan = new Scan();
            scan.Start = System.DateTime.Now;
            scan.Files = new List<File>();
            scan.Exceptions = new List<Exception>();

            foreach (string directory in directories)
            {
                log.Info($"Scanning directory '{directory}'...");

                string[] fileList = new string[] { };

                try
                {
                    fileList = System.IO.Directory.GetFiles(directory, "*", System.IO.SearchOption.AllDirectories);

                    foreach (string file in fileList)
                    {
                        try
                        {
                            log.Info($"File: {file}");
                            scan.Files.Add(GetFile(file));
                        }
                        catch (System.Exception ex)
                        {
                            scan.Exceptions.Add(GetException(ex));
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    scan.Exceptions.Add(GetException(ex));
                }
            }

            return scan;
        }

        public async Task<Scan> ScanAsync(List<string> directories)
        {
            return await Task.Run(() => Scan(directories));
        }

        #endregion Public Methods

        #region Private Methods

        private Exception GetException(System.Exception exception)
        {
            return new Exception()
            {
                Timestamp = System.DateTime.Now,
                Message = exception.Message
            };
        }

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
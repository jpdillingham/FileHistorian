/*
 * Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
 * The MIT License (MIT)
 */

using System.Collections.Generic;
using System.Threading.Tasks;
using FileHistorian.Data.Entities;
using NLog;

namespace FileHistorian.Services
{
    /// <summary>
    ///     Scans directories and generates <see cref="Scan"/> objects containing the results.
    /// </summary>
    public class Scanner
    {
        #region Private Fields

        /// <summary>
        ///     The logger for the class.
        /// </summary>
        private static Logger log = LogManager.GetCurrentClassLogger();

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        ///     Scans the specified list of directories and generates a <see cref="Scan"/> containing the results.
        /// </summary>
        /// <param name="directories">The list of directories to scan.</param>
        /// <returns>A Scan containing the result of the scan.</returns>
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

        /// <summary>
        ///     Asynchronously executes a scan.
        /// </summary>
        /// <param name="directories">The list of directories to scan.</param>
        /// <returns>A Scan containing the result of the scan.</returns>
        public async Task<Scan> ScanAsync(List<string> directories)
        {
            return await Task.Run(() => Scan(directories));
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Generates an <see cref="Exception"/> record from the specified <see cref="System.Exception"/>.
        /// </summary>
        /// <param name="exception">The originating Exception for which the Exception is to be generated.</param>
        /// <returns>The generated Exception record.</returns>
        private Exception GetException(System.Exception exception)
        {
            return new Exception()
            {
                Timestamp = System.DateTime.Now,
                Message = exception.Message
            };
        }

        /// <summary>
        ///     Generates a <see cref="File"/> record from the specified file.
        /// </summary>
        /// <param name="filename">The filename of the file for which the File record is to be generated.</param>
        /// <returns>The generated File record.</returns>
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
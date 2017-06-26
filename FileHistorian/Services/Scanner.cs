using FileHistorian.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHistorian.Services
{
    public class Scanner
    {
        private IFileFactory FileFactory { get; set; }

        public Scanner()
            : this(new FileFactory())
        {
        }

        public Scanner(IFileFactory fileFactory)
        {
            FileFactory = fileFactory;
        }

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
                        scan.Files.Add(FileFactory.GetFile(file));
                    }
                }
            }
            catch (System.Exception ex)
            {
                scan.Exceptions.Add(new Data.Entities.Exception() { Timestamp = System.DateTime.Now, Message = ex.Message });
            }

            return scan;
        }

        public Task<Scan> ScanAsync(List<string> directories)
        {
            return new Task<Scan>(() => Scan(directories));
        }

        #endregion Public Methods
    }
}
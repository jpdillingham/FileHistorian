using FileHistorian.Data.Entities;

namespace FileHistorian.Services
{
    public class FileFactory : IFileFactory
    {
        public File GetFile(string filename)
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
    }
}
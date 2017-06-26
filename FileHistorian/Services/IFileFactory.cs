using FileHistorian.Data.Entities;

namespace FileHistorian.Services
{
    public interface IFileFactory
    {
        File GetFile(string filename);
    }
}
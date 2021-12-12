using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDFtoExcel.Models;

namespace PDFtoExcel.Data
{
    public interface IFilesRepo
    {
        bool SaveChanges();

        Task<IEnumerable<LocalFile>> GetFilesAsync();
        Task<LocalFile> GetFileAsync(int id);
        Task CreateFileAsync(LocalFile file);
    }
}

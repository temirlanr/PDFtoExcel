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

        IEnumerable<File> GetFiles();
        File GetFile(int id);
        void Convert(File file);
    }
}

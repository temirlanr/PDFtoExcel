using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDFtoExcel.Models;

namespace PDFtoExcel.Data
{
    public class SqlFilesRepo : IFilesRepo
    {
        private readonly FilesContext _context;

        public SqlFilesRepo(FilesContext context)
        {
            _context = context;
        }

        public void Convert(File file)
        {
            throw new NotImplementedException();
        }

        public File GetFile(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<File> GetFiles()
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

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

        public void CreateFile(LocalFile file)
        {
            _context.Files.Add(file);
        }

        public LocalFile GetFile(int id)
        {
            return _context.Files.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<LocalFile> GetFiles()
        {
            return _context.Files.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

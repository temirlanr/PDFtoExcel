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

        public async Task CreateFileAsync(LocalFile file)
        {
            _context.Files.Add(file);
            await Task.CompletedTask;
        }

        public async Task<LocalFile> GetFileAsync(int id)
        {
            return await Task.FromResult(_context.Files.FirstOrDefault(f => f.Id == id));
        }

        public async Task<IEnumerable<LocalFile>> GetFilesAsync()
        {
            return await Task.FromResult(_context.Files.ToList());
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

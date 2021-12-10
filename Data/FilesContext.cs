using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDFtoExcel.Models;

namespace PDFtoExcel.Data
{
    public class FilesContext : DbContext
    {
        public FilesContext(DbContextOptions<FilesContext> opt) : base(opt)
        {

        }

        public DbSet<LocalFile> Files { set; get; }
    }
}

using PDFtoExcel.Dtos;
using PDFtoExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDFtoExcel
{
    public static class Extensions
    {
        public static FileDto AsDto(this LocalFile file)
        {
            return new FileDto
            {
                Id = file.Id,
                FirstName = file.FirstName,
                LastName = file.LastName,
                MyFileName = file.MyFileName
            };
        }
    }
}

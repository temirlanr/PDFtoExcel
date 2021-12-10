using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PDFtoExcel.Dtos
{
    public class FileDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { set; get; }

        public string MyFileName { set; get; }
    }
}

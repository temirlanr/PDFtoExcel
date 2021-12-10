using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PDFtoExcel.Dtos
{
    public class CreateFileDto
    {
        [Required]
        public string FirstName { set; get; }
        [Required]
        public string LastName { set; get; }
        
        public string MyFileName { set; get; }
    }
}

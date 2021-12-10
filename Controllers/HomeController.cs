using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PDFtoExcel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PDFtoExcel.Data;
using PDFtoExcel.Dtos;
using Aspose.Pdf;

namespace PDFtoExcel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFilesRepo _repository;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(ILogger<HomeController> logger, IFilesRepo repository, IHostingEnvironment environment)
        {
            _logger = logger;
            _repository = repository;
            hostingEnvironment = environment;
        }

        public IActionResult List()
        {
            return View(_repository.GetFiles());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Convert()
        {
            return base.View(new LocalFile());
        }

        // GET 
        [HttpGet]
        public IEnumerable<FileDto> GetFiles()
        {

            var files = (_repository.GetFiles())
                        .Select(file => file.AsDto());
            return files;
        }

        // GET /{id}
        [HttpGet]
        [Route("{id}")]
        public ActionResult<FileDto> GetFile(int id)
        {

            var file = _repository.GetFile(id);

            if (file is null)
            {
                return NotFound();
            }

            return file.AsDto();
        }

        // POST
        [HttpPost]
        public ActionResult<FileDto> Convert(LocalFile model)
        {
            // TO DO - other validations
            if (model.MyFile != null)
            {
                var uniqueFileName = GetUniqueFileName(model.MyFile.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var converts = Path.Combine(hostingEnvironment.WebRootPath, "converts");
                var filePath = Path.Combine(uploads, uniqueFileName);
                string excelFileName = uniqueFileName.Replace(".pdf", ".xlsx");
                string excelFileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var excelPath = Path.Combine(converts, excelFileName);
                model.MyFile.CopyTo(new FileStream(filePath, FileMode.Create));

                Stream stream = model.MyFile.OpenReadStream();
                Document pdfDocument = new(stream);
                ExcelSaveOptions excelsave = new();
                pdfDocument.Save(excelPath, excelsave);

                LocalFile file = new()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MyFileName = model.MyFile.FileName
                };

                _repository.CreateFile(file);
                _repository.SaveChanges();

                return PhysicalFile(excelPath, excelFileType, excelFileName);
            }

            return RedirectToAction("Error", "Home");
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

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
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IFilesRepo repository, IWebHostEnvironment environment)
        {
            _logger = logger;
            _repository = repository;
            _environment = environment;
        }

        public async Task<IActionResult> ListAsync()
        {
            return View(await _repository.GetFilesAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Convert()
        {
            return View(new LocalFile());
        }

        // GET 
        [HttpGet]
        [Route("/api/")]
        public async Task<IEnumerable<FileDto>> GetFilesAsync()
        {

            var files = (await _repository.GetFilesAsync()).Select(file => file.AsDto());
            return files;
        }

        // GET /{id}
        [HttpGet]
        [Route("/api/{id}")]
        public async Task<ActionResult<FileDto>> GetFileAsync(int id)
        {

            var file = await _repository.GetFileAsync(id);

            if (file is null)
            {
                return NotFound();
            }

            return file.AsDto();
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<FileDto>> ConvertAsync(LocalFile model)
        {
            
            if (model.MyFile != null)
            {

                if (Path.GetExtension(model.MyFile.FileName) != ".pdf")
                {
                    _logger.LogWarning("NotPDF");
                    return RedirectToAction("Convert", "Home");
                }

                var uniqueFileName = GetUniqueFileName(model.MyFile.FileName);
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                var converts = Path.Combine(_environment.WebRootPath, "converts");
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

                await _repository.CreateFileAsync(file);
                _repository.SaveChanges();

                return PhysicalFile(excelPath, excelFileType, excelFileName);
            }

            _logger.LogWarning("NullFile");
            return RedirectToAction("Convert", "Home");
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

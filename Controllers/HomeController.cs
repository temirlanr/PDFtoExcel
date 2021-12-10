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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Convert()
        {
            return base.View(new Models.File());
        }

        [HttpPost]
        public IActionResult Convert(Models.File model)
        {
            // do other validations on your model as needed
            if (model.MyFile != null)
            {
                var uniqueFileName = GetUniqueFileName(model.MyFile.FileName);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                var filePath = Path.Combine(uploads, uniqueFileName);
                model.MyFile.CopyTo(new FileStream(filePath, FileMode.Create));

                //to do : Save uniqueFileName  to your db table   
            }
            // to do  : Return something
            return RedirectToAction("Index", "Home");
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

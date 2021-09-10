using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using csv_uploader.Models;
using csv_uploader.Services;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace csv_uploader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CSVUploaderController : ControllerBase
    {
        private readonly CSVUploaderService _csvUploaderService;

        public CSVUploaderController(CSVUploaderService csvUploaderService)
        {
            _csvUploaderService = csvUploaderService;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // Handle File Upload
            if (file == null || file.Length == 0)  
                return Content("file not selected");
            var fileId = Guid.NewGuid();
            var path = Path.Combine(  
                Directory.GetCurrentDirectory(), "wwwroot", "uploads",  
                fileId + file.FileName);  
            using (var stream = new FileStream(path, FileMode.Create))  
            {  
                await file.CopyToAsync(stream);  
            }

            // Read Uploaded File
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                var users = csv.GetRecords<User>();
                await _csvUploaderService.ParseFile(users);
            }
            return Ok();
        }
    }
}

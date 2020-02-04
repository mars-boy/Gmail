using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ImageService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ImageService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private readonly ImageServiceConfig _imageServiceConfig;

        public ImageController(IOptionsSnapshot<ImageServiceConfig> imageServiceConfig ) {
            this._imageServiceConfig = imageServiceConfig.Value;
        }

        [Route("GetMainifest/{mpdName}")]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Get(string mpdName) {
            var res = PhysicalFile("C:\\Users\\sai\\Desktop\\Temp2\\" + mpdName, "application/dash+xml");
            return res;
        }

        [Route("GetMainifest/VideoFile/{fileName}")]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult VideoFile(string fileName) {
            //var stream = System.IO.File.OpenRead("C:\\Users\\sai\\Desktop\\Temp2\\" + fileName);
            var res = PhysicalFile("C:\\Users\\sai\\Desktop\\Temp2\\" + fileName, "video/webm");
            return res;
        }

        [Route("GetMainifest/AudioFile/{fileName}")]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult AudioFile(string fileName) {
            var res = PhysicalFile("C:\\Users\\sai\\Desktop\\Temp2\\" + fileName, "audio/weba");
            return res;
        }

        [Route("Upload")]
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] string parameters, IFormFile file) {
            var headers = HttpContext.Request.Headers;
            string fileName = headers["FileName"];
            long chunckId = Convert.ToInt64(headers["CurrentChunck"]);
            long totalChuncks = Convert.ToInt64(headers["TotalChuncks"]);
            var fileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var saveFilePath = Path.Combine(fileDirectory, fileName+".part");
            var finalFilePath = Path.Combine(fileDirectory, fileName);
            var fileMode = chunckId == 1 ? FileMode.Create : FileMode.Append;
            if (chunckId <= totalChuncks) {
                using (var stream = new FileStream(saveFilePath, fileMode)) {
                    await file.CopyToAsync(stream);
                }
                if (chunckId == totalChuncks)
                {
                    System.IO.File.Move(saveFilePath, finalFilePath);
                    System.IO.File.Delete(saveFilePath);
                }
            }
            return Ok();
        }
    }
}
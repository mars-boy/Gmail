using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageService.Models;
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


    }
}
using com.picsfeed.ImageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.picsfeed.ImageService.Services
{
    public interface IImageService
    {
        void PublishToRabbit(UploadModel uploadModel);
    }
}

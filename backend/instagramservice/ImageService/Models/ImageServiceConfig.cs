using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.picsfeed.ImageService.Models
{
    public class ImageServiceConfig
    {
        public string Secret { get; set; }
        public string ImageDBConnectionString { get; set; }
    }
}

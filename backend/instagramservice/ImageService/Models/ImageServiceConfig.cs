using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageService.Models
{
    public class ImageServiceConfig
    {
        public string Secret { get; set; }
        public string ImageDBConnectionString { get; set; }
    }
}

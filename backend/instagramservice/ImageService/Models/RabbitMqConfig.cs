using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace com.picsfeed.ImageService.Models
{
    public class RabbitMqConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public string QueueName { get; set; }
    }
}

using com.picsfeed.ImageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using System.Text;
using Newtonsoft.Json;

namespace com.picsfeed.ImageService.Services
{
    public class ImageService : IImageService
    {

        private readonly RabbitMqConfig _rabbitMqConfig;

        public ImageService(IOptionsSnapshot<RabbitMqConfig> rabbitMqConfig) {
            this._rabbitMqConfig = rabbitMqConfig.Value;
        }

        public void PublishToRabbit(UploadModel uploadModel)
        {
            var connectionFactory = new ConnectionFactory() {
                UserName = _rabbitMqConfig.UserName,
                Password = _rabbitMqConfig.Password,
                HostName = _rabbitMqConfig.Host
            };

            using (var connection = connectionFactory.CreateConnection()) {
                var model = connection.CreateModel();
                var properties = model.CreateBasicProperties();
                properties.Persistent = false;
                var uploadModelJson = JsonConvert.SerializeObject(uploadModel);
                byte[] message = Encoding.Default.GetBytes(uploadModelJson);
                model.BasicPublish(_rabbitMqConfig.ExchangeName, _rabbitMqConfig.RoutingKey,
                    properties, message);
            }
        }
    }
}

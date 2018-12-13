using System;
using System.Text;
using API.Core.Logging;
using API.EventBus.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace API.EventBus.Pusher
{
    public class RabbitMQPush
    {
        Eventbus _eventBus { get; set; }
        public Stoc data;
        public IConfigurationRoot Configuration { get; set; }

        public RabbitMQPush(Stoc _data)
        {
            this.data = _data;
        }
        public string Push()
        {
            try
            {
                Configuration = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                //EventBus
                var eventBusConfig = Configuration.GetSection("EventBus");
                this._eventBus = new Eventbus
                {
                    Port = int.Parse(eventBusConfig["Port"]),
                    HostName = eventBusConfig["HotName"]
                };


                var factory = new ConnectionFactory() { HostName = this._eventBus.HostName };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: data.Consumer,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var stocData = JsonConvert.SerializeObject(data);
                    var body = Encoding.UTF8.GetBytes(stocData);

                    channel.BasicPublish(exchange: "",
                                         routingKey: data.Consumer,
                                         basicProperties: null,
                                         body: body);
                    return $"[x] Sent {data.Consumer}";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogError("Event bus", ex);
                return $"[x] Event bus: Fail";
            }
        }
    }
}

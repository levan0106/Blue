using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using API.EventBus.Entities;
using API.Core.Logging;

namespace API.EventBus.Consumer
{
    public class RabbitMQListener
    {
        ConnectionFactory _factory { get; set; }
        IConnection _connection { get; set; }
        IModel _channel { get; set; }
        SignalR _signalR { get; set; }
        Eventbus _eventBus { get; set; }

        static HubConnection connectionSignalR;

        public IConfigurationRoot Configuration { get; set; }


        public RabbitMQListener()
        {
            try
            {
                Configuration = new ConfigurationBuilder()
                       .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       .Build();

                //EventBus
                var eventBusConfig = Configuration.GetSection("EventBus");
                _eventBus = new Eventbus
                {
                    Port = int.Parse(eventBusConfig["Port"]),
                    HostName = eventBusConfig["HotName"]
                };

                this._factory = new ConnectionFactory() { HostName = this._eventBus.HostName };
                this._connection = _factory.CreateConnection();
                this._channel = _connection.CreateModel();

                //SignalR
                var signalRConfig = Configuration.GetSection("SignalR");
                bool enableSignalR = false;
                bool.TryParse(signalRConfig["Enable"], out enableSignalR);
                this._signalR = new SignalR
                {
                    UseSignalR = enableSignalR,
                    HubUrl = signalRConfig["Url"],
                    EventName = signalRConfig["EventName"]
                };
            }
            catch (Exception ex)
            {
                LogManager.LogError("Event bus",ex);
            }
        }

        public void Register(string queueName)
        {
            if (this._signalR != null)
            {
                if (this._signalR.UseSignalR)
                {
                    //Trigger The SignalR
                    ConnectToSignalRHub(this._signalR.HubUrl).Wait();
                }

                _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(_channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    if (this._signalR.UseSignalR)
                    {
                        connectionSignalR.InvokeAsync(this._signalR.EventName, message);
                    }
                };
                _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }
            else
            {
                LogManager.LogError("SignalR setting is null");
            }
        }

        public void Deregister()
        {
            this._connection.Close();
        }
        public static async Task ConnectToSignalRHub(string url)
        {
            connectionSignalR = new HubConnectionBuilder()
               .WithUrl(url)
               .Build();
            await connectionSignalR.StartAsync();
        }
    }

    public static class RegisterEventBus
    {
        private static RabbitMQListener _listener { get; set; }
        private static string _queueName { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app, string queueName)
        {
            _queueName = queueName;
            _listener = app.ApplicationServices.GetService<RabbitMQListener>();

            var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();

            lifetime.ApplicationStarted.Register(OnStarted);

            //press Ctrl+C to reproduce if your app runs in Kestrel as a console app
            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            _listener.Register(_queueName);
        }

        private static void OnStopping()
        {
            _listener.Deregister();
        }
    }
}

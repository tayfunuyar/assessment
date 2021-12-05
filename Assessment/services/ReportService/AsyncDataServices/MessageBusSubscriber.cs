using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.EventProcessing;

namespace ReportService.AsyncDataServices
{

    public class MessageBusSubscriber : BackgroundService
    {
        private IConfiguration _configuration;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        private readonly IEventProcessor _eventProcessor;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor processor)
        {
            _eventProcessor = processor;
            _configuration = configuration;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(type: ExchangeType.Fanout, exchange: "trigger");
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName, exchange: "trigger", routingKey: "");
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Connection Shutdown");
        }
        public override void Dispose()
        {
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
            base.Dispose();
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ModuleHandle, ea) =>
            {
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };
               _channel.BasicConsume(queue: _queueName, autoAck:true,consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
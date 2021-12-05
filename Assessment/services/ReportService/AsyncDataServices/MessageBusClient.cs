
using System;
using System.Text;
using System.Text.Json;
using ReportService.Dtos;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace ReportService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutDown;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Couldn't connect to Message Bus: {ex.Message}");
                throw new Exception($"--> Couldn't connect to Message Bus: {ex.Message}");
            }
        }
        public void PublishNewReportRequest(ReportPublishedDto reportPublishedDto)
        {
            var message = JsonSerializer.Serialize(reportPublishedDto);
            if (_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message...");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connection is closed, not sending");
            }
        }
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);
            Console.WriteLine($"--> we have send {message}");
        }

        private void Dispose()
        {
            Console.WriteLine("--> MessageBus disposed.");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void RabbitMQ_ConnectionShutDown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> RabbitMQ Connection Shutdown");
        }


    }
}
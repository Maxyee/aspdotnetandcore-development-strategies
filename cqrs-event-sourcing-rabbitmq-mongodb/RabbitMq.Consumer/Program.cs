﻿using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMq.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            { 
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue", 
                durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                console.writeLine("Message: " + message);
            };

            channel.BasicConsume("demo-queue", true, consumer);
            Console.ReadLine();
        }
    }
}
// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");


ConnectionFactory factory = new();
factory.Uri = new("amqps://lhlfzvgo:***@crow.rmq.cloudamqp.com/lhlfzvgo");


using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers);

Console.WriteLine("Header value giriniz");
string value = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(
    queue: queueName,
    exchange: "header-exchange-example",
    routingKey: string.Empty,
    new Dictionary<string, object>()
    {
        ["no"] = value
    });

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.ReadLine();
// See https://aka.ms/new-console-template for more information
using MassTransit;
using RabbitMQ.ESB.MassTransit.Consumer.Consumers;

Console.WriteLine("Hello, World!");

string rabbitMQUri = "amqps://lhlfzvgo:***@crow.rmq.cloudamqp.com/lhlfzvgo";
string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

await bus.StartAsync();

Console.ReadLine();
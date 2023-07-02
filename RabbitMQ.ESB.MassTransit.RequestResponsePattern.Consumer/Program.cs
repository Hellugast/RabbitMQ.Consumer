// See https://aka.ms/new-console-template for more information
using MassTransit;
using RabbitMQ.ESB.MassTransit.RequestResponsePattern.Consumer.Consumers;
using RabbitMQ.ESB.MassTransit.Shared.RequestResponseMessages;

Console.WriteLine("Hello, World!");

string rabbitMQUri = "amqps://lhlfzvgo:***@crow.rmq.cloudamqp.com/lhlfzvgo";
string requestQueue = "request-queue";

var bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<RequestMessageConsumer>();
    });
});

await bus.StartAsync();

Console.ReadLine();

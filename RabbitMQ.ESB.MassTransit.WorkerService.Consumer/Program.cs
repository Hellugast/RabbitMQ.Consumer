
using MassTransit;
using RabbitMQ.ESB.MassTransit.WorkerService.Consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsumer>();

            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://lhlfzvgo:***@crow.rmq.cloudamqp.com/lhlfzvgo");

                _configurator.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));
            });
        });
    })
    .Build();

await host.RunAsync();

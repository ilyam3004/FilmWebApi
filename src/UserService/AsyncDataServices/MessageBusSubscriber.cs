using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserService.EventProcessing;

namespace UserService.AsyncDataServices;

public class MessageBusSubscriber : BackgroundService 
{
    private readonly IConfiguration _configuration;
    private readonly IEventProcessor _eventProcessor;
    private IConnection _connection;
    private IModel _channel;
    private QueueDeclareOk _queue;

    public MessageBusSubscriber(
        IConfiguration configuration, 
        IEventProcessor eventProcessor)
    {
        _configuration = configuration;
        _eventProcessor = eventProcessor;

        InitializeRabbitMq();
    }

    private void InitializeRabbitMq()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"]
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "request-queue",
            exclusive: false);

        Console.WriteLine("--> Listening the message bus...");

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("--> MessageBus Subscriber is starting");
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            Console.WriteLine($"Received request: {Encoding.UTF8.GetString(ea.Body.ToArray())}");

            var replyMessage = $"Reply from microservice: {Encoding.UTF8.GetString(ea.Body.ToArray())}";
            var replyBody = Encoding.UTF8.GetBytes(replyMessage);

            _channel.BasicPublish(
                exchange: "",
                routingKey: ea.BasicProperties.ReplyTo,
                null,
                body: replyBody);
            Console.WriteLine($"Sent reply: {replyMessage}");
        };

        _channel.BasicConsume(
            queue: "request-queue",
            autoAck: true,
            consumer: consumer);
        Console.WriteLine("--> MessageBus Subscriber is started after sending a message");

        return Task.CompletedTask;
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
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
}
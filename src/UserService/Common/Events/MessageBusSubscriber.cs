using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserService.EventProcessing;

namespace UserService.Common.Events;

public class MessageBusSubscriber : BackgroundService 
{
    private readonly IEventProcessor _eventProcessor;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private QueueDeclareOk _queue;

    public MessageBusSubscriber(
        IEventProcessor eventProcessor,
        IConnection connection)
    {
        _eventProcessor = eventProcessor;
        _connection = connection;
        _channel = connection.CreateModel();
        _queue = _channel.QueueDeclare(
            queue: "request-queue",
            exclusive: false);
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("--> MessageBus Subscriber is starting");
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            string request = Encoding.UTF8.GetString(ea.Body.ToArray());
            
            Console.WriteLine($"Received request: {request}");
            
            var replyMessage = _eventProcessor.ProcessEvent(request);
            var replyBody = Encoding.UTF8.GetBytes(replyMessage);

            _channel.BasicPublish(
                exchange: "",
                routingKey: ea.BasicProperties.ReplyTo,
                null,
                body: replyBody);
            Console.WriteLine($"Sent reply: {replyBody}");
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

public record DecodeTokenRequest(
    string Token);
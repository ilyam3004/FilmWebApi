using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WatchlistService.Dtos.Requests;

namespace WatchlistService.Common.Events;

public class MessageBusProducer : IMessageBusProducer
{
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;
    private QueueDeclareOk _replyQueue;

    public MessageBusProducer(
        IConfiguration configuration,
        IConnection connection)
    {
        _configuration = configuration;
        InitializeRabbitMQ();
    }

    private void InitializeRabbitMQ()
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQHost"],
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _replyQueue = _channel.QueueDeclare();
        _channel.QueueDeclare("", exclusive: true);
    }

    public async Task<string> PublishAuthMessage(AuthUserRequest request)
    {
        var message = JsonSerializer.Serialize(request);

        if (!_connection.IsOpen)
        {
            throw new InvalidOperationException();
        }

        var tsc = new TaskCompletionSource<string>();

        _channel.QueueDeclare(
                "request-queue", 
                exclusive: false);

        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tsc.SetResult(response);
            Console.WriteLine($"Received response: {response}");
        };
        
        _channel.BasicConsume(
            queue: _replyQueue.QueueName,
            autoAck: true,
            consumer: consumer);
        
        var body = Encoding.UTF8.GetBytes(message);
        var properties = _channel.CreateBasicProperties();
        
        properties.ReplyTo = _replyQueue.QueueName;
        properties.CorrelationId = Guid.NewGuid().ToString();
        
        _channel.BasicPublish(
            exchange: "",
            routingKey: "request-queue",
            basicProperties: properties,
            body: body);
        
        Console.WriteLine("Sending request: {0}", message);
        
        // Wait for the response or a timeout
        var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
        var completedTask = await Task.WhenAny(tsc.Task, timeoutTask);

        if (completedTask == timeoutTask)
        {
            throw new TimeoutException("The request timed out.");
        }
        
        var responseMesaage = await tsc.Task;
        return responseMesaage;
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}

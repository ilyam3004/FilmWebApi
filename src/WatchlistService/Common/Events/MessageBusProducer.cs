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

    public void PublishAuthUser(AuthUserRequest request)
    {
        var message = JsonSerializer.Serialize(request);

        if (_connection.IsOpen)
        {
            _channel.QueueDeclare(
                "request-queue", 
                exclusive: false);

            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
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
        }
        else
        {
            Console.WriteLine("Connection closed.");
        }
    }

    public async Task<string> PublishAuthMessage(AuthUserRequest request)
    {
        if (!_connection.IsOpen)
        {
            throw new InvalidOperationException("RabbitMQ connection is not open.");
        }

        var message = JsonSerializer.Serialize(request);

        var tcs = new TaskCompletionSource<string>();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.SetResult(response);
        };

        var correlationId = Guid.NewGuid().ToString();

        var properties = _channel.CreateBasicProperties();
        properties.ReplyTo = _replyQueue.QueueName;
        properties.CorrelationId = correlationId;

        _channel.BasicPublish(
            exchange: "",
            routingKey: "request-queue",
            basicProperties: properties,
            body: Encoding.UTF8.GetBytes(message));
        
        var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
        var completedTask = await Task.WhenAny(tcs.Task, timeoutTask);

        if (completedTask == timeoutTask)
        {
            throw new TimeoutException("The request timed out.");
        }

        var responseMessage = await tcs.Task;

        return responseMessage;
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

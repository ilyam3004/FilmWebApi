using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WatchlistService.Dtos.Requests;

namespace WatchlistService.Common.Events;

public class MessageBusProducer : IMessageBusProducer
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly QueueDeclareOk _replyQueue;

    public MessageBusProducer(
        IConfiguration configuration,
        IConnection connection)
    {
        _configuration = configuration;
        _connection = connection;
        _channel = _connection.CreateModel();
        _replyQueue = _channel.QueueDeclare("", exclusive: true);   
    }

    public string PublishAuthMessage(AuthUserRequest request)
    {
        _channel.QueueDeclare(
            "request-queue", 
            exclusive: false);

        var consumer = new EventingBasicConsumer(_channel);

        var response = "";

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            response = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Received response: {response}");
        };
        
        _channel.BasicConsume(
            queue: _replyQueue.QueueName,
            autoAck: true,
            consumer: consumer);
        
        var message = JsonSerializer.Serialize(request);
        PublishMessage(message);
        
        return response;
    }

    private void PublishMessage(string message)
    {
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

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }
}

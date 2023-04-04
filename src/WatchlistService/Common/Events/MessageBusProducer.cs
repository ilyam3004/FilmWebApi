using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WatchlistService.Dtos.Requests;
using WatchlistService.MessageBus.Requests;

namespace WatchlistService.Common.Events;

public class MessageBusProducer : IMessageBusProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly QueueDeclareOk _replyQueue;

    public MessageBusProducer(
        IConnection connection)
    {
        _connection = connection;
        _channel = connection.CreateModel();
        _replyQueue = _channel.QueueDeclare(queue: "",
            exclusive: false);
    }

    public async Task<string> PublishDecodeTokenMessage(DecodeTokenRequest request)
    {
        _channel.QueueDeclare(
            "request-queue", 
            exclusive: false);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        var response = "";

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            response = Encoding.UTF8.GetString(body);
            _channel.BasicAck(ea.DeliveryTag, false);
            await Task.Yield();
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
        
        Console.WriteLine("--> Sending request: {0}", message);
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

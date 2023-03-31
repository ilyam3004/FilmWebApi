using WatchlistService.Dtos.Requests;
using System.Text.Json;
using RabbitMQ.Client;
using System.Text;

namespace WatchlistService.SyncDataServices;

public class MessageBusClient : IMessageBusClient
{
    private readonly IConfiguration _configuration;
    private IConnection _connection;
    private IModel _channel;

    public MessageBusClient(IConfiguration configuration)
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

        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

        _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
    }

    public void PublishAuthUser(AuthUserRequest request)
    {
        var message = JsonSerializer.Serialize(request);

        if (_connection.IsOpen)
        {
            Console.WriteLine("Connection opened. Sending message to the message bus");
            SendMessage(message);
        }
        else
        {
            Console.WriteLine("Connection closed.");
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: "trigger",
            routingKey: "",
            basicProperties: null,
            body: body
        );

        Console.WriteLine($"Message have sent: {message}");
    }

    public void Dispose()
    {
        Console.WriteLine("--> Message Bus Disposed");
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("--> RabbitMQ Connection Shutdown");
    }
}

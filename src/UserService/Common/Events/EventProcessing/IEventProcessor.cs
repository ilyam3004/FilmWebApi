namespace UserService.EventProcessing;

public interface IEventProcessor
{
    string ProcessEvent(string message);
}
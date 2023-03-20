namespace UserService.Common.Services;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}


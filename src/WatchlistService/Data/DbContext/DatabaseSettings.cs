namespace WatchlistService.Data.DbContext;

public class DatabaseSettings
{
    public static string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
}
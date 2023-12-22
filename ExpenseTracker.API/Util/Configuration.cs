namespace ExpenseTracker.API.Util;

public class Configuration
{

    public static ConfigurationModel? CurrentConfiguration { get; set; }

    public static void UpdateConfiguration()
    {
        if (CurrentConfiguration == null)
        {
            CurrentConfiguration = new ConfigurationModel();    
        }
        CurrentConfiguration.DbConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "ERROR";
    }
}

public class ConfigurationModel
{
    public string DbConnectionString { get; set; }

}

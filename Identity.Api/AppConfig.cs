namespace Identity
{
    public class AppConfig
    {
        private static IConfiguration Configuration { get; set; }


        static AppConfig()
        {
            if (Configuration == null)
            {
                Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();
            }
        }


        public static string? GetConfigurationString() => Configuration.GetConnectionString("postgres");
    }
}

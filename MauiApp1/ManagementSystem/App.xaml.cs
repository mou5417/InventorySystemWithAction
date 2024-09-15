using Entities;
using System.Security.Cryptography.X509Certificates;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole;



namespace ManagementSystem
{
    public partial class App : Application
    {
       public static UserInfo UserInfo { get; set; }
       public App()
        {
           
            try

            {
               
                Log.Logger = new LoggerConfiguration().WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day).MinimumLevel.Information().CreateLogger();
                Log.Information("Log file create");
                InitializeComponent();
               
                MainPage = new AppShell();
                UserInfo = new UserInfo();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{ex.Message}");
            }
            
        }
    }
}

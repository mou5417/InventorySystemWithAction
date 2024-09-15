namespace ApiService
{
    public class Constants
    {
        public const string DatabaseFileName = "Systemdatabase.db3";

        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);

    }
}

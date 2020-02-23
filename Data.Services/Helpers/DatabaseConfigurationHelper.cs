namespace Artnivora.Studio.Portal.Data.Services.Helpers
{
	public class DatabaseConfigurationHelper
	{
		public const string databaseConnectionString = "Database"; // This is linked to the string in line 9

		public class ConnectionStrings
		{
			public string Database { get; set; }
		}

		public class AppSettings
		{
			public ConnectionStrings ConnectionStrings { get; set; }
		}

	}
}

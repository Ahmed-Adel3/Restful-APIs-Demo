using Microsoft.Extensions.Configuration;

namespace Assignment.Infrastructure.Options
{
    public class DatabaseConnectionOptions
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public static string GetConnectionString(IConfiguration Configuration)
        {
            DatabaseConnectionOptions db = Configuration.GetSection("DBConnection").Get<DatabaseConnectionOptions>();
            var connection =  $"Data Source={db.Server};Initial Catalog={db.DatabaseName};MultipleActiveResultSets=true;";
            if (!string.IsNullOrWhiteSpace(db.Password))
                connection += $"User ID ={db.UserId}; Password ={db.Password}";

            return connection;
        }
    }
}

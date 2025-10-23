using Npgsql;

namespace Sidh_Api.Database;

public class PostgreSqlTcp
{
    private  IConfiguration _configuration;
    private  string INSTANCE_HOST;
    private  string DB_USER;
    private  string DB_PASS;
    private  string DB_NAME;
    public  PostgreSqlTcp(IConfiguration configuration)
    { 
      _configuration= configuration;        
    }
    public  NpgsqlConnectionStringBuilder NewPostgreSqlTCPConnectionString()
    {
        INSTANCE_HOST = _configuration["ConnectionStrings:INSTANCE_HOST"];
        DB_USER = _configuration["ConnectionStrings:DB_USER"];
        DB_PASS = _configuration["ConnectionStrings:DB_PASS"];
        DB_NAME = _configuration["ConnectionStrings:DB_NAME"];
        var connectionString = new NpgsqlConnectionStringBuilder()
        {
            Host = INSTANCE_HOST,
            Username = DB_USER,
            Password = DB_PASS,
            Database = DB_NAME,
            SslMode = SslMode.Prefer,
        
        };

        if (Environment.GetEnvironmentVariable("DB_CERT") != null)
        {
            connectionString.SslMode = SslMode.VerifyCA;
            connectionString.SslCertificate =
                Environment.GetEnvironmentVariable("DB_CERT"); 
        }
        connectionString.Pooling = true;
        connectionString.MaxPoolSize = 1500;
        connectionString.MinPoolSize = 20;
        connectionString.Timeout = 500;
        connectionString.ConnectionIdleLifetime = 500;
        connectionString.SslMode= SslMode.Prefer;
        connectionString.TrustServerCertificate=true;
        
        return connectionString;
    }
}

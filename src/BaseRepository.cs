using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class BaseRepository
{
    IConfiguration _configuration;

    public BaseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private NpgsqlConnection SqlConnection()
    {
        var stringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = _configuration["PGHOST"],
            Database = _configuration["PGDATABASE"],
            Username = _configuration["PGUSER"],
            Port = Int32.Parse(_configuration["PGPORT"]),
            Password = _configuration["PGPASSWORD"],
            SslMode = SslMode.Require,
            TrustServerCertificate = true
        };
        return new NpgsqlConnection(stringBuilder.ConnectionString);
    }


    public IDbConnection CreateConnection()
    {
        var conn = SqlConnection();
        conn.Open();
        return conn;
    }

}
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;

public class BusinessRepository : BaseRepository, IRepository<Business>
{
    public BusinessRepository(IConfiguration configuration) : base(configuration){}

    public async Task<IEnumerable<Business>> GetAll()
    {
        using var connection = CreateConnection();
        //update sql query once table is set
        return await connection.QueryAsync<Business>("SELECT * FROM Business;");
    }

    public void Delete(long id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Busienss WHERE Id=@Id;", new {Id = id});
    }

    public async Task<Business> Get (long id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("SELECT * FROM Business WHERE Id= @Id;", new {Id = id});
    }


//UPDATE SQL for all of the below!
     public async Task<Business> Update(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("UPDATE Business SET Date = @Date, Title = @Title, Description = @Description, Industry = @Industry WHERE Id = @Id RETURNING *", Business);
    }

    public async Task<Business> Insert(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("INSERT INTO Business (Date, Title, Description, Industry) VALUES (@Date, @Title, @Description, @Industry) RETURNING *;", Business);
    }

    public async Task<IEnumerable<Business>> Search(string query)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Business>("SELECT * FROM Business WHERE Title ILIKE @Query OR Description ILIKE @Query OR Industry ILIKE @Query;", new { Query = $"%{query}%" });
    }

}
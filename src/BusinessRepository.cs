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
        return await connection.QueryAsync<Business>("SELECT * FROM Business INNER JOIN Product ON id = BusinessId;");
    }

    public void Delete(long id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Business WHERE Id=@Id;", new {Id = id});
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
        return await connection.QuerySingleAsync<Business>("UPDATE Business SET BusinessName = @BusinessName, PrimaryContact = @PrimaryContact, AddrBuildingName = @AddrBuildingName, AddrBuildingNumber = @AddrBuildingNumber, AddrStreet = @AddrStreet, AddrCity = @AddrCity, AddrCounty = @AddrCounty, AddrPostcode = @AddrPostcode, TelephoneNumber = @TelephoneNumber, TwitterHandle = @TwitterHandle, SocialmediaLink = @SocialmediaLink, BusinessImage = @BusinessImage, IsTrading = @IsTrading WHERE Id = @Id RETURNING *", Business);
    }

    public async Task<Business> Insert(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("INSERT INTO Business (BusinessName, PrimaryContact, AddrBuildingName, AddrBuildingNumber, AddrStreet, AddrCity, AddrCounty, AddrPostcode,TelephoneNumber,TwitterHandle,SocialmediaLink,BusinessImage,IsTrading) VALUES (@BusinessName, @PrimaryContact, @AddrBuildingName, @AddrBuildingNumber, @AddrStreet, @AddrCity,@AddrCounty, @AddrPostcode, @TelephoneNumber, @TwitterHandle,@SocialmediaLink, @BusinessImage, @IsTrading) RETURNING *;", Business);
    }

    // this is vacant for our business database
    public async Task<IEnumerable<Business>> Search(string query)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Business>("SELECT * FROM Business WHERE BusinessName ILIKE @Query;", new { Query = $"%{query}%" });
    }

}
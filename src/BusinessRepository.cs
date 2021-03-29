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
        return await connection.QueryAsync<Business>("SELECT * FROM Business LEFT JOIN Product ON id = BusinessId;"); 
    }


    public void DeletebyBusiness(long Id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Business WHERE Id=@Id;", new {Id = Id});
    }



    public void DeletebyProduct(long ProductId)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Product WHERE ProductId=@ProductId;", new {ProductId = ProductId});
    }



    public async Task<Business> GetbyBusiness (long Id)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("SELECT * FROM Business WHERE Id=@Id;", new {Id = Id});
    }



     public async Task<Business> GetbyProduct (long ProductId)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("SELECT * FROM Product WHERE ProductId=@ProductId;", new {ProductId = ProductId});
    }



     public async Task<Business> UpdatebyBusiness(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("UPDATE Business SET BusinessName = @BusinessName, PrimaryContact = @PrimaryContact, AddrBuildingName = @AddrBuildingName, AddrBuildingNumber = @AddrBuildingNumber, AddrStreet = @AddrStreet, AddrCity = @AddrCity, AddrCounty = @AddrCounty, AddrPostcode = @AddrPostcode, TelephoneNumber = @TelephoneNumber, TwitterHandle = @TwitterHandle, SocialmediaLink = @SocialmediaLink, BusinessImage = @BusinessImage, IsTrading = @IsTrading WHERE Id = @Id RETURNING *", Business);
    }


     public async Task<Business> UpdatebyProduct(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("UPDATE Product SET ProductName = @ProductName, ProductType = @ProductType, ProductDescription = @ProductDescription, ProductImage = @ProductImage, ProductPrice = @ProductPrice, UnitSize = @UnitSize, Quantity = @Quantity WHERE ProductId = @ProductId RETURNING *", Business);
    }


    public async Task<Business> InsertbyBusiness(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("INSERT INTO Business (BusinessName, PrimaryContact, AddrBuildingName, AddrBuildingNumber, AddrStreet, AddrCity, AddrCounty, AddrPostcode,TelephoneNumber,TwitterHandle,SocialmediaLink,BusinessImage,IsTrading) VALUES (@BusinessName, @PrimaryContact, @AddrBuildingName, @AddrBuildingNumber, @AddrStreet, @AddrCity,@AddrCounty, @AddrPostcode, @TelephoneNumber, @TwitterHandle,@SocialmediaLink, @BusinessImage, @IsTrading) RETURNING *;", Business);
    }



    public async Task<Business> InsertbyProduct(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("INSERT INTO Product (BusinessId, ProductName, ProductType, ProductDescription, ProductImage, ProductPrice, UnitSize, Quantity) VALUES (@BusinessId, @ProductName, @ProductType, @ProductDescription, @ProductImage, @ProductPrice, @UnitSize, @Quantity) RETURNING *;", Business);
    }



    public async Task<IEnumerable<Business>> Search(string query)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Business>("SELECT * FROM Business LEFT JOIN Product ON id = BusinessId WHERE BusinessName ILIKE @Query OR ProductName ILIKE @Query;", new { Query = $"%{query}%" });
    }

}
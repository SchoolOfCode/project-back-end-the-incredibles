using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;
using System;

public class BusinessRepository : BaseRepository, IRepository<Business>
{
    //gets business by Auth0Id
    public async Task<Business> GetbyBusiness (string Auth)
    {
        using var connection = CreateConnection();
        var business = await connection.QuerySingleAsync<Business>("SELECT * FROM Business WHERE auth0Id=@Auth;", new {Auth = Auth});

        return business;
    }
    public async Task<Business> GetbyBusinessName(string Name)
    {
        string businessName = Name.Replace("-", " ");
        using var connection = CreateConnection();
        var business = await connection.QuerySingleAsync<Business>("SELECT * FROM Business WHERE businessName ILIKE @businessName;", new { businessName = businessName });

        return business;
    }
    //Gets List of Products from BusinessID
    public async Task<IEnumerable<Product>> GetProducts(long Id)
    {
        using var connection = CreateConnection();
        
        return await connection.QueryAsync<Product>("SELECT * FROM Product WHERE businessID=@Id", new { Id = Id });
    }


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





    public async Task<Product> GetbyProduct (long ProductId)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Product>("SELECT * FROM Product WHERE ProductId=@ProductId;", new {ProductId = ProductId});
    }



     public async Task<Business> UpdatebyBusiness(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("UPDATE Business SET BusinessName = @BusinessName, PrimaryEmail = @PrimaryEmail, AddrLocation = @AddrLocation, TelephoneNumber = @TelephoneNumber, BusinessLogo = @BusinessLogo, IsTrading = @IsTrading WHERE Id = @Id RETURNING *", Business);
    }


     public async Task<Business> UpdatebyProduct(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("UPDATE Product SET ProductName = @ProductName, ProductDescription = @ProductDescription, ProductImage = @ProductImage, ProductPrice = @ProductPrice, Quantity = @Quantity WHERE ProductId = @ProductId RETURNING *", Business);
    }


    public async Task<Business> InsertbyBusiness(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("INSERT INTO Business (auth0Id) VALUES (@Auth0Id) RETURNING *;", Business);
    }



    public async Task<Product> InsertbyProduct(Product product)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Product>("INSERT INTO Product (BusinessId, ProductName, ProductType, ProductDescription, ProductImage, ProductPrice, Quantity) VALUES (@BusinessId, @ProductName, @ProductDescription, @ProductImage, @ProductPrice, @Quantity) RETURNING *;", product);
    }



    public async Task<IEnumerable<Business>> Search(string query)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Business>("SELECT * FROM Business LEFT JOIN Product ON id = BusinessId WHERE BusinessName ILIKE @Query OR ProductName ILIKE @Query;", new { Query = $"%{query}%" });
    }

}
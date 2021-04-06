using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Threading.Tasks;
using System;

public class BusinessRepository : BaseRepository, IRepository<Business>
{
    //gets business by Auth0Id
    public async Task<Business> GetbyBusiness(string Auth)
    {
        using var connection = CreateConnection();
        var business = await connection.QuerySingleAsync<Business>("SELECT * FROM Business WHERE auth0Id=@Auth;", new { Auth = Auth });

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


    public BusinessRepository(IConfiguration configuration) : base(configuration) { }


    public async Task<IEnumerable<Business>> GetAll()
    {
        using var connection = CreateConnection();
        // return await connection.QueryAsync<Business>("SELECT * FROM Business LEFT JOIN Product ON id = BusinessId;"); 
        return await connection.QueryAsync<Business>("SELECT * FROM Business;");
    }


    public void DeletebyBusiness(long Id)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Business WHERE Id=@Id;", new { Id = Id });
    }



    public void DeletebyProduct(long ProductId)
    {
        using var connection = CreateConnection();
        connection.Execute("DELETE FROM Product WHERE ProductId=@ProductId;", new { ProductId = ProductId });
    }





    public async Task<Product> GetbyProduct(long ProductId)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Product>("SELECT * FROM Product WHERE ProductId=@ProductId;", new { ProductId = ProductId });
    }



    public async Task<Business> UpdatebyBusiness(Business Business)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("UPDATE Business SET BusinessName = @BusinessName, PrimaryEmail = @PrimaryEmail, AddrLocation = @AddrLocation, TelephoneNumber = @TelephoneNumber, BusinessLogo = @BusinessLogo, IsTrading = @IsTrading WHERE Id = @Id RETURNING *", Business);
    }


    public async Task<Product> UpdatebyProduct(Product product)
    {
        Console.WriteLine(product.ProductId);
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Product>("UPDATE Product SET BusinessID = @BusinessId, ProductId = @ProductId, ProductName = @ProductName, ProductImage = @ProductImage, ProductPrice = @ProductPrice, Quantity = @Quantity WHERE ProductId = @ProductId RETURNING *", product);
    }


    public async Task<Business> InsertbyBusiness(Business Business)
    {
        
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Business>("INSERT INTO Business (auth0Id, BusinessName, PrimaryEmail, AddrLocation, BusinessLogo) VALUES (@Auth0Id, @BusinessName, @PrimaryEmail, @AddrLocation, @BusinessLogo) RETURNING *;", new Business{ Auth0Id = Business.Auth0Id, BusinessName = Business.BusinessName, PrimaryEmail = Business.PrimaryEmail, AddrLocation = Business.AddrLocation, BusinessLogo = Business.BusinessLogo});
    }



    public async Task<Product> InsertbyProduct(Product product)
    {
        using var connection = CreateConnection();
        return await connection.QuerySingleAsync<Product>("INSERT INTO Product (BusinessId, ProductName, ProductImage, ProductPrice, Quantity) VALUES (@BusinessId, @ProductName, @ProductImage, @ProductPrice, @Quantity) RETURNING *;", product);
    }



    public async Task<IEnumerable<Business>> Search(string query)
    {
        using var connection = CreateConnection();
        return await connection.QueryAsync<Business>("SELECT * FROM Business LEFT JOIN Product ON id = BusinessId WHERE BusinessName ILIKE @Query OR ProductName ILIKE @Query;", new { Query = $"%{query}%" });
    }

}
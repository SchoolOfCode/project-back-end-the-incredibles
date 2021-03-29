using System;


public class Business
{

    public long Id { get; set; }

    public string Auth0Id { get; set; }

    public string BusinessName { get; set; }

    public string PrimaryEmail { get; set; }

    public string AddrLoaction { get; set; }

    public int TelephoneNumber { get; set; }

    public string BusinessLogo { get; set; }

    public bool IsTrading {get; set;} 

    public int BusinessId {get; set;}

    public int ProductId {get; set;}

    public string ProductName {get; set;}

    public string ProductDescription {get; set;}

    public string ProductImage {get; set;}
    
    public string ProductPrice {get; set;}
    
    public int Quantity {get; set; }

}
